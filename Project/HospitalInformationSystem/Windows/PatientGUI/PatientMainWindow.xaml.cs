using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using System.Windows.Threading;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        private Patient _loggedInPatient;
        private static PatientMainWindow _instance = null;
        private List<DispatcherTimer> _notificationTimers = new List<DispatcherTimer>();
        public PatientMainWindow(Patient patient)
        {
            InitializeComponent();
            LoadDataFromFiles();
            _loggedInPatient = patient;
            Notify();
        }
        public static PatientMainWindow GetInstance(Patient patient)
        {
            if (_instance == null)
                _instance = new PatientMainWindow(patient);
            return _instance;
        }
        private void LoadDataFromFiles()
        {
            DoctorController.getInstance().LoadFromFile();
            PatientController.getInstance().LoadFromFile();
            AppointmentController.getInstance().LoadAppointmentsFromFile();
            NotificationController.GetInstance().LoadFromFile();
        }
        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow(_loggedInPatient);
            window.Show();
            this.Hide();
        }
        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow window = new NotificationsWindow(_loggedInPatient);
            window.Show();
            this.Hide();
        }
        private void HospitalReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (HospitalReviewValidation())
            { 
                ReviewHospitalWindow window = new ReviewHospitalWindow(_loggedInPatient);
                window.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Niste imali dovoljno pregleda od prethodnog puta kada ste ocenjivali bolnicu da bi ste je ponovo ocenjivali.", "Ocenjivanje bolnice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool HospitalReviewValidation()
        {
            AppointmentController.getInstance().CalculateFinishedAppointments(_loggedInPatient);
            return _loggedInPatient.Activity.NumberOfFinishedAppointmentsSinceReview >= 5;
        }
        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AnamnesisButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAnamnesisWindow window = new PatientAnamnesisWindow(_loggedInPatient);
            this.Hide();
            window.Show();
        }
        public void Notify()
        {
            NotificationController.GetInstance().Notify(_loggedInPatient);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoctorController.getInstance().SaveInFlie();
            AppointmentController.getInstance().SaveAppointmentsInFile();
            PatientController.getInstance().SaveInFile();
            NotificationController.GetInstance().SaveInFile();
            _instance = null;
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfLightTable pdfLightTable = new PdfLightTable();
            DataTable table = new DataTable();

            pdfLightTable.Style.ShowHeader = true;

            PdfCellStyle headerStyle = new PdfCellStyle(new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.DarkBlue, PdfPens.DarkBlue);
            pdfLightTable.Style.HeaderStyle = headerStyle;

            table.Columns.Add("PONEDELJAK");
            table.Columns.Add("UTORAK");
            table.Columns.Add("SREDA");
            table.Columns.Add("ČETVRTAK");
            table.Columns.Add("PETAK");
            table.Columns.Add("SUBOTA");
            table.Columns.Add("NEDELJA");

            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);
            List<DateTime> datesCurrentWeek = new List<DateTime>();
            List<String> mondayTherapyInfo = new List<String>();
            List<String> tuesdayTherapyInfo = new List<String>();
            List<String> wednesdayTherapyInfo = new List<String>();
            List<String> thursdayTherapyInfo = new List<String>();
            List<String> fridayTherapyInfo = new List<String>();
            List<String> saturdayTherapyInfo = new List<String>();
            List<String> sundayTherapyInfo = new List<String>();

            for (var date = startOfWeek; date <= endOfWeek; date = date.AddDays(1))
            {
                datesCurrentWeek.Add(date);
            }

            foreach (var therapy in _loggedInPatient.MedicalRecord.getPrescriptions())
            {
                foreach (var currentDate in datesCurrentWeek)
                {
                    foreach (var therapyDate in GetTherapyDates(therapy))
                    {
                        if (therapyDate == currentDate)
                        {
                            switch (therapyDate.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    mondayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                                case DayOfWeek.Tuesday:
                                    tuesdayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                                case DayOfWeek.Wednesday:
                                    wednesdayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                                case DayOfWeek.Thursday:
                                    thursdayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                                case DayOfWeek.Friday:
                                    fridayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                                case DayOfWeek.Saturday:
                                    saturdayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                                case DayOfWeek.Sunday:
                                    sundayTherapyInfo.Add(therapy.medicine.Name + "-" + therapy.info);
                                    break;
                            }
                        }
                    }
                }
            }

            int[] numberOfTherapiesInDays = { mondayTherapyInfo.Count, tuesdayTherapyInfo.Count, wednesdayTherapyInfo.Count, thursdayTherapyInfo.Count,
                                              fridayTherapyInfo.Count, saturdayTherapyInfo.Count, sundayTherapyInfo.Count };

            fillTherapyInformation(mondayTherapyInfo, numberOfTherapiesInDays);
            fillTherapyInformation(tuesdayTherapyInfo, numberOfTherapiesInDays);
            fillTherapyInformation(wednesdayTherapyInfo, numberOfTherapiesInDays);
            fillTherapyInformation(thursdayTherapyInfo, numberOfTherapiesInDays);
            fillTherapyInformation(fridayTherapyInfo, numberOfTherapiesInDays);
            fillTherapyInformation(saturdayTherapyInfo, numberOfTherapiesInDays);
            fillTherapyInformation(sundayTherapyInfo, numberOfTherapiesInDays);

            for (int i = 0; i <= numberOfTherapiesInDays.Max(); i++)
            {
                table.Rows.Add(new string[] { mondayTherapyInfo[i], tuesdayTherapyInfo[i], wednesdayTherapyInfo[i], thursdayTherapyInfo[i],
                                              fridayTherapyInfo[i], saturdayTherapyInfo[i], sundayTherapyInfo[i] });
            }

            pdfLightTable.DataSource = table;
            pdfLightTable.Draw(page, new PointF(0, 0));
            doc.Save(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".pdf");
            doc.Close(true);
        }

        private List<DateTime> GetTherapyDates(Prescription prescription)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (var date = prescription.startTime; date <= prescription.endTime; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            return allDates;
        }

        private static void fillTherapyInformation(List<string> therapyInfo, int[] numberOfTherapiesInDays)
        {
            for (int i = therapyInfo.Count; i <= numberOfTherapiesInDays.Max(); i++)
            {
                therapyInfo.Add("");
            }
        }

        private void feedbackButton_Click(object sender, RoutedEventArgs e)
        {
            PatientFeedback.GetInstance(_loggedInPatient).Show();
        }
    }
}
