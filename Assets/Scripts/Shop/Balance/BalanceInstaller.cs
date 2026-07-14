using UnityEngine;
using Zenject;

namespace Shop.Balance
{
    public class BalanceInstaller : MonoInstaller
    {
        [SerializeField] private BalanceView _balanceView;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BalancePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BalanceModel>().AsSingle();
            Container.Bind<BalanceView>().FromInstance(_balanceView).AsSingle();
        }
    }
}