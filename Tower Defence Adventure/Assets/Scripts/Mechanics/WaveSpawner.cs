using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Static Values")]
    public static int goblinsAlive = 0;

    [Header("External Sources")]
    public TweenAnimations anim;

    [Header("Unity Handles")]
    public Wave[] waves;
    public Transform spawnArea;
    public Text countdownTxt, WaveTxt;

    [Header("Floats")]
    public float timebetweenWaves = 7f;
    public float countdown = 2f;

    [Header("Intergers")]
    int waveIndex;
    int indexWave;

    [Header("Booleans")]
    public bool hasBeenCalled = false;
    void Update()
    {
        if (goblinsAlive > 0)
            return;
        
        if(countdown <= 15f && !hasBeenCalled)
		{
            anim.gameObject.SetActive(true);
            hasBeenCalled = true;
		}

        if(countdown <= 0f)
		{
            StartCoroutine(SpawnWave());
            countdown = timebetweenWaves;
            hasBeenCalled = false;
            return;
		}
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        indexWave = waveIndex + 1;
        
        countdownTxt.text = countdown.ToString("F1") + "s";
        WaveTxt.text = "Wave " + indexWave;

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
