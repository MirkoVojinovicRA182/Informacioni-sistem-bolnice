using HospitalInformationSystem.Model;
using System;
using System.Windows;
using System.Windows.Input;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.K))
            {
                if (commentTextBox.Text.Length > 0 && _medicineToAddComment.Comment != null)
                {
                    _medicineToAddComment.Comment = commentTextBox.Text;
                    MessageBox.Show("Uspesno ste dodali komentar na lek.", "Komentar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Greska prilikom dodavanja komentara na lek!", "Komentar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void commentTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
