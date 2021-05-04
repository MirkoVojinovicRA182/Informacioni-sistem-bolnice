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
        private Medicine medicine;
        private static AddCommentOnMedicineWindow instance = null;

        public static AddCommentOnMedicineWindow GetInstance(Medicine medicine)
        {
            if (instance == null)
                instance = new AddCommentOnMedicineWindow(medicine);
            return instance;
        }
        private AddCommentOnMedicineWindow(Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
        }

        private void addCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (commentTextBox.Text.Length > 0)
            {
                medicine.Comment = commentTextBox.Text;
                MessageBox.Show("Uspesno ste dodali komentar na lek.", "Komentar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
