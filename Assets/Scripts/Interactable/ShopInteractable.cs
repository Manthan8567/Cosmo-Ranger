using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject ShopUI_Display;
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

        ShopUI_Display.SetActive(true);

        GameManager.Singleton.StopGame();
    }

    public void ExitShop()
    {
        ShopUI_Display.SetActive(false);
        GameManager.Singleton.ResumeGame();
    }

}
