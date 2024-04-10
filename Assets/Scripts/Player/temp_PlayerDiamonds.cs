using System;
using UnityEngine;

// This is a temporary class for Sand Planet's quest.
// Made it to avoid a conflict for Player class (bc Carlos is also working on inventory(=Player) class)
public class temp_PlayerDiamonds : MonoBehaviour
{
    public event Action OnDiamondCollected;


    public void AddDiamond()
    {
        OnDiamondCollected?.Invoke();
    }
}
