using HospitalInformationSystem.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace HospitalInformationSystem.ViewModel
{
    class TestViewModel: INotifyPropertyChanged
    {
        public TestViewModel()
        {
            OpenMessageBoxCommand = new RelayCommand(OpenMessageBoxButton);
        }
        public RelayCommand OpenMessageBoxCommand { get; private set; }

        public void OpenMessageBoxButton(object obj)
        {
            MessageBox.Show("TEST");
        }
        public static TestViewModel instance = null;
        public static TestViewModel Instance
        {
            get
            {
                lock (new object())
                {
                    if (instance == null)
                    {
                        instance = new TestViewModel();
                    }
                    return instance;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
