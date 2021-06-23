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
    public Text AmmoTxt, SwordTxt, DamageTxt;

    [Header("Integers")]
    public int baseGold = 200;
    public int healthItemsAmount, DamageItemsAmount, Sword = 1;
    public int bullets = 20;
    public static int Gold;

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
        UpdateTexts();
        Amounts();

    }

    void UpdateTexts()
	{
        GoldTxt.text = Gold.ToString();
        Gold = (int)Mathf.Clamp(Gold, 0f, Mathf.Infinity);

        AmmoTxt.text = bullets.ToString();
        SwordTxt.text = Sword.ToString();
        DamageTxt.text = DamageItemsAmount.ToString();

    }
    void Amounts()
	{
        if (bullets <= 0)
        {
            bullets = 0;
            hasBullets = false;
        }
        else if (bullets >= 1)
            hasBullets = true;

        if (healthItemsAmount <= 0)
        {
            healthItemsAmount = 0;
        }

        if (healthItemsAmount <= 0)
        {
            healthItemsAmount = 0;
        }

        if (DamageItemsAmount <= 0)
        {
            DamageItemsAmount = 0;
        }
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
