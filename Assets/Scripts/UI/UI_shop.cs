using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform.Find("containerS");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Armor, Item.GetSprite(Item.ItemType.Armor), "Armor", Item.GetCost(Item.ItemType.Armor),0);
        CreateItemButton(Item.ItemType.Sword,Item.GetSprite(Item.ItemType.Sword), "Sword", Item.GetCost(Item.ItemType.Sword), 1);
        CreateItemButton(Item.ItemType.HealthPotion, Item.GetSprite(Item.ItemType.HealthPotion), "Potion", Item.GetCost(Item.ItemType.HealthPotion), 2);

        Hide();
    }

    private void CreateItemButton(Item.ItemType itemType ,Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 120f;
        shopItemRectTransform.anchoredPosition = new Vector2(9, -shopItemHeight * positionIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyItem(itemType);
        };
        
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        if (shopCustomer.TrySpendDiamondAmount(Item.GetCost(itemType)))
        {
            //can afford the cost
            shopCustomer.BoughtItem(itemType);
        }
        
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
