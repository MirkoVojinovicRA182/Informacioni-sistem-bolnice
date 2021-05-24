using HospitalInformationSystem.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for AllergensPage.xaml
    /// </summary>
    public partial class AllergensPage : Page
    {
        public AllergensPage()
        {
            InitializeComponent();
            allergensList.ItemsSource = PatientController.getInstance().getAllergens();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new AddAllergenPage();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientController.getInstance().getAllergens().Remove((Allergen)allergensList.SelectedItem);
        }

        private void AllergensList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           allergenNameTxt.Text = ((Allergen)allergensList.SelectedItem).Name;
        }

        private void RenameBtn_Click(object sender, RoutedEventArgs e)
        {
            ((Allergen)allergensList.SelectedItem).Name = allergenNameTxt.Text;
            allergensList.Items.Refresh();
        }
    }
}
