using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[Header("External Sources")]
	public static GameManager instance;
	public PlayerStats playerStats;
	public TweenAnimations anim;
	PlayerInputs inputActions;

	[Header("Unity Handles")]
	public GameObject fx;
	//public GameObject shop;
	public Image healthBar;

	[Header("Blueprint Items")]
	public GameBlueprint baseItem;
	public GameBlueprint HealthItem;

	[Header("Floats")]
	public float villageHealth = 100;
	public static float wallIndex;
	public float dmg;

	[Header("Integers")]
	int min = 0, max = 100;

	[Header("Booleans")]
	public bool isGameOver = false;
	public bool shopOpen = false;
	public bool isPaused = false;

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
		if (instance != null)
		{
			Debug.LogError("More than one BuildingManager");
			return;
		}
		instance = this;

		inputActions = new PlayerInputs();

	}

	private void Start()
	{

		dmg = villageHealth / wallIndex;
	}

	private void Update()
	{
		if (isPaused)
			return;

		if (villageHealth <= 0)
		{
			isGameOver = true;
		}

		villageHealth = Mathf.Clamp(villageHealth, min, max);

		healthBar.fillAmount = villageHealth / 100;
	}

	private void FixedUpdate()
	{
		Debug.Log("Walls: " + wallIndex);
	}
	public void PauseGame()
	{
		anim.Open();
		isPaused = true;
		StartCoroutine(SetTimeZero());
	}

	public void ResumeGame()
	{
		StartCoroutine(SetTimeOne());
		anim.Close();
		isPaused = false;
	}
	public void SelectHealth(GameBlueprint health, int amount)
	{
		if (PlayerStats.Gold < health.cost)
			return;

		PlayerStats.Gold -= health.cost;
		Debug.Log(PlayerStats.Gold);
		amount++;
		HealthItem.amount += amount;
		playerStats.healthItemsAmount += amount;
	}

	public void Purchase(GameBlueprint health)
	{
		if (PlayerStats.Gold < health.cost)
			return;

		PlayerStats.Gold -= health.cost;
		Debug.Log(PlayerStats.Gold);

	}
	public void SetAmount(int amount)
	{
		Debug.Log(amount);
	}

	public void OpenShop(GameObject shop)
	{
		shop.SetActive(true);
		shopOpen = true;
	}

	public void CloseShop(GameObject shop)
	{
		shop.SetActive(false);
		shopOpen = false;
	}

	IEnumerator SetTimeZero()
	{
		yield return new WaitForSeconds(anim.tweenTime);
		//Time.timeScale = 0;
	}
	IEnumerator SetTimeOne()
	{
		yield return new WaitForSeconds(anim.tweenTime);
		//Time.timeScale = 1;
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
