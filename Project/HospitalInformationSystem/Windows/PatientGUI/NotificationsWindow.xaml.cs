
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
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
    /// Interaction logic for NotificationsWindow.xaml
    /// </summary>
    public partial class NotificationsWindow : Window
    {
        private Patient _loggedInPatient;
        public NotificationsWindow(Patient patient)
        {
            InitializeComponent();
            _loggedInPatient= patient;
            LoadHourComboBox();
            LoadMinuteComboBox();
            RefreshTable();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PrescriptionIsNotSelected())
            {
                Notification prescriptionNotification = CreateNotification();
                AddCreatedNotification(prescriptionNotification);
                PatientMainWindow.GetInstance(_loggedInPatient).Notify();
            }
            else
            {
                MessageBox.Show("Niste selektovali terapiju.", "Notifikacija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool PrescriptionIsNotSelected()
        {
            return prescriptionsDataGrid.SelectedItem != null;
        }

        private Notification CreateNotification()
        {
            Prescription prescription = (Prescription)prescriptionsDataGrid.SelectedItem;
            Notification prescriptionNotification = new Notification(prescription.medicine.Name + " " + prescription.info, GetTimeFromComboBoxes(), prescription.startTime, prescription.endTime, true);
            prescriptionNotification.Patient = _loggedInPatient;
            MessageBox.Show("Kreirana je notifikacija za selektovanu terapiju.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            return prescriptionNotification;
        }
        private static void AddCreatedNotification(Notification prescriptionNotification)
        {
            if (!NotificationExists(prescriptionNotification))
            {
                NotificationController.GetInstance().AddNotification(prescriptionNotification);
            }
        }
        private static bool NotificationExists(Notification prescriptionNotification)
        {
            return NotificationController.GetInstance().GetNotifications().Contains(prescriptionNotification);
        }
        private DateTime GetTimeFromComboBoxes()
        {
            return DateTime.Parse(hourComboBox.SelectedItem + ":" + minuteComboBox.SelectedItem);
        }
        private void LoadHourComboBox()
        {
            hourComboBox.ItemsSource = Enumerable.Range(0, 24);
        }
        private void LoadMinuteComboBox()
        {
            minuteComboBox.ItemsSource = Enumerable.Range(0, 60).ToList<int>();
        }
        public void RefreshTable()
        {
            var prescriptionList = new ObservableCollection<Prescription>(_loggedInPatient.MedicalRecord.getPrescriptions());
            prescriptionsDataGrid.ItemsSource = null;
            prescriptionsDataGrid.ItemsSource = prescriptionList;
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
        private void CustomNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            CustomNotificationsWindow window = new CustomNotificationsWindow(_loggedInPatient);
            window.Show();
        }
    }
}
