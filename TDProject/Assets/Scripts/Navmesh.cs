using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Navmesh : MonoBehaviour
{
    private int wayPoints = 14;
    private int currentWaypoint = 0;
    private Transform target;

    private NavMeshAgent agent;

    public float startSpeed = 5;
  

    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       target = WayPoints.wayPoints[0];
       agent.SetDestination(target.position);
       agent.speed= startSpeed;
    }

    void Update()
    {
        
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            NextWaypoint();
        }
        agent.SetDestination(target.position);
    }

    void NextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= wayPoints)
        {
            EndPath();
        }
        else
        {
            
            target = WayPoints.wayPoints[currentWaypoint];
            
        }
        agent.speed = startSpeed;
    }

    void EndPath()
    {
        Destroy(gameObject);
        GameMaster.playerHealth--;
    }

    public void Slow(float amount)
    {
        agent.speed = startSpeed * (1 - amount);
    }

}
