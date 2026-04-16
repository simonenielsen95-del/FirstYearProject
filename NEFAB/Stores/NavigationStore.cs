using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.ViewModels;

namespace NEFAB.Stores
{
    public class NavigationStore
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
            }
        }

    }
}
