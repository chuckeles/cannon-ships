using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine.SceneManagement;

[Category("Game")]
public class RestartGame : CallableActionNode {

    public override void Invoke() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
