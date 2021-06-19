using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DefenceWalls : MonoBehaviour
{
    [Header("External Elements")]
    public GoblinAttacker goblinToAttack;
    GoblinAttacker[] goblins;

    [Header("Unity Handles")]
    public Transform goblinBody, attackPos;
    public GameObject fx;

    [Header("Floats")]
    public float attackRange = 3f;

    [Header("Booleans")]
    public bool attackController = false;


    void Update()
    {
        
            if (goblinToAttack == null)
            {
                CancelInvoke();
                goblinToAttack = FindToAttack();
            }
            else
            {
                if (!attackController)
                {
                    attackController = true;
                    StartCoroutine(InitiateAttack());
                }
            }
    }


    IEnumerator InitiateAttack()
	{
        yield return new WaitForSeconds(1f);
        attackController = false;
        Attack();
	}

    void Attack()
	{
        if(goblinToAttack == null)
		{
            goblinToAttack = FindToAttack();
		}
        else if(attackPos != null )
		{
            Vector3 PointToAttack = goblinToAttack.transform.position;
            goblinBody.LookAt(PointToAttack);
            GameObject eff = Instantiate(fx, attackPos.position, attackPos.rotation);
            Rigidbody rb = eff.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(goblinBody.forward * 1300);
            
        }
	}

    GoblinAttacker FindToAttack()
	{
        goblins = FindObjectsOfType<GoblinAttacker>();
        List<GoblinAttacker> GoblinsAlive = new List<GoblinAttacker>();
		foreach (GoblinAttacker gob in goblins)
		{
            if (!gob.isDead)
                GoblinsAlive.Add(gob);
		}

        if (GoblinsAlive.Count == 0)
            return null;
        else
		{
            float minDistance = attackRange;
            int nearIndex = -1;
			for (int i = 0; i < GoblinsAlive.Count; i++)
			{
                float DistanceIndex = Distance(GoblinsAlive[i]);
                if(DistanceIndex < minDistance)
				{
                    minDistance = DistanceIndex;
                    nearIndex = i;
				}
			}
            if (nearIndex != -1)
            {
                GoblinsAlive[nearIndex].NewGoblinAttacker(this);
                return GoblinsAlive[nearIndex];
            }
            else
                return null;
		}
	}

    float Distance(GoblinAttacker gob)
	{
        return Vector3.Distance(transform.position, gob.transform.position);
	}

    public void GetNotified()
	{
        goblinToAttack = null;
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
