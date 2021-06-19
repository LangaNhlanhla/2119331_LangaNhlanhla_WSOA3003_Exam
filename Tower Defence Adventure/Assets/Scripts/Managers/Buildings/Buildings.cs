using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Buildings : MonoBehaviour
{
    [Header("External Element")]
    public BuildingName buildingName;
    List<GoblinAttacker> goblin = new List<GoblinAttacker>();

    [Header("Unity Handles")]
    public GameObject healthHolder;
    public Transform building, replacedBuilding;
    public Image healthBar;

    [Header("Integers")]
    public int rank = 1;

    [Header("Floats")]
    public float health = 100;
    private float min = 0;
    private float max = 100;

    [Header("Booleans")]
    public bool isDestroyed = false;
    public bool hasBeenCalled = false;
    public bool isThereAListener = false;

    public enum BuildingName
	{
        Wall, Gunsmith, GeneralStore, Blacksmith, Home  
	};

    void Start()
    {
        building.gameObject.SetActive(true);
        replacedBuilding.gameObject.SetActive(false);

        healthBar.fillAmount = health / 100;
        //healthHolder.SetActive(false);
        
    }

    void Update()
    {
		if (health <= 0)
		{
            isDestroyed = true;
            if (!hasBeenCalled)
            {
                GameManager.instance.villageHealth -= 5;
                hasBeenCalled = true;
            }
		}

        healthBar.fillAmount = health / 100;
        health = Mathf.Clamp(health, min, max);

        CheckForDestruction();
    }

    void CheckForDestruction()
	{
        if(isDestroyed)
		{
            healthHolder.gameObject.SetActive(false);
            StartCoroutine(BuildingTransition());
            if (!isThereAListener)
                NotifyGoblin();
		}
	}

    void NotifyGoblin()
	{
		foreach (GoblinAttacker gob in goblin)
		{
            if (gob != null)
                gob.GetNotified();

            if (gob.buildingToAttack != this)
                isThereAListener = true;
            else
                isThereAListener = false;
		}
	}
    IEnumerator BuildingTransition()
	{
        yield return new WaitForSeconds(0.2f);
        //Instantiate(buildingParticle, transform.position, Quaternion.identity);
        building.gameObject.SetActive(false);
        replacedBuilding.gameObject.SetActive(true);
    }

    int damage()
	{
        return 6 - rank;
	}

    public void NewGoblin(GoblinAttacker gob)
	{
        if (goblin.Contains(gob))
            return;
        else
            goblin.Add(gob);
	}

    public void Attacked()
	{
        if (health > 0)
        {
            health -= damage();
        }
        else
            health = 0;
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
