
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
        private Patient loggedInPatient;
        public NotificationsWindow(Patient patient)
        {
            InitializeComponent();
            loggedInPatient= patient;
            LoadHourComboBox();
            LoadMinuteComboBox();
            RefreshTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Prescription prescription = (Prescription)PrescriptionsDataGrid.SelectedItem;
            Notification prescriptionNotification = new Notification(prescription.medicine.Name + " " + prescription.info, GetTimeFromComboBoxes(), prescription.startTime, prescription.endTime, true);
            prescriptionNotification.Patient = loggedInPatient;
            if(!NotificationController.GetInstance().GetNotifications().Contains(prescriptionNotification))
            {
                NotificationController.GetInstance().AddNotification(prescriptionNotification);
            }
            PatientMainWindow.GetInstance(loggedInPatient).Notify();
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
            var prescriptionList = new ObservableCollection<Prescription>(loggedInPatient.GetMedicalRecord().getPrescriptions());
            PrescriptionsDataGrid.ItemsSource = null;
            PrescriptionsDataGrid.ItemsSource = prescriptionList;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(loggedInPatient).Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(loggedInPatient).Show();
        }

        private void CustomNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            CustomNotificationsWindow window = new CustomNotificationsWindow(loggedInPatient);
            window.Show();
        }
    }
}
