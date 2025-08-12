using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIPedastrians : MonoBehaviour
{
    public Transform[] waypoints; 
    public float waitTime = 3f;  
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        
    }

    void Update()
    {
        
        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                
                StartCoroutine(WaitAndMoveToNext());
            }
        }
    }

    IEnumerator WaitAndMoveToNext()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime); 

        
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        agent.SetDestination(waypoints[currentWaypointIndex].position);

        isWaiting = false;
    }
}