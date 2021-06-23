using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Integers")]
    public int attDmg;

    [Header("Floats")]
    public float attRange;
    public float cooldown;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attRange);
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
