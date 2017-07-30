using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform player;
    public GameObject enemyPrefab;
    public GameObject crystalPrefab;

    public float initialSpawnChance = .6f;
    public float pushAmount = 40f;
    public float chanceIncrease = 30;

    private float elapsed = 0;

    private void Update() {
        if (!player) {
            return;
        }

        elapsed += Time.deltaTime;

        var playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        var spawnChance = initialSpawnChance;
        spawnChance += elapsed / chanceIncrease;

        // account for changed window size
        spawnChance += (Mathf.Max(Screen.width, Screen.height) - 500) / 1400f;

        var distance = Camera.main.orthographicSize * 3;

        // spawn an enemy
        if (Random.Range(0f, 100f) < spawnChance) {
            var angle = Random.Range(0f, Mathf.PI * 2);

            var x = distance * Mathf.Cos(angle) + player.position.x;
            var y = distance * Mathf.Sin(angle) + player.position.y;

            var enemy = Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            var body = enemy.GetComponent<Rigidbody2D>();

            var direction = (player.position - enemy.transform.position).normalized;

            body.velocity = playerVelocity;
            body.AddForce(direction * pushAmount);
        }

        // spawn a crystal
        if (Random.Range(0f, 100f) < spawnChance * .5f + .5) {
            var angle = Random.Range(0f, Mathf.PI * 2);

            var x = distance * Mathf.Cos(angle) + player.position.x;
            var y = distance * Mathf.Sin(angle) + player.position.y;

            var crystal = Instantiate(crystalPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            var body = crystal.GetComponent<Rigidbody2D>();

            var direction = (player.position - crystal.transform.position).normalized;

            body.velocity = playerVelocity;
            body.AddForce(direction * pushAmount);
        }
    }

}
