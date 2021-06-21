using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    [Header("Unity Handles")]
    public Transform target;
    public GameObject shrapnelEffect;
    Rigidbody rb;


    [Header("Floats")]
    public float bulletSpeed = 8f;
    public float bulletDuration;

    [Header("Integers")]
    public int DamageOutput = 10;
    public int BuildingDamageOutput = 5;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, bulletDuration);
    }

    void Update()
    {
        if(target == null)
		{
            Destroy(gameObject);
            return;
		}

        Vector3 dir = target.position - transform.position;
        float dis = bulletSpeed * Time.deltaTime;

        if(dir.magnitude <= dis)
		{
            InitiateTarget();
            return;
		}

        transform.Translate(dir.normalized * dis, Space.World);
       
    }

    public void Set(Transform tar)
	{
        target = tar;
	}

    void InitiateTarget()
	{
        Debug.Log("Hit");
        //Instantiate(shrapnelEffect,transform.position, transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(shrapnelEffect, transform.position, transform.rotation);

        if (other.transform.tag == "Goblin")
        {
            other.transform.GetComponent<GoblinEnemy>().goblinAttacked(DamageOutput);
            Destroy(this.gameObject);
        }
    }
    // Instantiates shrapnel and destroys the bullet.
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(shrapnelEffect,transform.position, transform.rotation);

        if (collision.transform.tag == "building")
        {
            Debug.Log("Colliding");
            collision.transform.GetComponentInParent<Buildings>().Attacked(BuildingDamageOutput);
        }
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
