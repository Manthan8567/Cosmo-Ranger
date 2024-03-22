using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable 
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Transform chatBubblePos;
    [SerializeField] private AudioClip voice; // NPC will have different voices
    [SerializeField] private string interactText;
    [SerializeField] private string chatBubbleText = "Write your own text in inspector";


    public void Interact(Transform interactorTransform) 
    {
        AudioManager.Singleton.PlaySoundEffect(voice);

        dialogueManager.StartDialogue();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ChatBubble3D.Create(this.transform, chatBubblePos.localPosition, ChatBubble3D.IconType.Happy, chatBubbleText);
        }
    }

    public string GetInteractText() 
    {
        return interactText;
    }

    public Transform GetTransform() 
    {
        return transform;
    }
}