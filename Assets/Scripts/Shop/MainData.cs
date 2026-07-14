using System.Collections.Generic;
using Shop.Item;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "MainData", menuName = "Shop/MainData")]
    public class MainData : ScriptableObject
    {
        [SerializeField] private List<ItemData> _items;
        [SerializeField] private int _defaultBalance;

        public List<ItemData> Items => _items;
        public int DefaultBalance => _defaultBalance;
        
        public bool TryGetById(string id, out ItemData itemById)
        {
            itemById = _items.Find(item => item.Id == id);
            return itemById != null;
        }
    }
}