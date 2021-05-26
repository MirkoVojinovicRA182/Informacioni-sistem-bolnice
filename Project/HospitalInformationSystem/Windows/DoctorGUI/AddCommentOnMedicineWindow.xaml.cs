using HospitalInformationSystem.Model;
using System;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for AddCommentOnMedicineWindow.xaml
    /// </summary>
    public partial class AddCommentOnMedicineWindow : Window
    {
        private Medicine _medicineToAddComment;
        private static AddCommentOnMedicineWindow instance = null;

        public static AddCommentOnMedicineWindow GetInstance(Medicine medicine)
        {
            if (instance == null)
                instance = new AddCommentOnMedicineWindow(medicine);
            return instance;
        }
        private AddCommentOnMedicineWindow(Medicine medicineToAddComment)
        {
            InitializeComponent();
            this._medicineToAddComment = medicineToAddComment;
        }

        private void addCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (commentTextBox.Text.Length > 0 && _medicineToAddComment.Comment != null)
            {
                _medicineToAddComment.Comment = commentTextBox.Text;
                MessageBox.Show("Uspesno ste dodali komentar na lek.", "Komentar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Greska prilikom dodavanja komentara na lek!", "Komentar", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
