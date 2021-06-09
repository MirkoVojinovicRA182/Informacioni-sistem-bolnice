using HospitalInformationSystem.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void GenerateReportBtn_Click(object sender, RoutedEventArgs e)
        {
            PdfFreeRoomsReport report = new PdfFreeRoomsReport();
            report.GenerateReport((DateTime)fromDate.SelectedDate, (DateTime)toDate.SelectedDate);
        }
    }
}
