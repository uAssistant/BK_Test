using TMPro;
using UnityEngine;

namespace Shop.Balance
{
    public class BalanceView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;

        public void Set(int newBalance)
        {
            _balanceText.text = newBalance.ToString();
        }
    }
}