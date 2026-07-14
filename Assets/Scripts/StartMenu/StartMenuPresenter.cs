using System;
using Zenject;

namespace StartMenu
{
    public class StartMenuPresenter : IInitializable, IDisposable
    {
        private readonly StartMenuView _startMenuView;
        private readonly SceneLoader _sceneLoader;

        public StartMenuPresenter(StartMenuView startMenuView, SceneLoader sceneLoader)
        {
            _startMenuView = startMenuView;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _startMenuView.ShopButtonClicked += OnShopButtonClicked;
            _startMenuView.HockeyButtonClicked += OnHockeyButtonClicked;
        }

        private void OnShopButtonClicked()
        {
            _sceneLoader.LoadScene(SceneNames.ShopScene);
        }

        private void OnHockeyButtonClicked()
        {
            _sceneLoader.LoadScene(SceneNames.HockeyScene);
        }
        
        public void Dispose()
        {
            _startMenuView.ShopButtonClicked -= OnShopButtonClicked;
            _startMenuView.HockeyButtonClicked -= OnHockeyButtonClicked;
        }
    }
}