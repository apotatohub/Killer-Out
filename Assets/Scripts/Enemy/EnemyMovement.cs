using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{

    bool isSmartToMove, isSmartToDetect, isSmartToShoot;
    public Enemy enemy;
    public Transform target;
    public bool isDeafult;
    Vector3 position1;
    public Vector3 position2;
    public bool isFirstPos, isSecondPos;
    NavMeshAgent navmeshAgent;
    [SerializeField] Animator anim;
    float stealthTime;
    void Start()
    {
        stealthTime = Random.Range(3, 6);
        isSmartToMove = enemy.isSmartToMove;
        isSmartToDetect = enemy.isSmartToDetect;
        isSmartToShoot = enemy.isSmartToShoot;
        position1 = transform.position;
        enemy = GetComponent<Enemy>();
        navmeshAgent = GetComponent<NavMeshAgent>();
        isDeafult = isFirstPos = true;
    }

   
    void Update()
    {
      
        if (isDeafult)
        {

            anim.SetBool("is Chasing", false);
            //Navmesh Target Setter
            navmeshAgent.stoppingDistance = 0;
            if (isFirstPos)
            {
                 navmeshAgent.SetDestination(position1);
            }
            else if(isSecondPos)
            {
                navmeshAgent.SetDestination(position2);
            }
            //Movement Section
            if (Vector3.Distance(transform.position, position1) <= 0.2f)
            {
                Movement(isSmartToMove, true, false);
            }
            else if (Vector3.Distance(transform.position, position2) <= 0.2f)
            {
                Movement(isSmartToMove, false,true);
            }
            else if(Vector3.Distance(transform.position, position2) > 0.2f)
            {
                anim.SetBool("is Stealth", false);
            }
           

        }
        else
        {
            if (isSmartToDetect)
            {
                Chase(target);
            }
          
        }
  

    }
    public void Movement(bool _isSmartToMove, bool _isSecondPos,bool _isFirstPos)
    {
        anim.SetBool("is Stealth", true);
        if (_isSmartToMove)
        {
            if (stealthTime <= 0)
            {
                isSecondPos = _isSecondPos;
                isFirstPos = _isFirstPos;
                stealthTime = Random.Range(3, 6);
            }
            else
            {
                stealthTime -= Time.deltaTime;
            }
        }
      
    }
    private void Chase(Transform _target)
    {
        transform.LookAt(target, transform.up);
        navmeshAgent.stoppingDistance = 4;
        navmeshAgent.speed = 3f;
        navmeshAgent.SetDestination(target.position);
        if (!isSmartToShoot)
        {
            if (Vector3.Distance(transform.position, target.position) <= 4f)
            {
                anim.SetBool("is Stealth", true);
            }
            else
            {
                anim.SetBool("is Stealth", false);
            }
        }
        else
        {

            if (Vector3.Distance(transform.position, target.position) <= 4f)
            {
                anim.SetBool("stop And Shoot", true);
            }
            else
            {
                anim.SetBool("is Chasing", true);
                anim.SetBool("stop And Shoot", false);
            }
        }
       
    }
}
