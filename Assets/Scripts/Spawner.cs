using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform player;
    public GameObject enemyPrefab;

    public float spawnChance = 2f;
    public float pushAmount = 40f;

    private void Update() {
        if (!player) {
            return;
        }

        if (!(Random.Range(0f, 100f) < spawnChance)) {
            return;
        }

        // spawn an enemy

        var distance = Camera.main.orthographicSize * 2;
        var angle = Random.Range(0f, Mathf.PI * 2);

        var x = distance * Mathf.Cos(angle) + player.position.x;
        var y = distance * Mathf.Sin(angle) + player.position.y;

        var enemy = Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        var body = enemy.GetComponent<Rigidbody2D>();

        var direction = (player.position - enemy.transform.position).normalized;

        body.AddForce(direction * pushAmount);
    }

}
