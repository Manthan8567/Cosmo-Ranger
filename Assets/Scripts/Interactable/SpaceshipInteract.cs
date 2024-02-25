using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject askRideUI;
    [SerializeField] PlayerInventory _playerInventory;
    [SerializeField] private Transform chatBubblePos;

    private string chatBubbleText = "Bring more diamonds!";


    private void Start()
    {
        askRideUI.SetActive(false);
    }

    public void Interact(Transform interactorTransform)
    {
        // If player has completed the task, ask to ride
        // 3 is for test. Should use the real value (totalDiamonds) after level design.
        if (_playerInventory.NumberOfDiamonds >= 3)
        {
            askRideUI.SetActive(true);

            GameManager.Singleton.StopGame();
        }
        else
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
        return this.transform;
    }
}
