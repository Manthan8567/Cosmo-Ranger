using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{

    [SerializeField] private UI_shop uiShop;
    private void OnTriggerEnter(Collider collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if( shopCustomer != null)
        {
            Debug.Log("enter");
            uiShop.Show(shopCustomer);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            Debug.Log("exit");
            uiShop.Hide();
        }
    }
}
