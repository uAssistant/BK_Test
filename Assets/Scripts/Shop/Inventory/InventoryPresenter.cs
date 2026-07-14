using System;
using System.Collections.Generic;
using Shop.Item;
using Zenject;

namespace Shop.Inventory
{
    public class InventoryPresenter : IInitializable, IDisposable
    {
        private readonly MainData _mainData;
        private readonly ItemViewFactory<InventoryItemView> _inventoryItemFactory;
        private readonly InventoryModel _inventoryModel;
        
        private List<InventoryItemView> _itemViews = new();
        
        public InventoryPresenter(MainData mainData, ItemViewFactory<InventoryItemView> inventoryItemFactory, InventoryModel inventoryModel)
        {
            _mainData = mainData;
            _inventoryItemFactory = inventoryItemFactory;
            _inventoryModel = inventoryModel;
        }
        
        public void Initialize()
        {
            SetInventory();
            
            _inventoryModel.ItemAdded += OnOnItemAdded;
        }

        private void OnOnItemAdded(string itemId)
        {
            CreateViewById(itemId);
        }

        public void SetInventory()
        {
            foreach (var itemId in _inventoryModel.ItemsList)
            {
                CreateViewById(itemId);
            }
        }
        
        private void CreateViewById(string itemId)
        {
            if (_mainData.TryGetById(itemId, out ItemData itemData))
            {
                var item = _inventoryItemFactory.Create(itemData);
                _itemViews.Add(item);
            }
        }

        public void Dispose()
        {
            _inventoryModel.ItemAdded -= OnOnItemAdded;
        }
    }
}