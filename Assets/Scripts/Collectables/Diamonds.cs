using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Singleton.PlaySoundEffect("CollectDiamond");

            Player playerInventory = other.GetComponent<Player>();

            if (playerInventory != null)
            {
                playerInventory.DiamondCollected();
                gameObject.SetActive(false);
            }
        }
    }
}
