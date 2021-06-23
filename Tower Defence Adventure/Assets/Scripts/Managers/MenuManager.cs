using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[Header("Unity Handles")]
	public GameObject TransitionIMGOUT;
	public GameObject TransitionIMGIN;

	[Header("Floats")]
	public float TweenTime;
	public float delay;

	[Header("Generic Elements")]
	public string sceneName;

	private void Start()
	{
		TransitionIMGIN = GameObject.FindGameObjectWithTag("Entrance");
		TransitionIMGOUT = GameObject.FindGameObjectWithTag("Exit");

		//TransitionIMGIN.transform.position = new Vector3(-Screen.width * 2, Screen.height / 2, 0f);
		TransitionIMGOUT.LeanMoveX(-Screen.width * 1.5f, TweenTime);
	}
	public void PlayGame()
	{
		StartCoroutine(TransitionToPlay());
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	IEnumerator TransitionToPlay()
	{
		TransitionIMGIN.LeanMoveX(Screen.width / 1.5f, TweenTime);

		yield return new WaitForSeconds(delay);

		SceneManager.LoadScene(sceneName);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
