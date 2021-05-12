using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for MergeTwoRoomsWindow.xaml
    /// </summary>
    public partial class MergeTwoRoomsWindow : Window
    {
        private static MergeTwoRoomsWindow _instance;
        public static MergeTwoRoomsWindow GetInstance()
        {
            if (_instance == null)
                _instance = new MergeTwoRoomsWindow();
            return _instance;
        }
        private MergeTwoRoomsWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}
