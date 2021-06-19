using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Unity Handles")]
    public Text GoldTxt;

    [Header("Integers")]
    public static int Gold;
    public int baseGold = 200;
    public int healthItemsAmount;

    void Start()
    {
        Gold = baseGold;
    }

    void Update()
    {
        GoldTxt.text = "Gold: " + Gold;
        Gold =(int)Mathf.Clamp(Gold, 0f, Mathf.Infinity);
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
