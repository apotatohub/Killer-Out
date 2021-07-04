﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
	EnemyMovement enemyMovement;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
	[HideInInspector]
	public Transform visibleTarget;
	public bool isDetectedOnce;

	public bool isSniper, isGunman;
	float waitTime = 1;

	private void Start()
    {
		enemyMovement = GetComponent<EnemyMovement>();
		if (isSniper)
		{
			viewAngle = 35;
			viewRadius = 7;
		}
		else if (isGunman)
		{
			viewAngle = 50;
			viewRadius = 5;
		}
	}
    private void Update()
	{

		Debug.DrawRay(transform.position, transform.forward * 10, Color.white);

        if (!isDetectedOnce)
        {
		
			FindPlayer(viewRadius, viewAngle);
		}
        else
        {
			Debug.DrawRay(transform.position, useDir * disToTarget, Color.red);
			if (waitTime<=0)
            {
				Shoot();
				waitTime = 1;
			}
            else
            {
				waitTime -= Time.deltaTime;
            }
		
        }
        if (target)
        {
			useDir = (target.position - transform.position).normalized;
			disToTarget = Vector3.Distance(target.transform.position, transform.position);
		}		
		if (disToTarget>7f)
        {
			enemyMovement.isDeafult = true;
			isDetectedOnce = false;
        }

	}

	public Transform target;
	void FindPlayer(float _viewRadius, float _viewAngle)
	{
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, targetMask);
		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			if (targetsInViewRadius[i].CompareTag("Player"))
			{			
				target = targetsInViewRadius[i].transform;
				AfterDetectionAction(target, _viewAngle);	
			}
		}
	}

	public float disToTarget;
	Vector3 dirToTarget;
	Vector3 useDir;
	public void AfterDetectionAction(Transform target, float _viewAngle)
    {
		dirToTarget = (target.position - transform.position).normalized;
		enemyMovement.target = target;	
		Debug.DrawLine(transform.position, target.position, Color.green);
	
		if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
		{
			if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
			{
				
				enemyMovement.isDeafult = false;
				isDetectedOnce = true;
			
			}
		}
	}
    private void Shoot()
    {
		Debug.Log("Shoot");
    }


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
