using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive;
    public Wave[] waves;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countDown = 5f;

    public Text waveCountDownText;

    private int waveIndex = 0;

    public GameManager gameManager;

    private void Start()
    {
        enemiesAlive = 0;
    }

    private void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length && PlayerStats.amountOfLives > 0 && SceneManager.GetActiveScene().name != "Level20")
        {
            gameManager.WinLevel();
            enabled = false;
        }

        if (waveIndex == waves.Length && PlayerStats.amountOfLives > 0 && SceneManager.GetActiveScene().name == "Level20")
        {
            gameManager.FinalScene();
            enabled = false;
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);

        waveCountDownText.text = string.Format("{0:00.00}", countDown);

    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        enemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }

        waveIndex++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
