using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIPedestrian : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;
    public float waitTime = 2f;

    [Header("Components")]
    private NavMeshAgent agent;
    public Animator animator;

    private int currentWaypointIndex = -1;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            MoveToNextWaypoint();
        }
    }

    void Update()
    {
        if (isWaiting) return;

        // Check if agent reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
            {
                StartCoroutine(WaitAndMove());
            }
        }
    }

    IEnumerator WaitAndMove()
    {
        isWaiting = true;

        // Switch to idle animation
        animator.SetBool("idle", true);

        yield return new WaitForSeconds(waitTime);

        MoveToNextWaypoint();

        // Switch back to walk animation
        animator.SetBool("idle", false);

        isWaiting = false;
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        int nextIndex;
        do
        {
            nextIndex = Random.Range(0, waypoints.Length);
        }
        while (nextIndex == currentWaypointIndex && waypoints.Length > 1);

        currentWaypointIndex = nextIndex;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
