using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Enemy")]
public class Aim : ActionTask {

    public bool opposite = false;

    protected override void OnExecute() {
        var player = Camera.main; // use camera instead
        if (!player) {
            EndAction(false);
            return;
        }

        // update precision
        var precision = blackboard.GetVariable<float>("precision");
        var aimingValue = Random.Range(-2f, 1f) * Mathf.Sign(precision.value);

        precision.value += aimingValue;

        // get the angle to the player
        var vector = player.transform.position - agent.transform.position;
        var angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        if (opposite) {
            angle += 180f;
        }

        // move cannon
        var cannon = agent.transform.GetChild(0);
        cannon.rotation = Quaternion.Euler(0, 0, angle + precision.value);

        EndAction(true);
    }

}
