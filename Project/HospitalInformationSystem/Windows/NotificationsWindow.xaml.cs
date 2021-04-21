
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

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for NotificationsWindow.xaml
    /// </summary>
    public partial class NotificationsWindow : Window
    {
        private Model.Patient patient;
        public NotificationsWindow(Model.Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            RefreshTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Therapy therapy = (Therapy)TherapiesDataGrid.SelectedItem;
            if (therapy.NotificationEnabled)
                therapy.NotificationEnabled = false;
            else
                therapy.NotificationEnabled = true;
            TherapiesDataGrid.SelectedItem = therapy;
            RefreshTable();
        }

        public void RefreshTable()
        {
            var therapyList = new ObservableCollection<Therapy>(patient.GetTherapy());
            TherapiesDataGrid.ItemsSource = null;
            TherapiesDataGrid.ItemsSource = therapyList;
        }
    }
}
