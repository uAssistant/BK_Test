using System;
using System.Collections.Generic;
using Shop.Balance;
using Shop.Item;
using Zenject;

namespace Shop
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private MainData _mainData;
        private ItemViewFactory<ShopItemView> _factory;
        private ShopService _shopService;
        private BalanceModel _balanceModel; 
        private readonly List<ShopItemView> _itemViews = new();

        [Inject]
        public void Init(MainData mainData, ItemViewFactory<ShopItemView> factory, ShopService shopService, BalanceModel balanceModel)
        {
            _mainData = mainData;
            _factory = factory;
            _shopService = shopService;
            _balanceModel = balanceModel;
        }

        public void Initialize()
        {
            SetShop();
            
            _balanceModel.BalanceChanged += OnBalanceChanged;
        }

        private void SetShop()
        {
            foreach (var itemData in _mainData.Items)
            {
                var item = _factory.Create(itemData);
                item.BuyClicked += OnBuyClicked;
                _itemViews.Add(item);
            }

            UpdateButtons();
        }

        private void OnBuyClicked(ItemData itemData)
        {
            _shopService.TryBuy(itemData);
        }
        
        private void OnBalanceChanged(int balance)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            foreach (var item in _itemViews)
            {
                var canBuy = _balanceModel.CanAfford(item.GetItemData.Price);
                item.SetAvailable(canBuy);
            }
        }

        public void Dispose()
        {
            foreach (var item in _itemViews)
            {
                item.BuyClicked -= OnBuyClicked;
            }
            
            _balanceModel.BalanceChanged -= OnBalanceChanged;
        }
    }
}