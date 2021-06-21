using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GoblinEnemy : MonoBehaviour
{
    [Header("Unity Handles")]
    public Transform target;
    public Transform spawnPoint, Holder;
    public GameObject bullet;
    public NavMeshAgent agent;
    public Image healthBar;
    public GameObject holder;

    [Header("Floats")]
    public float Range = 10f;
    public float rateOfFire = 1f;
    public float health = 100;
    float min = 0;
    float max = 100;
    float currentHealth;
    float countDown = 0f;
    float speed = 5f;

    [Header("Booleans")]
    public bool isDead = false;

    [Header("Generic Elements")]
    public string enemyTag;

    void Start()
    {
        Holder = FindObjectOfType<Transform>().Find("Holder");
        currentHealth = health;
        healthBar.fillAmount = currentHealth / health;

        InvokeRepeating("NewTarget", 0.2f, 0.7f);
    }

    void Update()
    {
        if (health <= 0)
        {
            holder.SetActive(false);
            Destroy(gameObject, 1.1f);
        }

        health = Mathf.Clamp(health, min, max);
        healthBar.fillAmount = currentHealth / health;

        if (target == null)
            return;

        if (countDown <= 0)
        {
            Spawn();
            countDown = 1f / rateOfFire;
        }
        countDown -= Time.deltaTime;
    }
    void NewTarget()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag(enemyTag);
        float nearestBuilding = Mathf.Infinity;
        GameObject nearBuilding = null;

        foreach (GameObject building in buildings)
        {
            float dis = Vector3.Distance(transform.position, building.transform.position);
            if (dis < nearestBuilding)
            {
                nearestBuilding = dis;
                nearBuilding = building;
            }
        }

        if (nearBuilding != null && nearestBuilding <= Range)
        {
            target = nearBuilding.transform;
            agent.SetDestination(target.position);
        }
        else
        {
         
            target = null;
            agent.SetDestination(Vector3.zero);
        }
    }
   
    void Spawn()
    {
        GameObject obj = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        obj.transform.SetParent(Holder);

        TowerBullet bull = obj.GetComponent<TowerBullet>();

        if (bull != null)
            bull.Set(target);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
