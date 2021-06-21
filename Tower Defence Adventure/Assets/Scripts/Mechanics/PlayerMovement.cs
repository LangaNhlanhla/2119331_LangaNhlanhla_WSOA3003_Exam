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
	public Transform spawnPoint;
	public GameObject bullet, sword, holdBullets;
	Camera main;
	Vector2 horizontalMovement;

	[Header("Floats")]
	public float speed = 10f;

	private void Awake()
	{
        inputActions = new PlayerInputs();
		controller = GetComponent<CharacterController>();
		main = Camera.main;

		inputActions.Movement.Move.performed += ctx => horizontalMovement = ctx.ReadValue<Vector2>();
	}

	void Start()
	{
		inputActions.Combat.ShootRMB.performed += _ => Shoot();
	}

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
	void Update()
    {
		Vector3 velocity = (Vector3.right * horizontalMovement.x + Vector3.forward * horizontalMovement.y) * speed;
		controller.Move(velocity * Time.deltaTime);

		Vector2 mouseScreenPos = inputActions.Movement.Mouse.ReadValue<Vector2>();

		Ray ray = main.ScreenPointToRay(mouseScreenPos);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance))
		{
			Vector3 point = ray.GetPoint(rayDistance);
			Debug.DrawLine(ray.origin, point, Color.red);
			transform.LookAt(point);
		}
	}

	void Shoot()
	{
		Vector2 mousePos = inputActions.Movement.Mouse.ReadValue<Vector2>();
		mousePos = main.ScreenToWorldPoint(mousePos);

		if (PlayerStats.instance.hasBullets)
		{
			GameObject bull = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
			bull.transform.SetParent(holdBullets.transform);
			PlayerStats.instance.bullets--;
		}

		Debug.Log("Gun has: " + PlayerStats.instance.bullets + " Bullets");
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
