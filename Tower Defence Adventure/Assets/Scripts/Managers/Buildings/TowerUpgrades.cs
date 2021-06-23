using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
	[Header("External Sources")]
	Tower tower;

    [Header("Unity Handles")]
    public GameObject[] ranks;

	[Header("Integers")]
	[SerializeField] int currentRank;
	[SerializeField] int outputIncrease;

	private void Awake()
	{
		tower = GetComponent<Tower>();

	}
	
	public void Upgrade(GameBlueprint c)
	{
		if (PlayerStats.Gold > c.cost)
		{

			if (currentRank < ranks.Length - 1)
			{
				currentRank++;
				UpgradeLevel(currentRank);
				tower.bullet.GetComponent<TowerBullet>().DamageOutput += outputIncrease; 
			}
		}
	}

    void UpgradeLevel(int rank)
	{
		for (int i = 0; i < ranks.Length; i++)
		{
			if (i == rank)
				ranks[i].SetActive(true);
			else
				ranks[i].SetActive(false);
		}
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
