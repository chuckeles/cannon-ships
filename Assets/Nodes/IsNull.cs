using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Utilities")]
public class IsNull : CallableFunctionNode<bool, Object> {

    public override bool Invoke(Object obj) {
        return obj == null;
    }

}
