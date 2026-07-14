using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop.Item
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;

        protected ItemData ItemData;
        public ItemData GetItemData => ItemData;

        [Inject]
        public virtual void Init(ItemData data)
        {
            ItemData = data;
                
            _name.text = data.Name;
            _icon.sprite = data.Icon;
        }
    }
}