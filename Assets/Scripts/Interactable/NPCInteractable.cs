using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable 
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private string interactText;
    [SerializeField] private Transform chatBubblePos;
    // NPC will have different voice
    [SerializeField] private AudioClip voice;

    private string chatBubbleText = "AHHHH! OUCH!";


    public void Interact(Transform interactorTransform) 
    {
        AudioManager.Singleton.PlaySoundEffect(voice);

        _dialogueManager.StartDialogue();
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