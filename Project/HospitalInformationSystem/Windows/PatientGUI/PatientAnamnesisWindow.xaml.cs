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
    /// Interaction logic for PatientAnamnesisWindow.xaml
    /// </summary>
    public partial class PatientAnamnesisWindow : Window
    {
        private Patient loggedInPatient;
        public PatientAnamnesisWindow(Patient patient)
        {
            InitializeComponent();
            loggedInPatient = patient;
            LoadComboBox();
        }


        private void LoadComboBox()
        {
            anamnesisComboBox.ItemsSource = loggedInPatient.GetMedicalRecord().getAnamneses();
        }

        private void AnamnesisComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Anamnesis anamnesis = (Anamnesis)anamnesisComboBox.SelectedItem;
            anamnesisTextBlock.Text = anamnesis.anamnesis;
            noteTextBox.Text = anamnesis.Note;
        }

        private void AnamnesisComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Anamnesis anamnesis = (Anamnesis)anamnesisComboBox.SelectedItem;
            anamnesisTextBlock.Text = anamnesis.anamnesis;
            noteTextBox.Text = anamnesis.Note;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var anamnesis in loggedInPatient.GetMedicalRecord().getAnamneses())
            {
                if (anamnesis == (Anamnesis)anamnesisComboBox.SelectedItem)
                {
                    anamnesis.Note = noteTextBox.Text;
                }
            }
            LoadComboBox();
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
    }
}

