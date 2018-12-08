using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfig;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Use this for initialization
    // 初始接口start方法，循环敌人路线
	IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
	}
    // 利用Coroutine增加敌人军队waves
    private IEnumerator SpawnAllWaves()
    {
        for (int wavesIndex = startingWave; wavesIndex < waveConfig.Count; wavesIndex++)
        {
            var currentWave = waveConfig[wavesIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    // 延续敌人军队waves
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveconfig)
    {
        for (int enemyCount = 0; enemyCount < waveconfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveconfig.GetEnemyPrefab(),
                waveconfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveconfig);
            yield return new WaitForSeconds(waveconfig.GetTimeBetweenSpawns());
        }   
    }

}
