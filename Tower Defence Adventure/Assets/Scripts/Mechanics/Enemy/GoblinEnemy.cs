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
    public Transform[] points;
    public GameObject bullet, fx;
    public NavMeshAgent agent;
    public Image healthBar;
    public GameObject holder;

    [Header("Floats")]
    public float Range = 10f;
    public float health = 100;
    public float damage = 10;
    public float countDown = 3f;
    float min = 0;
    float max = 100;
    float currentHealth;
    float speed = 5f;

    [Header("Integers")]
    public int worth = 50;
    int index;

    [Header("Booleans")]
    public bool isDead = false;
    
    [Header("Generic Elements")]
    public string enemyTag;

    void Start()
    {
        currentHealth = health;
        healthBar.fillAmount = currentHealth / health;
        agent.speed = speed;

        index = Random.Range(0, points.Length);
        InvokeRepeating("NewTarget", 0.2f, 0.7f);
    }

    void Update()
    {
        Death();

        health = Mathf.Clamp(health, min, max);
        healthBar.fillAmount = health / currentHealth;

        if (target == null)
            return;

        if(transform.position.y >= 5f)
		{
            GameObject eff = Instantiate(fx, transform.position, Quaternion.identity);
            Destroy(eff, 2f);

            WaveSpawner.goblinsAlive--;

            Destroy(gameObject);
        }

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
                nearBuilding.gameObject.GetComponent<Buildings>();
            }
        }

        if(nearBuilding !=null && nearBuilding.GetComponent<Buildings>().isDestroyed == true)
           nearBuilding = null;

        

        if (nearBuilding != null && nearestBuilding <= Range)
        {
            target = nearBuilding.transform;
            if (nearBuilding.GetComponent<Buildings>().isDestroyed == false)
            {
                Attack(target.position);
                StartCoroutine(Spawn());
                return;
            }
        }
        else
        {

            target = null;
            Attack(points[index].position);
        }
    }
   
    void Attack(Vector3 tar)
	{

        if (tar != null)
		{

            RaycastHit hit;
            transform.LookAt(tar);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {
                if (hit.transform.tag == "building" || hit.transform.tag == "Player")
                {
                    tar = hit.point;
                    tar.x += Random.Range(-9, 3f);
                    tar.z += Random.Range(-13, 5f);
                }
            }
        }

        agent.SetDestination(tar);
        agent.speed = speed;
    }
    IEnumerator Spawn()
    {
        GameObject obj = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            obj.transform.SetParent(Holder);

            TowerBullet bull = obj.GetComponent<TowerBullet>();

            if (bull != null && target != null)
                bull.Set(target);

        yield return new WaitForSeconds(countDown);
    }
    public void goblinAttacked(int damage)
    {
        if (health > 0)
            health -= damage;
        else
            health = 0;
    }

    void Death()
	{
        if (health <= 0)
        {
            PlayerStats.Gold += worth;


            holder.SetActive(false);
            GameObject eff = Instantiate(fx, transform.position, Quaternion.identity);
            Destroy(eff, 2f);

            WaveSpawner.goblinsAlive--;

            Destroy(gameObject);
        }
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
