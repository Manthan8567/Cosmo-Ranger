using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;

    private bool isInteractable = false;


    private void OnEnable()
    {
        dialogueManager.OnFinishDialogue += SetInteractable;

        this.GetComponent<BoxCollider>().isTrigger = false;
    }

    private void OnDisable()
    {
        dialogueManager.OnFinishDialogue -= SetInteractable;
    }

    public void SetInteractable(bool notUsedHere)
    {
        isInteractable = true;

        this.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInteractable)
        {
            if (other.CompareTag("Player") || other.CompareTag("NPC") || other.CompareTag("Enemy"))
            {
                GetComponent<Animator>().SetBool("character_nearby", true);
            }
        }
    }
}
