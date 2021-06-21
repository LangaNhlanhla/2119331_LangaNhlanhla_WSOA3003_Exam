using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Unity Handles")]
    public Transform spawnPoint;
    public Transform target, holder;
    public GameObject bullet;

    [Header("Floats")]
    public float Range = 10f;
    public float rateOfFire = 1f;
    public float Health = 100f, currenthealth;
    float countDown = 0f;
    float speed = 5f;

    [Header("Generic ELements")]
    public string goblinTag;

    void Start()
    {
        InvokeRepeating("NewTarget", 0.2f, 0.7f);
        currenthealth = Health;
    }

    void Update()
    {
        if (target == null)
            return;

        RotateTower();

        if(countDown <= 0)
		{
            Spawn();
            countDown = 1f / rateOfFire;
        }
        countDown -= Time.deltaTime;
    }

    void NewTarget()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(goblinTag);
        float nearestGoblin = Mathf.Infinity;
        GameObject nearGob = null;

		foreach (GameObject goblin in enemies)
		{
            float dis = Vector3.Distance(transform.position, goblin.transform.position);
            if(dis < nearestGoblin)
			{
                nearestGoblin = dis;
                nearGob = goblin;
			}
		}

        if (nearGob != null && nearestGoblin <= Range)
        {
            target = nearGob.transform;
        }
        else
            target = null;
	}

    void RotateTower()
	{
        Vector3 dir = target.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);

        Vector3 actualRot = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, actualRot.y, 0);
	}

    void Spawn()
	{
        GameObject obj = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        obj.transform.SetParent(holder);

        TowerBullet bull = obj.GetComponent<TowerBullet>();

        if (bull != null)
            bull.Set(target);
    }

    
	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
	}

}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
