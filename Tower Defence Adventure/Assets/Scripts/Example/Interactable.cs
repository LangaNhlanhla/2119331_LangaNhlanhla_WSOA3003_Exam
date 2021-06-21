using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("External Sources")]
    PlayerInputs inputActions;

    [Header("Booleans")]
    public bool inRange;

    [Header("Unity Handles")]
    public UnityEvent action, a;

	#region Enable/Disable
	private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
#endregion

	private void Awake()
	{
        inputActions = new PlayerInputs();
    }
    void Update()
    {
        RangeCheck();
    }

    void RangeCheck()
	{
        if (inRange)
        {
            inputActions.Interactions.Interact.performed += ctx => action.Invoke();
        }
		else
		{
            a.Invoke();
		}

		if(GameManager.instance.shopOpen)
		{
            inputActions.Interactions.Escape.performed += ctx => a.Invoke();
		}
    }
	private void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
            inRange = true;
            PlayerManager.playerManager.NotifyPlayer();
            Debug.Log("In Range");
		}
	}

	private void OnTriggerExit(Collider collision)
	{
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            PlayerManager.playerManager.UnNotifyPlayer();
            Debug.Log("Out Of Range");
        }
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/


