using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Utilities")]
public class Rad2Deg : CallableFunctionNode<float, float> {

    public override float Invoke(float radians) {
        return radians * Mathf.Rad2Deg;
    }

}
