using System;
using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Random = UnityEngine.Random;

[Category("Enemy")]
public class Aim : ActionTask {

    public bool opposite = false;
    public float timeToAim = .6f;

    protected override void OnExecute() {
        var player = Camera.main; // use camera instead
        if (!player) {
            EndAction(false);
            return;
        }

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate() {
        // update precision
        var precision = blackboard.GetVariable<float>("precision");
        var aimingValue = Random.Range(-4f, 2f) * Mathf.Sign(precision.value);

        precision.value += aimingValue;

        // get the angle to the player
        var vector = Camera.main.transform.position - agent.transform.position;
        var targetAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        if (opposite) {
            targetAngle += 180f;
            if (targetAngle > 360) {
                targetAngle -= 360;
            }
        }

        // move cannon
        var cannon = agent.transform.GetChild(0);
        var currentAngle = cannon.rotation.eulerAngles.z;
        var currentAngleQ = Quaternion.Euler(0, 0, currentAngle);
        var targetAngleQ = Quaternion.Euler(0, 0, targetAngle);

        var diff = Quaternion.Angle(currentAngleQ, targetAngleQ);
        var targetTime = timeToAim / 180f * diff;

        for (var elapsed = 0f; elapsed < targetTime; elapsed += Time.deltaTime) {
            cannon.rotation = Quaternion.Slerp(currentAngleQ, targetAngleQ, elapsed / targetTime);
            yield return null;
        }

        EndAction(true);
        yield return null;
    }

}
