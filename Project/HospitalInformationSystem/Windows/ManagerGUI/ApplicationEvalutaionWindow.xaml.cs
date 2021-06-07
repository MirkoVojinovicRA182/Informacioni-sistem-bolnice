using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for ApplicationEvalutaionWindow.xaml
    /// </summary>
    public partial class ApplicationEvalutaionWindow : Window
    {
        private static ApplicationEvalutaionWindow _instance = null;
        public static ApplicationEvalutaionWindow GetInstance()
        {
            if (_instance == null)
                _instance = new ApplicationEvalutaionWindow();
            return _instance;
        }
        private ApplicationEvalutaionWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackController.GetInstance().AddNewFeedback(new Feedback("Upravnik", DateTime.Now, evaluationTextBox.Text));
            this.Close();
            MessageBox.Show("HVALA NA POVRATNOJ INFORMACIJI!", "", MessageBoxButton.OK);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}
