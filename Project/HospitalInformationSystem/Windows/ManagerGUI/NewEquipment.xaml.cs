using HospitalInformationSystem.Controller;
using Model;
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
    /// Interaction logic for NewEquipment.xaml
    /// </summary>
    public partial class NewEquipment : Window
    {

        private static NewEquipment instance = null;

        public static NewEquipment getInstance()
        {
            if (instance == null)
                instance = new NewEquipment();
            return instance;
        }
        private NewEquipment()
        {
            InitializeComponent();
            LoadTypeOfEquipmentComboBox();
        }
        private void LoadTypeOfEquipmentComboBox()
        {
            List<String> typeList = new List<String>();
            typeList.Add("Statička");
            typeList.Add("Dinamička");
            typeOfEquipmentComboBox.ItemsSource = typeList;
        }

        private void newEquipmentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;
            string name = nameTextBox.Text;
            int quantity = int.TryParse(quanitityTextBox.Text, out quantity) ? quantity : 0;
            string description = decriptionTextBox.Text;

            TypeOfEquipment typeOfEquipment;
            if(typeOfEquipmentComboBox.SelectedIndex == 0)
                typeOfEquipment = TypeOfEquipment.Static;
            else
                typeOfEquipment = TypeOfEquipment.Dynamic;

            if (string.Compare(id, "") == 0)
                MessageBox.Show("Polje za unos šifre ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(EquipmentController.getInstance().findEquipment(id) != null)
                MessageBox.Show("U sistemu postoji oprema sa ovom šifrom!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.Compare(nameTextBox.Text, "") == 0)
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(typeOfEquipmentComboBox.SelectedItem == null)
                MessageBox.Show("Niste odabrali tip opreme!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (quantity == 0)
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                EquipmentController.getInstance().addNewEquipment(new Equipment(id, name, typeOfEquipment, quantity, description));
                ManagerMainWindow.getInstance().equipmentTable.refreshTable();
                //obavestavanje korisnika o uspesno unetoj opremi
                this.Close();
                MessageBox.Show("Uneta je nova oprema u sistem.", "Nova prostorija", MessageBoxButton.OK, MessageBoxImage.Information);
                //brisanje polja nakon uspesnog unosa
                idTextBox.Clear();
                nameTextBox.Clear();
                quanitityTextBox.Clear();
            }
        }
    }
}
