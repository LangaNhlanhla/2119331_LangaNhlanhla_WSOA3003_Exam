using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoblinAttackBuilding : MonoBehaviour
{
	[Header("Booleans")]
	public bool isconn = false;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "building")
		{
			Debug.Log("Colliding");
			collision.transform.GetComponentInParent<Buildings>().Attacked();
		}
			
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Goblin" && !isconn)
		{
			other.transform.GetComponent<GoblinAttacker>().goblinAttacked(10);
			Destroy(this.gameObject);
		}
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
