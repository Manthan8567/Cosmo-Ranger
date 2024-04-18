using UnityEngine;


public class InventoryToggler : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
