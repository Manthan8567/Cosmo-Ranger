using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable 
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Transform chatBubblePos;
    [SerializeField] private AudioClip voice;
    [SerializeField] private string interactText;
    [SerializeField] private string chatBubbleText = "Write your own text in inspector";
    [SerializeField] private float chatBubbleCoolTime = 10f;
    [SerializeField] private float chatBubblePopUpTime = 6f;

    private float timeSinceChatBubblePopUp;


    private void Start()
    {
        timeSinceChatBubblePopUp = chatBubbleCoolTime;
    }

    private void Update()
    {
        timeSinceChatBubblePopUp += Time.deltaTime;

        if (timeSinceChatBubblePopUp > chatBubbleCoolTime)
        {
            PopUpChatBubble();

            timeSinceChatBubblePopUp = 0;
        }
    }

    public void Interact(Transform interactorTransform) 
    {
        AudioManager.Singleton.PlaySoundEffect(voice);

        dialogueManager.StartDialogue();
    }

    private void PopUpChatBubble()
    {
        ChatBubble3D.Create(this.transform, chatBubblePos.localPosition, ChatBubble3D.IconType.Happy, chatBubbleText, chatBubblePopUpTime);
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