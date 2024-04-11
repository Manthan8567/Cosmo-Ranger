using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject ShopObject;
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
        ShopObject.SetActive(true);

        GameManager.Singleton.StopGame();
    }

    public void ExitShop()
    {
        ShopObject.SetActive(false);
        GameManager.Singleton.ResumeGame();
    }

}
