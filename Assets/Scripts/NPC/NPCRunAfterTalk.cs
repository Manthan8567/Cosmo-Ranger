using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCRunAfterTalk : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Transform destination;

    private float rangeCloseEnough = 0.5f;


    private void Start()
    {
        dialogueManager.OnFinishDialogue += RunToDestination;
    }

    private void Update()
    {
        float distanceToDestination = (destination.position - this.transform.position).magnitude;

        // If the NPC arrives at the destination, stop running animation
        if (distanceToDestination < rangeCloseEnough)
        {
            this.transform.rotation = destination.rotation;
            GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    public void RunToDestination(bool notUsedHere)
    {
        GetComponent<NavMeshAgent>().SetDestination(destination.position);
        GetComponent<Animator>().SetBool("isRunning", true);
    }
}
