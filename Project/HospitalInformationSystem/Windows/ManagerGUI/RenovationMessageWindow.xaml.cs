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
    /// Interaction logic for RenovationMessageWindow.xaml
    /// </summary>
    public partial class RenovationMessageWindow : Window
    {
        private static RenovationMessageWindow instance = null;
        public static RenovationMessageWindow GetInstance()
        {
            if (instance == null)
                instance = new RenovationMessageWindow();
            return instance;
        }
        private RenovationMessageWindow()
        {
            InitializeComponent();
        }
    }
}
