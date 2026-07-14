using Shop.Save;
using Zenject;

namespace Shop
{
    public class ShopBootstrapper : IInitializable
    {
        private readonly ISaveService _saveService;

        public ShopBootstrapper(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            _saveService.Load();
        }
    }
}