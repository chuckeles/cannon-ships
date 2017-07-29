using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Player")]
public class IsPlayerAlive : ConditionTask {

    protected override bool OnCheck() {
        return GameObject.FindWithTag("Player") != null;
    }

}
