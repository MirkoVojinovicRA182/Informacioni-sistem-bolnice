using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
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
