using System;
using System.Collections.Generic;
using System.Text;

namespace Wikeasy.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private string _themeUrl;
        public string ThemeUrl
        {
            set
            {
                _themeUrl = value;
                OnPropertyChanged();
            }
            get
            {
                return _themeUrl;
            }
        }

        public AppViewModel()
        {

        }
    }
}
