using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GoblinAttacker : MonoBehaviour
{
    [Header("External Elements")]
    Buildings[] everyBuilding;
    public Buildings buildingToAttack;
    List<DefenceWalls> defence = new List<DefenceWalls>();

    [Header("Unity Handles")]
    public NavMeshAgent agent;
    public Image healthBar;
    public GameObject holder;

    [Header("Vectors")]
    private Vector3 attackPoint;

    [Header("Floats")]
    public float goblinSpeed = 3.5f;
    public float health = 100;
    private float min = 0;
    private float max = 100;

    [Header("Booleans")]
    public bool isDead = false;
    public bool noBuildingToAttack = false;
    public bool IsThereAListener = false;


    void Start()
    {
        buildingToAttack = FindBuildingToAttack();

        healthBar.fillAmount = health / 100;
    }

    void Update()
    {
        if(health <= 0)
		{
            holder.SetActive(false);
            Destroy(gameObject, 1.1f);
		}

        health = Mathf.Clamp(health, min, max);
        healthBar.fillAmount = health / 100;

        CheckBuildings();
        NotifyGoblin();
    }

    void CheckBuildings()
	{
        if(noBuildingToAttack)
		{
            holder.SetActive(false);
            agent.speed = 0;
            Destroy(this.gameObject, Random.Range(1, 2.5f));
            Debug.Log("No Buildings");
		}
		else
		{
            if (buildingToAttack != null)
                Attack();
            else
                buildingToAttack = FindBuildingToAttack();
		}
	}  
    
    void Attack()
	{
        if(attackPoint == Vector3.zero)
		{
            agent.SetDestination(buildingToAttack.transform.position);
            agent.speed = goblinSpeed;

            RaycastHit hit;
            transform.LookAt(buildingToAttack.transform);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {
                if (hit.transform.tag == "building" &&
                   hit.transform.GetComponent<Buildings>() == buildingToAttack &&
                   hit.transform.GetComponent<Buildings>().isDestroyed == false)
                {
                    attackPoint = hit.point;
                   attackPoint.x += Random.Range(-9, 3f);
                    attackPoint.z += Random.Range(-13, 5f);
                }
                else
                {
                    attackPoint = Vector3.zero;
                }
            }
            else
                attackPoint = Vector3.zero;
		}
        else
		{
            agent.SetDestination(attackPoint);
            if(Distance(attackPoint) > 1.2f)
			{
                agent.speed = goblinSpeed;
			}
            else
                agent.speed = 0;
        }
	}

    public void GetNotified()
	{
        attackPoint = Vector3.zero;
        buildingToAttack = FindBuildingToAttack();
	}

    Buildings FindBuildingToAttack()
	{
        everyBuilding = FindObjectsOfType<Buildings>();
        List<Buildings> IntactBuildings = new List<Buildings>();
		foreach  (Buildings aBuilding in everyBuilding)
		{
            if(!aBuilding.isDestroyed)
			{
                IntactBuildings.Add(aBuilding);
			}
		}

        if(IntactBuildings.Count == 0)
		{
            noBuildingToAttack = true;
            return null;
		}
		else
		{
            float minDistance = 10000;
            int nearIndex = 0;
			for (int i = 0; i < IntactBuildings.Count; i++)
			{
                float indexDistance = Distance(IntactBuildings[i]);
                if(indexDistance < minDistance)
				{
                    minDistance = indexDistance;
                    nearIndex = i;
				}
			}

            IntactBuildings[nearIndex].NewGoblin(this);
            return IntactBuildings[nearIndex];
		}
	}

    float Distance(Buildings aBuilding)
	{
        if (aBuilding != null)
            return Vector3.Distance(transform.position, aBuilding.transform.position);
        else
		{
            attackPoint = Vector3.zero;
            return 0;
		}
	}

    float Distance(Vector3 pos)
	{
        return Vector3.Distance(transform.position, pos);
	}

    public void goblinAttacked(int damage)
	{
        if (health > 0)
            health -= damage;
        else
            health = 0;
	}

    
    void NotifyGoblin()
	{
		foreach (DefenceWalls walls in defence)
		{
            walls.GetNotified();
            if (walls.goblinToAttack == this)
                IsThereAListener = false;
            else
                IsThereAListener = true;
		}
	}

    public void NewGoblinAttacker(DefenceWalls wall)
	{
        if (defence.Contains(wall))
            return;
        else
            defence.Add(wall);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
