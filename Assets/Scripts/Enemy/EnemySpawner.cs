using System.Collections;
using System.Collections.Generic;
using TP;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [FormerlySerializedAs("path")] public EnemyPath enemyPath;
    public List<GameObject> enemys;
    public List<int> enemysAmount;
    [SerializeField] private int enemysToSpawn;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float timeBetweenGroups;
    [SerializeField] private WaveManager waveManager;

    private void Start()
    {
        //StartCoroutine(spawnEnemies());
    }

    public IEnumerator spawnEnemies()
    {
        Debug.Log("Spawning has commenced");
        for(int i = 0; i < enemys.Count; i++)
        {
            Debug.Log(i);
            for (int j = 0; j < enemysAmount[i]; j++)
            {
                Debug.Log(j);

                Debug.Log("spawn");

                GameObject enemySpawned = Instantiate(enemys[i], transform.position, Quaternion.identity, this.transform);
                
                Enemy eS = enemySpawned.GetComponent<Enemy>();
                eS.enemyPath = enemyPath;
                eS.nextNode = eS.enemyPath.node[0];

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(timeBetweenGroups);
        }
        yield return new WaitForSeconds(timeBetweenSpawns);

        waveManager.WaveEnd();
    }
}
