using System;
using System.Collections.Generic;
using Shop.Item;
using Shop.Save;

namespace Shop.Inventory
{
    public class InventoryModel : ISavable
    {
        public List<string> ItemsList { get; private set; }
        
        public event Action<string> ItemAdded;
        
        public void AddItem(ItemData item)
        {
            ItemsList.Add(item.Id);
            ItemAdded?.Invoke(item.Id);
        }
        
        public void Load(PlayerSaveData saveData)
        {
            ItemsList = saveData.PurchasedItemIds;
        }

        public void Write(PlayerSaveData saveData)
        {
            saveData.PurchasedItemIds = ItemsList;
        }
    }
}