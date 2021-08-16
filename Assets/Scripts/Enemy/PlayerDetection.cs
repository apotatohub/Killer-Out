﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
	bool isSmartToDetect;
	EnemyMovement enemyMovement;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
	public bool isDetected;
	public bool isSniper, isAsaulter;
	public Transform target;
	public float disToTarget;
	Vector3 dirToTarget;
	public float meshResolution;
	public int edgeResolveIterations;
	public float edgeDstThreshold;

	public MeshFilter viewMeshFilter;
	public MeshRenderer viewAngleMeshRenderer;
	Mesh viewMesh;


	private void Start()
    {
		isSmartToDetect = GetComponent<Enemy>().isSmartToDetect;
		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;

		enemyMovement = GetComponent<EnemyMovement>();
		if (isSniper)
		{
			viewAngle = 35;
			viewRadius = 7;
		}
		else if (isAsaulter)
		{
			viewAngle = 50;
			viewRadius = 5;
		}
	}
    private void Update()
	{
        if (!isSmartToDetect)
        {
			return;
        }
        if (!isDetected)
        {
			FindPlayer(viewRadius, viewAngle);
			viewAngleMeshRenderer.material.color = Color.green;
			
		}
        else
        {
			
			viewAngleMeshRenderer.material.color = Color.red;
		}
        if (target)
        {

			disToTarget = Vector3.Distance(target.transform.position, transform.position);
		}		
		if (disToTarget>7f)
        {
			enemyMovement.isDeafult = true;
			isDetected = false;
        }

	}


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

	public void AfterDetectionAction(Transform target, float _viewAngle)
    {
		dirToTarget = (target.position - transform.position).normalized;
		enemyMovement.target = target;	
	
		if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
		{
			if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
			{
				
				enemyMovement.isDeafult = false;
				isDetected = true;
			
			}
		}
  //      else if (disToTarget<=3)
  //      {
		//	enemyMovement.isDeafult = false;
		//	isDetected = true;
		//}


	}


	

	void LateUpdate()
	{
		DrawFieldOfView();
	}
	void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i <= stepCount; i++)
		{
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
				{
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero)
					{
						viewPoints.Add(edge.pointA);
					}
					if (edge.pointB != Vector3.zero)
					{
						viewPoints.Add(edge.pointB);
					}
				}

			}


			viewPoints.Add(newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear();

		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals();
	}


	EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++)
		{
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo(minPoint, maxPoint);
	}


	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
		{
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

	public struct EdgeInfo
	{
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
		{
			pointA = _pointA;
			pointB = _pointB;
		}
	}

}
