using UnityEngine;

public class DeleteOutsideView : MonoBehaviour {

    private void Update() {
        var size = Screen.width;
        var positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (positionOnScreen.x < -size || positionOnScreen.x > size * 2 || positionOnScreen.y < -size
            || positionOnScreen.y > size * 2) {
            Destroy(gameObject);
        }
    }

}
