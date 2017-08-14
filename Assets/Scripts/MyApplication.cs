using UnityEngine;
using UnityEngine.SceneManagement;


public class MyApplication : MonoBehaviour {

    private void Update() {
        if (Input.GetButtonDown("Restart")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetButtonDown("Quit")) {
            Application.Quit();
        }
    }

}
