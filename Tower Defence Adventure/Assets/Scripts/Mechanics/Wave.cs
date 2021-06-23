using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    [Header("Unity Handles")]
    public GameObject goblin;

    [Header("Integers")]
    public int count;
    public int addition;
    public int worthAddition;

    [Header("Floats")]
    public float rate;
    

    public void Extrahealth()
	{
        GoblinEnemy obj = goblin.GetComponent<GoblinEnemy>();
        obj.bullet.GetComponent<TowerBullet>().BuildingDamageOutput += addition;
        obj.worth += worthAddition;
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
