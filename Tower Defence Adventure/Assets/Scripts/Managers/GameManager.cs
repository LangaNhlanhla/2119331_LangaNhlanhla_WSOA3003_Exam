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

	[Header("Unity Handles")]
	public GameObject fx;
	//public GameObject shop;
	public Image healthBar;

    [Header("Blueprint Items")]
    public GameBlueprint baseItem;
    public GameBlueprint HealthItem;

	[Header("Integers")]
	public float villageHealth = 100;
	int min = 0, max = 100;

	[Header("Booleans")]
	public bool isGameOver = false;
	public bool shopOpen = false;

	private void Awake()
	{
        if(instance != null)
		{
            Debug.LogError("More than one BuildingManager");
            return;
		}
        instance = this;
	}


	private void Update()
	{
		if(villageHealth <= 0)
		{
			isGameOver = true;
		}

		villageHealth = Mathf.Clamp(villageHealth, min, max);

		healthBar.fillAmount = villageHealth / 100;

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
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
