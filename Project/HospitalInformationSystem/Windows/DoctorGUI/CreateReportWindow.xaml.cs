using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Tables;
using System;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for CreateReportWindow.xaml
    /// </summary>
    public partial class CreateReportWindow : Window
    {
        private static CreateReportWindow instance;
        public static CreateReportWindow GetInstance()
        {
            if (instance == null)
                instance = new CreateReportWindow();
            return instance;
        }
        public CreateReportWindow()
        {
            InitializeComponent();
        }
        private void CreateReport()
        {
            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                table.Columns.Add("Lek");
                table.Columns.Add("Utrošena količina");
                table.Rows.Add(new string[] { "Lek", "Utrosena kolicina" });
                DateTime startDate = DateTime.ParseExact(startDateTextBox.Text,
                    "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(endDateTextBox.Text,
                    "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                foreach (Medicine medicine in MedicineController.GetInstance().GetAllMedicines())
                {
                    int number = 0;
                    foreach (Patient patient in PatientController.getInstance().getPatient())
                    {
                        foreach (Prescription prescription in patient.MedicalRecord.getPrescriptions())
                        {
                            if (prescription.medicine.Name.Equals(medicine.Name) && prescription.startTime > startDate && prescription.startTime < endDate)
                                number++;
                        }
                    }
                    table.Rows.Add(new string[] { medicine.Name, number.ToString() });
                }
                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 0));
                doc.Save("Lekovi.pdf");
                doc.Close(true);
            }
            MessageBox.Show("Uspesno kreiran izvestaj o potrosnji lekova");
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.I))
                CreateReport();
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
