using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Header("Floats")]
    [SerializeField] float duration = 1.5f;

	private void Start()
	{
		Destroy(gameObject, duration);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
