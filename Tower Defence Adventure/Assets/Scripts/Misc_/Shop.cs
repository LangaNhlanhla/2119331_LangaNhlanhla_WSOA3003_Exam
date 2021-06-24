using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[Header("External Source")]
	GameManager gameManager;
	public GameBlueprint baseAmmo;
	public GameBlueprint baseWall;
	public GameBlueprint healthItem;
	public GameBlueprint Sword, Gun;

	private void Start()
	{
		gameManager = GameManager.instance;
	}

	public void SelectAmmo(int amountGiven)
	{
		Debug.Log("Ammo Puchased");
		gameManager.SetAmount(baseAmmo, amountGiven);
	}
	public void SelectHealth()
	{
		Debug.Log("Health Puchased");
		gameManager.SelectHealth(healthItem, healthItem.amount);
	}

	public void SelectDamage()
	{
		gameManager.SetHealth(healthItem, healthItem.amount);
	}

	public void UpgradeStation(TowerUpgrades upgrade)
	{
			gameManager.Purchase(baseWall);
			upgrade.Upgrade(baseWall);
	}

	public void UpgradeSword(Sword mySword)
	{
		if (PlayerStats.Gold < Sword.cost)
			return;

		PlayerStats.Gold -= Sword.cost;
		mySword.attDmg += Sword.amount;
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/