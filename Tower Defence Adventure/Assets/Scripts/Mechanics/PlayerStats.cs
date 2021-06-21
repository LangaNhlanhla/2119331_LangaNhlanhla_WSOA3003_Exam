using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Singleton")]
    public static PlayerStats instance;

    [Header("Unity Handles")]
    public Text GoldTxt;

    [Header("Integers")]
    public static int Gold;
    public int baseGold = 200;
    public int healthItemsAmount;
    public int bullets = 20;

    [Header("Booleans")]
    public bool hasBullets;

	private void Awake()
	{
        if (instance != null)
        {
            Debug.LogError("More than one Player Stats");
            return;
        }
        instance = this;
    }
    void Start()
    {
        Gold = baseGold;
    }

    void Update()
    {
        GoldTxt.text = "Gold: " + Gold;
        Gold =(int)Mathf.Clamp(Gold, 0f, Mathf.Infinity);
        
        if(bullets <= 0)
		{
            bullets = 0;
            hasBullets = false;
		}
        else if(bullets >= 1)
            hasBullets = true;

    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
