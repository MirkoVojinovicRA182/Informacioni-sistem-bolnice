using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Windows.SecretaryGUI;
using Model;
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
using System.IO;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for MainPatientManagement.xaml
    /// </summary>
    public partial class MainPatientManagement : Window
    {
        public MainPatientManagement()
        {
            InitializeComponent();
            //string imgPath = Path.Combine(Environment.CurrentDirectory, "..", "..", "Images", "Secretary", "accounts.png");
            //Image img = new Image();
            //img.Source = new BitmapImage(new Uri(imgPath));
            //naloziBtn.Content = img;
        }
        private void naloziBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AccountsPage(this);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PatientController.getInstance().SaveInFile();
        }

        private void vestiBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AnnouncementsPage();
        }

        private void allergensBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AllergensPage();
        }
    }
}
