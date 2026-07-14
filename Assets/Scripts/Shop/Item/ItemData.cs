using UnityEngine;

namespace Shop.Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Shop/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public int Price => _price;
        public Sprite Icon => _icon;
    }
}
