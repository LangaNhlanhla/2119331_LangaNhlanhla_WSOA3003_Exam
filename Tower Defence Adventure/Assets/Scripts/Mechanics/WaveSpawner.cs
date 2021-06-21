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
    public Wave[] waves;
    public Transform spawnArea;
    public Text countdownTxt;

    [Header("Floats")]
    public float timebetweenWaves = 7f;
    float countdown = 2f;

    [Header("Intergers")]
    int waveIndex;


    void Update()
    {
        if (goblinsAlive > 0)
            return;

        if(countdown <= 0f)
		{
            StartCoroutine(SpawnWave());
            countdown = timebetweenWaves;
            return;
		}
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        countdownTxt.text = string.Format("{0:00.00}", countdown);
    }

    void SpawnEnemy(GameObject goblin)
	{
        Instantiate(goblin, spawnArea.position, Quaternion.identity);
        goblinsAlive++;
	}
    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.goblin);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

        if (waveIndex == waves.Length)
        { 
            Debug.Log("GGs");
            this.enabled = false;
        }
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, spawnArea.position);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
