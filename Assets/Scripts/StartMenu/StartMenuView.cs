using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StartMenu
{
    public class StartMenuView : MonoBehaviour
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _hockeyButton;

        public event Action ShopButtonClicked;
        public event Action HockeyButtonClicked;

        [Inject]
        public void Init()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
            _hockeyButton.onClick.AddListener(OnHockeyButtonClicked);
        }

        private void OnShopButtonClicked()
        {
            ShopButtonClicked?.Invoke();
        }

        private void OnHockeyButtonClicked()
        {
            HockeyButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
            _hockeyButton.onClick.RemoveListener(OnHockeyButtonClicked);
        }
    }
}
