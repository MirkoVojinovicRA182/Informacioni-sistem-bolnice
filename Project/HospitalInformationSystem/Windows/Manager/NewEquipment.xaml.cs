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

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for NewEquipment.xaml
    /// </summary>
    public partial class NewEquipment : Window
    {

        private static NewEquipment instance = null;
        private int selectedEquipment;

        public static NewEquipment getInstance(int selectedEquipment)
        {
            if (instance == null)
                instance = new NewEquipment(selectedEquipment);
            return instance;
        }
        private NewEquipment(int selectedEquipment)
        {
            InitializeComponent();

            this.selectedEquipment = selectedEquipment;
        }

        private void newEquipmentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;
            string name = nameTextBox.Text;
            int quanity = int.Parse(quanitityTextBox.Text);
            string description = decriptionTextBox.ToString();
            TypeOfEquipment typeOfEquipment;

            if (selectedEquipment == 1)
                typeOfEquipment = TypeOfEquipment.Static;
            else
                typeOfEquipment = TypeOfEquipment.Dynamic;

            Equipment equipment = new Equipment(id, name, typeOfEquipment, quanity, description);

            EquipmentController.getInstance().addNewEquipment(equipment);

            //obavestavanje korisnika o uspesno unetoj opremi
            MessageBox.Show("Uneta je nova oprema u sistem.", "Nova prostorija", MessageBoxButton.OK, MessageBoxImage.Information);

            //brisanje polja nakon uspesnog unosa

            idTextBox.Clear();
            nameTextBox.Clear();
            quanitityTextBox.Clear();
            
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            instance = null;
        }
    }
}
