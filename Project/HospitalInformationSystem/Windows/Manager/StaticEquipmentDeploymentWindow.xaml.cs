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

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for StaticEquipmentDeploymentWindow.xaml
    /// </summary>
    public partial class StaticEquipmentDeploymentWindow : Window
    {
        private static StaticEquipmentDeploymentWindow instance = null;

        public static StaticEquipmentDeploymentWindow getInstance()
        {
            if (instance == null)
                instance = new StaticEquipmentDeploymentWindow();
            return instance;
        }
        private StaticEquipmentDeploymentWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
