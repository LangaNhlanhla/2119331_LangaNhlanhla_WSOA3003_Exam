using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("External Sources")]
    PlayerInputs inputActions;
	CharacterController controller;
	public Sword mySword;

    [Header("Unity Handles")]
	public Transform spawnPoint;
	public Transform SwordPoint;
	public GameObject bullet, sword, holdBullets, SwordFX;
	public LayerMask GoblinLayer;
	Camera main;
	Vector2 horizontalMovement;

	[Header("Floats")]
	public float speed = 10f;
	float attTimer;


	private void Awake()
	{
        inputActions = new PlayerInputs();
		controller = GetComponent<CharacterController>();
		mySword = sword.GetComponent<Sword>();
		SwordPoint = sword.GetComponent<Transform>();
		main = Camera.main;

		inputActions.Movement.Move.performed += ctx => horizontalMovement = ctx.ReadValue<Vector2>();
		inputActions.Combat.ShootLMB.performed += _ => Melee();
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

		attTimer += Time.deltaTime;

		
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

	void Melee()
	{
		if (attTimer >= mySword.cooldown)
		{
			attTimer = 0;

			//Animation
			GameObject fx = Instantiate(SwordFX, SwordPoint.position, Quaternion.identity);
			fx.transform.SetParent(holdBullets.transform);
			Destroy(fx, 0.7f);

			Collider[] goblin = Physics.OverlapSphere(SwordPoint.position, mySword.attRange, GoblinLayer);
			foreach  (Collider gob in goblin)
			{
				gob.GetComponent<GoblinEnemy>().goblinAttacked(mySword.attDmg);
			}
			
		}
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
