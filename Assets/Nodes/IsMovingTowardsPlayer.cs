using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Player")]
public class IsMovingTowardsPlayer : ConditionTask {

    protected override bool OnCheck() {
        var player = GameObject.FindWithTag("Player");
        if (!player) {
            return false;
        }

        var playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        var myVelocity = agent.GetComponent<Rigidbody2D>().velocity;

        var relativeVelocity = myVelocity - playerVelocity;
        relativeVelocity.Normalize();

        var dirToPlayer = player.transform.position - agent.transform.position;
        dirToPlayer.Normalize();

        return Vector3.Angle(relativeVelocity, dirToPlayer) < 90f;
    }

}
