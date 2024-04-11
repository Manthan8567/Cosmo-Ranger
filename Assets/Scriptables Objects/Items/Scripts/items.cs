using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items : MonoBehaviour
{
    public ItemObject item;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            // Add the item to the player's inventory
            player.inventory.AddItem(item, 1);
            // Play sound effect or perform any other action
            AudioManager.Singleton.PlaySoundEffect("CollectDiamond");
            // Destroy the item object
            Destroy(gameObject);
        }
    }
}
