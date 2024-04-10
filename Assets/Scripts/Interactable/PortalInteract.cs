using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject askRideUI;


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
        askRideUI.SetActive(true);

        GameManager.Singleton.StopGame();
    }
}
