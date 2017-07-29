using FlowCanvas;
using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Utilities")]
public class IsGraphOwner : CallableFunctionNode<bool, GameObject> {

    public override bool Invoke(GameObject gameObject) {
        return gameObject.GetComponent<FlowScriptController>();
    }

}
