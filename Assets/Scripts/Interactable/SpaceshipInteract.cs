using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject askRideUI;
    [SerializeField] private Transform chatBubblePos;
    [SerializeField] private MrDoQuestManager questManager;

    private bool canRide = false;
    private string chatBubbleText = "Bring more diamonds!";


    private void OnEnable()
    {
        questManager.OnQuestDone += AllowToRide;
        askRideUI.SetActive(false);
    }

    private void OnDisable()
    {
        questManager.OnQuestDone -= AllowToRide;
    }

    private void AllowToRide(int notUsedHere)
    {
        canRide = true;
    }

    public void Interact(Transform interactorTransform)
    {
        // If player has completed the quest, ask to ride
        if (canRide)
        {
            askRideUI.SetActive(true);

            GameManager.Singleton.StopGame();
        }
        else
        {
            AudioManager.Singleton.PlaySoundEffect("SpaceshipTalk");

            ChatBubble3D.Create(this.transform, chatBubblePos.localPosition, ChatBubble3D.IconType.Happy, chatBubbleText);
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}
