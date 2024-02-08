using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds {  get; private set; }

    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        Debug.Log("Diamonds:" +  NumberOfDiamonds);
    }
}
