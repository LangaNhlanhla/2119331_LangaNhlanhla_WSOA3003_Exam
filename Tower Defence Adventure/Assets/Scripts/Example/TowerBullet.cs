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
    // Instantiates shrapnel and destroys the bullet.
    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate(shrapnelEffect,transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
