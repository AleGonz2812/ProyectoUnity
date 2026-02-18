using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemyPrefab;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            enabled = false;
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave(waves[waveIndex]));
            waveIndex++;
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.name);

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        EnemiesAlive++;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
