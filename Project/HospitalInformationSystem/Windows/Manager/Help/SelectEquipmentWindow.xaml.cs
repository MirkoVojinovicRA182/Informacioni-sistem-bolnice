using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HospitalInformationSystem.Windows.Manager.Help
{
    /// <summary>
    /// Interaction logic for SelectEquipmentWindow.xaml
    /// </summary>
    public partial class SelectEquipmentWindow : Window
    {

        private static SelectEquipmentWindow instance = null;
        private ObservableCollection<Equipment> equipmentList;
        public static SelectEquipmentWindow getInstance()
        {
            if (instance == null)
                instance = new SelectEquipmentWindow();
            return instance;
        }
        private SelectEquipmentWindow()
        {
            InitializeComponent();

            equipmentList = new ObservableCollection<Equipment>(EquipmentController.getInstance().getEquipment());

            equipmentComboBox.ItemsSource = equipmentList;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentController.getInstance().deleteEquipment((Equipment)equipmentComboBox.SelectedItem);


            MessageBox.Show("Izabrana oprema je sada obrisana iz sistema.", "Brisanje opreme", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
