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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for EmergentSurgeryPage.xaml
    /// </summary>
    public partial class EmergentSurgeryPage : Page
    {
        private ObservableCollection<Appointment> appointmentsList = new ObservableCollection<Appointment>();
        public EmergentSurgeryPage()
        {
            InitializeComponent();
        }

        private void patientCmb_Loaded(object sender, RoutedEventArgs e)
        {
            patientCmb.ItemsSource = PatientController.getInstance().getPatient();
        }

        private void specilizationCmb_Loaded(object sender, RoutedEventArgs e)
        {
            specilizationCmb.ItemsSource = Enum.GetValues(typeof(Specialization)).Cast<Specialization>();
        }

        private void FindNearestAppointments()
        {
            DateTime currentTime = DateTime.Now;
            DateTime timeToFind;
            if (currentTime.Minute < 30)
            {
                timeToFind = currentTime.Date + new TimeSpan(currentTime.Hour, 30, 0);
            }
            else
            {
                timeToFind = currentTime.Date + new TimeSpan(currentTime.Hour + 1, 0, 0);
            }

            for (int i = 0; i < 6; i++) 
            {
                if (i > 0)
                {
                    if (timeToFind.Minute == 0)
                    {
                        timeToFind = currentTime.Date + new TimeSpan(currentTime.Hour, 30, 0);
                    }
                    else
                    {
                        timeToFind = currentTime.Date + new TimeSpan(currentTime.Hour + 1, 0, 0);
                    }
                }

                bool found = false;
                if (found == true)
                    break;
                foreach (Doctor doctor in DoctorController.getInstance().GetDoctors())
                {
                    if (doctor.Specialization != (Specialization)specilizationCmb.SelectedItem) 
                        continue;

                    bool free = true;
                    foreach (Appointment appointment in doctor.GetAppointment())
                    {
                        if (appointment.StartTime == timeToFind)
                            free = false;
                            break;
                    }

                    if (free == true)
                    {
                        Room room = FindFreeRoom(timeToFind);
                        if (room != null)
                        { 
                            appointmentsList.Add(new Appointment(timeToFind, TypeOfAppointment.Operacija, room, new Patient(), doctor));
                            found = true;
                            return;
                        }
                        
                    }
                        
                }
            }

        }

        private void AppointmentsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            appointmentsGrid.ItemsSource = (System.Collections.IEnumerable)appointmentsList;
        }

        private void SpecilizationCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appointmentsList.Clear();
            if (patientCmb.SelectedIndex != -1)
                FindNearestAppointments();
        }

        private void PatientCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appointmentsList.Clear();
            if (specilizationCmb.SelectedIndex != -1)
                FindNearestAppointments();
        }

        private Room FindFreeRoom(DateTime time)
        {
            foreach (Room room in RoomController.GetInstance().GetRooms())
            {
                if (room.Type != TypeOfRoom.OperationRoom)
                    continue;

                bool free = true;
                foreach (Doctor doctor in DoctorController.getInstance().GetDoctors())
                {
                    
                    foreach (Appointment appointment in doctor.GetAppointment())
                    {
                        if (appointment.StartTime == time)
                            free = false;
                        break;
                    }

                }

                if (free == true)
                {
                    return room;
                }
            }

            MessageBox.Show("NEMA SLOBODNIH SOBA ZA OPERACIJU!",
                                          "Obavestenje",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
            return null;
        }
    }
}
