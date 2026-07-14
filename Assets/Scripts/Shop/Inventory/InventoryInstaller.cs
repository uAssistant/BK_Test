using Shop.Item;
using UnityEngine;
using Zenject;

namespace Shop.Inventory
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventoryItemView _inventoryItemPrefab;
        [SerializeField] private Transform _inventoryContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InventoryModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<InventoryPresenter>().AsSingle();
            
            Container.BindFactory<ItemData, InventoryItemView, ItemViewFactory<InventoryItemView>>()
                .FromComponentInNewPrefab(_inventoryItemPrefab)
                .UnderTransform(_inventoryContainer);
        }
    }
}