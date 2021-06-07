using System.Windows;

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
            CreateReport();
        }

        private void CreateReport()
        {

        }
    }
}
