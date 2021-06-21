using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Static Values")]
    public static int goblinsAlive = 0;

    [Header("Unity Handles")]
    public Transform enemyPrefab;
    public Transform spawnArea;
    public Text countdownTxt;

    [Header("Floats")]
    public float timebetweenWaves = 7f;
    float countdown = 2f;

    [Header("Intergers")]
    int waveIndex;


    void Update()
    {
        if(countdown <= 0f)
		{
            StartCoroutine(SpawnWave());
            countdown = timebetweenWaves;
		}
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        countdownTxt.text = string.Format("{0:00.00}", countdown);
    }

    void SpawnEnemy()
	{
        Instantiate(enemyPrefab, spawnArea.position, spawnArea.rotation);
        goblinsAlive++;
	}
    IEnumerator SpawnWave()
	{
        waveIndex++;
		for (int i = 0; i < waveIndex; i++)
		{
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
