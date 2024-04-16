using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text itemNameText;
    public TMP_Text itemPriceText;
    public ItemObject item;
    public TMP_Text itemDescriptionText;
    public LevelUpEffectManager levelUpEffectManager;

    private void Start()
    {
        // Hide the description text initially
        HideDescription();
    }
    public void BuyItem()
    {
        if (Player.Instance.TrySpendDiamondAmount(item.price))
        {
            Debug.Log("Item purchased: " + item.name);
            Player.Instance.BoughtItem(item);
            Player.Instance.inventory.AddItem(new Item(item), 1);

        }
        else
        {
            Tooltip_Warning.ShowTooltip_Static("Not enough funds to buy: " + item.name);
            // Optionally, display a message to the player indicating insufficient funds
        }
    }

    public void BuySwordItem()
    {
        if (Player.Instance.TrySpendDiamondAmount(item.price))
        {
            Debug.Log("Item purchased: " + item.name);
            Player.Instance.BoughtItem(item);
            levelUpEffectManager.PlayLevelUpEffect();
            PlayerStateMachine.Instance.UpdateAttackDamageForSwordsBought();

        }
        else
        {
            Tooltip_Warning.ShowTooltip_Static("Not enough funds to upgrade: " + item.name);
            
            // Optionally, display a message to the player indicating insufficient funds
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Display the description text when the mouse enters the button
        ShowDescription(item.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the description text when the mouse exits the button
        HideDescription();
    }

    private void ShowDescription(string description)
    {
        // Display the description text
        itemDescriptionText.text = description;
        itemDescriptionText.gameObject.SetActive(true);
    }

    private void HideDescription()
    {
        // Hide the description text
        itemDescriptionText.gameObject.SetActive(false);
    }

}