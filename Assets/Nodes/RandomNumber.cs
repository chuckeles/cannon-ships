using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;
using Random = UnityEngine.Random;

[Category("Utilities")]
public class RandomNumber : CallableFunctionNode<int, int, int> {

    public override int Invoke(int from , int to) {
        return (int) Mathf.Floor(Random.value * (to - from) + from);
    }

}
