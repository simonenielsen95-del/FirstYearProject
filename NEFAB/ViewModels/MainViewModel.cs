using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Stores;

namespace NEFAB.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore NavigationStore;

        public BaseViewModel CurrentViewModel 
        {
            get
            {
                return NavigationStore.CurrentViewModel;
            }
        }

        public MainViewModel(NavigationStore navigationStore)
        {
            NavigationStore = navigationStore;
        }

    }
}
