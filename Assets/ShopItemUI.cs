using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemPriceText;
    public Button buyButton;
    public ItemObject item;

    public void Setup(ItemObject item)
    {
        this.item = item;
        itemNameText.text = item.name;
        itemPriceText.text = item.price.ToString(); // Assuming ItemObject has a price field
        buyButton.onClick.AddListener(BuyItem);
    }
    public void BuyItem()
    {
        if (Player.Instance.TrySpendDiamondAmount(item.price))
        {
            Debug.Log("Item purchased: " + item.name);
            Player.Instance.BoughtItem(item);
            Player.Instance.inventory.AddItem(item, 1);

        }
        else
        {
            Debug.Log("Not enough funds to buy item: " + item.name);
            // Optionally, display a message to the player indicating insufficient funds
        }
    }
    
}