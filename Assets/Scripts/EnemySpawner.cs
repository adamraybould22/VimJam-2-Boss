using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("A List of Enemies that can be Spawned")]
    public List<GameObject> listOfEnemies;

    [Tooltip("The Time for Each Spawn")]
    public float spawnRate;

    private BoxCollider2D SpawnArea;

    private void Awake()
    {
        SpawnArea = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomEnemy());
    }

    private IEnumerator SpawnRandomEnemy()
    {
        GameObject enemyToSpawn = listOfEnemies[Random.Range(0, listOfEnemies.Count)];
        Vector3 positionToSpawn = transform.position + (Vector3)(Random.insideUnitCircle * SpawnArea.size);

        GameObject enemy = Instantiate(enemyToSpawn, positionToSpawn, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - transform.position).normalized * 6000, ForceMode2D.Impulse);

        yield return new WaitForSeconds(spawnRate); 
        StartCoroutine(SpawnRandomEnemy());
    }
}
