using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
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

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for MedicineWithCommentPreview.xaml
    /// </summary>
    public partial class MedicineWithCommentPreview : Window
    {
        private static MedicineWithCommentPreview _instance;
        private ObservableCollection<Medicine> _medicinesWithComment;
        public static MedicineWithCommentPreview GetInstance()
        {
            if (_instance == null)
                _instance = new MedicineWithCommentPreview();
            return _instance;
        }
        private MedicineWithCommentPreview()
        {
            InitializeComponent();
            LoadListBox();
        }
        public void LoadListBox()
        {
            _medicinesWithComment = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicinesWithComment());
            medicineListBox.ItemsSource = null;
            medicineListBox.ItemsSource = _medicinesWithComment;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }

        private void medicineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicineCommentRevidation.GetInstance((Medicine)medicineListBox.SelectedItem).Show();
        }
    }
}
