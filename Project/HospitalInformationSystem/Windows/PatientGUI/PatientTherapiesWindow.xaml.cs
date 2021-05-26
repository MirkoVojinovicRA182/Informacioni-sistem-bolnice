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

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for PatientTherapiesWindow.xaml
    /// </summary>
    public partial class PatientTherapiesWindow : Window
    {
        private Patient _loggedInPatient;
        public PatientTherapiesWindow(Patient patient)
        {
            InitializeComponent();
            _loggedInPatient = patient;
            RefreshTable();
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_loggedInPatient).Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_loggedInPatient).Show();
        }
        public void RefreshTable()
        {
            var therapyList = new ObservableCollection<Therapy>(_loggedInPatient.GetTherapy());
            TherapiesDataGrid.ItemsSource = null;
            TherapiesDataGrid.ItemsSource = therapyList;
        }
    }
}
