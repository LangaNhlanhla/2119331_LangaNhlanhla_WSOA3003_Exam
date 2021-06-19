using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
	[Header("External Sources")]
	GameManager gameManager;

    [Header("Unity Handles")]
    public Color hoveringColour;
    private Color defaultColour;
	private Renderer nodeRenderer;

    [Header("Game Buildings")]
    public GameObject building;

	[Header("Unity Attributes")]
	public Vector3 yOffset;

	private void Awake()
	{
		nodeRenderer = GetComponent<Renderer>();
		defaultColour = nodeRenderer.material.color;
		gameManager = GameManager.instance;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;


		if(building != null)
		{
			Debug.Log("Can't Build");
			return;
		}

		//GameObject build = GameManager.instance.GetBuilding();
		//building = (GameObject)Instantiate(build, transform.position + yOffset, transform.rotation);
	}

	private void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		nodeRenderer.material.color = hoveringColour;
	}

	private void OnMouseExit()
	{
		nodeRenderer.material.color = defaultColour;
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
