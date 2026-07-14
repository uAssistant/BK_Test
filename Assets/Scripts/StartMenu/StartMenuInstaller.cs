using UnityEngine;
using Zenject;

namespace StartMenu
{
    public class StartMenuInstaller : MonoInstaller
    {
        [SerializeField] private StartMenuView _startMenuView;

        public override void InstallBindings()
        {
            Container.Bind<StartMenuView>()
                .FromInstance(_startMenuView)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<StartMenuPresenter>()
                .AsSingle();
        }
    }
}