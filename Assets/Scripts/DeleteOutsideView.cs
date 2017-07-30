using UnityEngine;

public class DeleteOutsideView : MonoBehaviour {

    private void Update() {
        var size = Mathf.Max(Screen.width, Screen.height);
        var positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (positionOnScreen.x < -size * 2 || positionOnScreen.x > size * 3 || positionOnScreen.y < -size * 2 ||
            positionOnScreen.y > size * 3) {
            Destroy(gameObject);
        }
    }

}
