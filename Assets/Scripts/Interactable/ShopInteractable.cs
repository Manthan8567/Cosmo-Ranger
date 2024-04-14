using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject ShopUI_Display;
    [SerializeField] private PlayerStateMachine playerStateMachine; // Reference to the player's state machine

    private void Start()
    {
        // Ensure that the reference to the PlayerStateMachine is assigned
        if (playerStateMachine == null)
        {
            Debug.LogError("PlayerStateMachine reference is not set in ShopInteractable.");
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

    public void Interact(Transform interactorTransform)
    {

        playerStateMachine.enabled = false;


        ShopUI_Display.SetActive(true);

    }

    public void ExitShop()
    {

        playerStateMachine.enabled = true;

        ShopUI_Display.SetActive(false);
    }

}
