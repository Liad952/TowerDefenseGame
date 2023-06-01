using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawn;
    public float timeBetweenWaves = 5f;
    private float countDown = 3f;
    private int waveNumber = 0;
    public Text wave;
    public Text waveTimer;

    void Update()
    {
        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        if(GameMaster.GameOver)
        {
            return;
        }
        else
        {
            countDown -= Time.deltaTime;
        }

        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);

        GameMaster.tempWaveNum = waveNumber;
        wave.text = waveNumber.ToString();
        waveTimer.text = string.Format("{0:00.00}", countDown);

    }


    IEnumerator SpawnWave()
    {
        waveNumber++;

        for (int i = 0;i< waveNumber;i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawn.position, transform.rotation);

    }

}
