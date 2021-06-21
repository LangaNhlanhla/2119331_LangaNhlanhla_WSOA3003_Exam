using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoblinAttackBuilding : MonoBehaviour
{
	[Header("Unity Handles")]
	public GameObject shrapnelEffect;

	[Header("Integers")]
	public int DamageOutput = 10;
	public int BuildingDamageOutput = 5;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "building")
		{
			Instantiate(shrapnelEffect, transform.position, transform.rotation);

			Debug.Log("Colliding");
			collision.transform.GetComponentInParent<Buildings>().Attacked(BuildingDamageOutput);
		}
			
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Goblin")
		{
			Instantiate(shrapnelEffect, transform.position, transform.rotation);

			//other.transform.GetComponent<GoblinAttacker>().goblinAttacked(DamageOutput);
			other.transform.GetComponent<GoblinEnemy>().goblinAttacked(DamageOutput);
			Destroy(this.gameObject);
		}
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
