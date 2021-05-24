using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for NewPatientAppointmentSystemWindow.xaml
    /// </summary>
    public partial class NewPatientAppointmentSystemWindow : Window
    {

        private ObservableCollection<Appointment> appointmentList;
        private Patient patient;
        private NewPatientAppointmentWindow previousWindow;
        public NewPatientAppointmentSystemWindow(Patient patient, NewPatientAppointmentWindow window)
        {
            InitializeComponent();
            this.patient = patient;
            var list = DoctorController.getInstance().GetDoctors();
            previousWindow = window;
            DoctorComboBox.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppointment();

            MessageBox.Show("Kreiran je novi termin.", "Novi termin", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RecommendButton_Click(object sender, RoutedEventArgs e)
        {
            RecommendAppointments();
            RefreshTable();
        }

        private void CreateNewAppointment()
        {
            Appointment appointment = (Appointment)AppointmentDataGrid.SelectedItem;

            Appointment app = new Appointment(appointment.StartTime, appointment.Type, appointment.room, appointment.patient, appointment.doctor);
            app.SchedulingTime = DateTime.Now;

            AppointmentController.getInstance().AddAppointmentToAppointmentList(app);

            app.doctor.AddAppointment(app);
            app.patient.AddAppointment(app);

        }

        private List<Appointment> RecommendAppointments()
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
  
            var patients = PatientController.getInstance().getPatient();
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;
            var existingAppointments = new List<Appointment>();
            var recommendedAppointments = new List<Appointment>();



            var timesString = new List<string>();
            timesString.AddRange(new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" , "12:00", "12:30", "13:00", "13:30",
                                                    "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30" , "18:00", "18:30", "19:00", "19:30"
                                                  });

            var dateTimes = new List<DateTime>();
            var dates = new List<DateTime>();
            var datesString = new List<string>();

            DateTime startDateTime = (DateTime)startDatePicker.SelectedDate;
            var endDateTime = (DateTime)endDatePicker.SelectedDate;
            
            /*for (int i = 0; i < patients.Count; i++)
            {
                for (int j = 0; j < patients[i].appointment.Count; j++)
                {
                    existingAppointments.Add((Appointment)patients[i].appointment[j]);

                }
            }*/

            existingAppointments = AppointmentController.getInstance().GetAppointments();

            for (var date = startDateTime; date <= endDateTime; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            for (int i = 0; i < dates.Count; i++)
            {
                datesString.Add(dates[i].Date.ToString("dd.MM.yyyy"));
            }

            for (int i = 0; i < datesString.Count; i++)
            {
                for (int j = 0; j < timesString.Count; j++)
                {
                    string s = datesString[i] + "." + " " + timesString[j];
                    dateTimes.Add(DateTime.ParseExact(s, "dd.MM.yyyy. HH:mm", provider));
                }
            }

            for (int i = 0; i < dateTimes.Count; i++)
            {
                recommendedAppointments.Add(new Appointment(dateTimes[i], TypeOfAppointment.Pregled, doctor.room, patient, doctor));
            }

            for (int i = 0; i < recommendedAppointments.Count; i++)
            {
                for (int j = 0; j < existingAppointments.Count; j++)
                {
                    if (existingAppointments[j].doctor == recommendedAppointments[i].doctor & existingAppointments[j].StartTime == recommendedAppointments[i].StartTime)
                    {
                        recommendedAppointments.RemoveAt(i);
                    }
                }
            }


            if (!recommendedAppointments.Any()) {
                if ((bool)DoctorPriorityRadioButton.IsChecked)
                {
                    dates.Clear();
                    datesString.Clear();
                    timesString.Clear();
                    for (var date = startDateTime.AddDays(-3); date <= endDateTime.AddDays(3); date = date.AddDays(1))
                    {
                        dates.Add(date.Date);
                    }

                    for (int i = 0; i < dates.Count; i++)
                    {
                        datesString.Add(dates[i].Date.ToString("dd.MM.yyyy HH:mm"));
                    }

                    for (int i = 0; i < datesString.Count; i++)
                    {
                        for (int j = 0; j < timesString.Count; j++)
                        {
                            string s = datesString[i] + "." + " " + timesString[j];
                            dateTimes.Add(DateTime.ParseExact(s, "dd.MM.yyyy. HH:mm", provider));
                        }
                    }

                    for (int i = 0; i < dateTimes.Count; i++)
                    {
                        recommendedAppointments.Add(new Appointment(dateTimes[i], TypeOfAppointment.Pregled, doctor.room, patient, doctor));
                    }

                    for (int i = 0; i < recommendedAppointments.Count; i++)
                    {
                        for (int j = 0; i < existingAppointments.Count; j++)
                        {
                            if (existingAppointments[j].doctor == recommendedAppointments[i].doctor & existingAppointments[j].StartTime == recommendedAppointments[i].StartTime)
                            {
                                recommendedAppointments.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                    var doctors = DoctorController.getInstance().GetDoctors();
                    for (int i = 0; i < doctors.Count; i++)
                    {
                        if(doctors[i].GetType() == doctor.GetType()) 
                        {
                            for (int j = 0; j < dateTimes.Count; j++)
                            {
 
                                recommendedAppointments.Add(new Appointment(dateTimes[j], TypeOfAppointment.Pregled, doctors[i].room, patient, doctors[i]));
                            }
                        }
                    }

                    for (int i = 0; i < recommendedAppointments.Count; i++)
                    {
                        for (int j = 0; i < existingAppointments.Count; j++)
                        {
                            if (existingAppointments[j].doctor == recommendedAppointments[i].doctor & existingAppointments[j].StartTime == recommendedAppointments[i].StartTime)
                            {
                                recommendedAppointments.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            return recommendedAppointments;
        }

        public void RefreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(RecommendAppointments());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(patient).Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            previousWindow.Show();
        }
    }
}
