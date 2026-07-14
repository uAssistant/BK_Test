using System;
using Zenject;

namespace Shop.Balance
{
    public class BalancePresenter : IInitializable, IDisposable
    {
        private readonly BalanceModel _balanceModel;
        private readonly BalanceView _balanceView;

        public BalancePresenter(BalanceView balanceView, BalanceModel balanceModel)
        {
            _balanceView = balanceView;
            _balanceModel = balanceModel;
        }
        
        public void Initialize()
        {
            _balanceView.Set(_balanceModel.Balance);

            _balanceModel.BalanceChanged += OnBalanceChanged;
        }

        private void OnBalanceChanged(int currentBalance)
        {
            _balanceView.Set(currentBalance);
        }
        
        public void Dispose()
        {
            _balanceModel.BalanceChanged -= OnBalanceChanged;
        }
    }
}