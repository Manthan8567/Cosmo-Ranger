using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Diamond Object", menuName = "Inventory System/Items/Diamond")]
public class DiamondObject : ItemObject
{
    
    public class DefaultObject : ItemObject
    {
        public void Awake()
        {
            type = ItemType.Diamonds;
        }
    }
}
