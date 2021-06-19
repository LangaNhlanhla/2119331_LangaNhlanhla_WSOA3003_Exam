using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[Header("Unity Handles")]
	private Transform camPos;
	public Transform FollowTarget;
	public Vector3 TargetOffset;

	[Header("Floats")]
	public float MoveSpeed = 2f;


	private void Start()
	{
		// Cache camera transform
		camPos = transform;
	}

	public void SetTarget(Transform aTransform)
	{
		FollowTarget = aTransform;
	}

	private void LateUpdate()
	{
		if (FollowTarget != null)
			camPos.position = Vector3.Lerp(camPos.position, FollowTarget.position + TargetOffset, MoveSpeed * Time.deltaTime);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
