using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave;
    public SO_WaveList waves;
    public EnemySpawner enemySpawner;
    private SO_Wave[] wavesInfo;

    public void Start()
    {
        wavesInfo = waves.waves;
        WaveStart();
    }

    public void WaveStart()
    {
        if (currentWave >= wavesInfo.Length) return;
        for(int i = 0; i < wavesInfo[currentWave].enemy.Length; i++)
        {
            if (wavesInfo[currentWave].enemy[i] == null || wavesInfo[currentWave].nbEnemy[i] <= 0) continue;
            enemySpawner.enemys.Add(wavesInfo[currentWave].enemy[i]);
            enemySpawner.enemysAmount.Add(wavesInfo[currentWave].nbEnemy[i]);
        }

        StartCoroutine(enemySpawner.spawnEnemies());

    }

    public void WaveEnd()
    {
        currentWave++;
        enemySpawner.enemys.Clear();
        enemySpawner.enemysAmount.Clear();
        WaveStart();
    }
}
