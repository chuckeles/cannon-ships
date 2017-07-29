using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Utilities")]
public class IsShip : CallableFunctionNode<bool, GameObject> {

    public override bool Invoke(GameObject gameObject) {
        return gameObject.GetComponent<Ship>() != null;
    }

}
