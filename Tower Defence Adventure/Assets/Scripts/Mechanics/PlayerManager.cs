using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	[Header("Unity Handles")]
    public GameObject notify;

    [Header("Singleton")]
    public static PlayerManager playerManager;


	private void Awake()
	{

		if (playerManager != null && playerManager != this)
		{
			Destroy(this.gameObject);
			return;
		}
		playerManager = this;
		DontDestroyOnLoad(this);
	}



	public void NotifyPlayer()
	{
		notify.SetActive(true);

	}

    public void UnNotifyPlayer()
	{
        notify.SetActive(false);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/


