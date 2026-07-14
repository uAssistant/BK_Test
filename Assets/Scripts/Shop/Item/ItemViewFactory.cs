using Zenject;

namespace Shop.Item
{
    public class ItemViewFactory<TView> : PlaceholderFactory<ItemData, TView> 
        where TView : ItemView
    {

    }
}