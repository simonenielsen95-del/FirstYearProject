using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.ViewModels;

namespace NEFAB.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value; OnCurrentViewModelChanged();
            }
        }

            public void OnCurrentViewModelChanged()
            {
                CurrentViewModelChanged?.Invoke();
        }

    }
}
