using Shop.Balance;
using Shop.Inventory;
using Shop.Item;
using Shop.Save;

namespace Shop
{
    public class ShopService
    {    
        private readonly BalanceModel _shopModel;
        private readonly InventoryModel _inventoryModel;
        private readonly JsonSaveService _saveService;

        public ShopService(BalanceModel shopModel, InventoryModel inventoryModel, JsonSaveService saveService)
        {
            _shopModel = shopModel;
            _inventoryModel = inventoryModel;
            _saveService = saveService;
        }


        public bool TryBuy(ItemData item)
        {
            if (item == null)
                return false;
            
            if (!_shopModel.TrySpend(item.Price))
                return false;

            _inventoryModel.AddItem(item);

            _saveService.Save();

            return true;
        }
    }
}