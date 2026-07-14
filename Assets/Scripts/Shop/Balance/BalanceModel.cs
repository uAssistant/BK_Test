using System;
using Shop.Save;

namespace Shop.Balance
{
    public class BalanceModel : ISavable
    {
        public int Balance { get; private set; }

        public event Action<int> BalanceChanged; 
        
        public bool CanAfford(int price)
        {
            return Balance >= price;
        }

        public bool TrySpend(int amount)
        {
            if (!CanAfford(amount))
                return false;

            Balance -= amount;
            BalanceChanged?.Invoke(Balance);

            return true;
        }
        
        public void Load(PlayerSaveData saveData)
        {
            Balance = saveData.Balance;
            BalanceChanged?.Invoke(Balance);
        }

        public void Write(PlayerSaveData saveData)
        {
            saveData.Balance = Balance;
        }
    }
}