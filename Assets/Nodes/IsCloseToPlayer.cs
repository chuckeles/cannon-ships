using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Player")]
public class IsCloseToPlayer : ConditionTask {

    protected override bool OnCheck() {
        var player = GameObject.FindWithTag("Player");
        if (!player) {
            return false;
        }

        var playerPosition = player.transform.position;
        var position = agent.transform.position;
        var cameraSize = Camera.main.orthographicSize * 1.2f;

        return Vector3.Distance(playerPosition, position) < cameraSize;
    }

}
