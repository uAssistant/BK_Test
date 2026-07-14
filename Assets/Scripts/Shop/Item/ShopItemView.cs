using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Item
{
    public class ShopItemView : ItemView
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _price;

        public event Action<ItemData> BuyClicked;
        public override void Init(ItemData data)
        {
            base.Init(data);
            
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _price.text = data.Price.ToString();
        }

        private void OnBuyButtonClicked()
        {
            BuyClicked?.Invoke(ItemData);
        }
        
        public void SetAvailable(bool isAvailable)
        {
            _buyButton.interactable = isAvailable;
        }
        
        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }
    }
}