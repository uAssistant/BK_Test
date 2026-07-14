using Zenject;

namespace Shop.Save
{
    public class SaveInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<JsonSaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShopBootstrapper>().AsSingle();
        }
    }
}