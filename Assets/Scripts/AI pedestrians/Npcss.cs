using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npcss : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    public NavMeshAgent agent;
    public Animator animator;

    private bool isWaiting;
    private int ind;

    private void Start()
    {
        StartCoroutine(SetDestination());
    }

    private void Update()
    {
        if (!isWaiting)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isWaiting = true;
                animator.SetBool("idle",true);
                StartCoroutine(SetDestination());
            }
        }
        
    }

    IEnumerator SetDestination()
    {
        yield return new WaitForSeconds(3);
        isWaiting=false;
        animator.SetBool("idle",false);
        ind=(ind+1)%wayPoints.Count;
        agent.SetDestination(wayPoints[ind].position);
    }

    


}
