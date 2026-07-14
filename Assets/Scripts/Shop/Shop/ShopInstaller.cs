using Shop.Item;
using UnityEngine;
using Zenject;

namespace Shop
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private ShopItemView _shopItemPrefab;
        [SerializeField] private Transform _shopContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShopPresenter>().AsSingle();
            Container.Bind<ShopService>().AsSingle();
            
            Container.BindFactory<ItemData, ShopItemView, ItemViewFactory<ShopItemView>>()
                .FromComponentInNewPrefab(_shopItemPrefab)
                .UnderTransform(_shopContainer);
        }
    }
}