using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("External Sources")]
    PlayerInputs inputActions;
	CharacterController controller;

    [Header("Unity Handles")]
    public Rigidbody rb;
	Vector2 horizontalMovement;

	[Header("Floats")]
	public float speed = 10f;

	private void Awake()
	{
        inputActions = new PlayerInputs();
		controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

		inputActions.Movement.Move.performed += ctx => horizontalMovement = ctx.ReadValue<Vector2>();
	}

	private void OnEnable()
	{
        inputActions.Enable();
	}

	private void OnDisable()
	{
		inputActions.Disable();
	}

	void Update()
    {
		Vector3 velocity = (Vector3.right * horizontalMovement.x + Vector3.forward * horizontalMovement.y) * speed;
		controller.Move(velocity * Time.deltaTime);
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
