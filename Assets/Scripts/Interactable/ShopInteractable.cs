using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject ShopUI_Display;
    [SerializeField] private PlayerStateMachine playerStateMachine; // Reference to the player's state machine
    [SerializeField]private Animator playerAnimator;

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

        if (playerAnimator != null)
        {
            playerAnimator.speed = 0f;
        }

    }

    public void ExitShop()
    {

        playerStateMachine.enabled = true;
        playerAnimator.speed = 1f;

        ShopUI_Display.SetActive(false);
    }

}
