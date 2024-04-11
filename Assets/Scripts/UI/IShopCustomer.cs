using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughtItem(ItemObject item);
    bool TrySpendDiamondAmount(int  amount);
}
