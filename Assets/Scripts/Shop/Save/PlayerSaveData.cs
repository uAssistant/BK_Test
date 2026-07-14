using System;
using System.Collections.Generic;

namespace Shop.Save
{
    [Serializable]
    public class PlayerSaveData
    {
        public int Balance;
        public List<string> PurchasedItemIds;
    }
}