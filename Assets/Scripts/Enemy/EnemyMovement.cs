using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Enemy enemy;
    public Transform target;
    public bool isDeafult;
    public Vector3 position1, position2;
    public bool isFirstPos, isSecondPos;
    NavMeshAgent navmeshAgent;
     
    void Start()
    {
        enemy = GetComponent<Enemy>();
        navmeshAgent = GetComponent<NavMeshAgent>();
        isDeafult = isFirstPos = true;
    }

   
    void Update()
    {
      
        if (isDeafult)
        {
            navmeshAgent.stoppingDistance = 0;
            if (isFirstPos)
            {
                 navmeshAgent.SetDestination(position1);
            }
            else if(isSecondPos)
            {
                navmeshAgent.SetDestination(position2);
            }

            if (Vector3.Distance(transform.position, position1) <= 0.2f)
            {
                isSecondPos = true;
                isFirstPos = false;
            }
            else if (Vector3.Distance(transform.position, position2) <= 0.2f)
            {
                isFirstPos = true;
                isSecondPos = false;
            }
            
        }
        else
        {
            Chase(target);
        }
  

    }

    private void Chase(Transform _target)
    {
        navmeshAgent.stoppingDistance = 4;
        navmeshAgent.speed = 3.5f;
        navmeshAgent.SetDestination(target.position);
    }
}
