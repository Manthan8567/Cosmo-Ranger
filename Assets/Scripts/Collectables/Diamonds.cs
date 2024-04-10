using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player playerInventory = other.GetComponent<Player>();

        if (playerInventory != null)
        {
            AudioManager.Singleton.PlaySoundEffect("CollectDiamond");
            playerInventory.DiamondCollected();
            gameObject.SetActive(false);
        }



        // This is for temporary use
        temp_PlayerDiamonds playerDiamonds = other.GetComponent<temp_PlayerDiamonds>();

        if (playerDiamonds != null)
        {
            playerDiamonds.AddDiamond();
        }
    }
}
