using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    public Path path;
    [SerializeField] private List<GameObject> enemys;
    [SerializeField] private List<int> enemysAvailable;
    [SerializeField] private int enemysToSpawn;
    [SerializeField] private float timeBetweenSpawns;

    private void Start()
    {
        StartCoroutine(spawnEnemies());
    }

    private IEnumerator spawnEnemies()
    {
        for (int i = 0; i < enemysToSpawn; i++)
        {
            Debug.Log("spawn");


            GameObject enemySpawned = Instantiate(enemys[Random.Range(0, enemys.Count - 1)], transform.position, Quaternion.identity, this.transform);
            Enemy eS = enemySpawned.GetComponent<Enemy>();
            eS.path = path;
            eS.nextNode = eS.path.node[0];
            
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
    }
}
