using HospitalInformationSystem.Model;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Documents;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class MedicineCommentRevidation : MetroWindow
    {
        private Medicine _commentedMedicine;
        private static MedicineCommentRevidation _instance;
        public static MedicineCommentRevidation GetInstance(Medicine commentedMedicine)
        {
            if (_instance == null)
                _instance = new MedicineCommentRevidation(commentedMedicine);
            return _instance;
        }
        private MedicineCommentRevidation(Medicine commentedMedicine)
        {
            InitializeComponent();
            _commentedMedicine = commentedMedicine;
            commentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(_commentedMedicine.Comment)));
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
        private void acceptCommentButton_Click(object sender, RoutedEventArgs e)
        {
            EditMedicineWindow.GetInstance(_commentedMedicine).ShowDialog();
            SetCommentToNullAndCloseWindow();
        }
        private void declineCommentButton_Click(object sender, RoutedEventArgs e)
        {
            _commentedMedicine.Comment = null;
            SetCommentToNullAndCloseWindow();
        }
        private void SetCommentToNullAndCloseWindow()
        {
            _commentedMedicine.Comment = null;
            MedicineWithCommentPreview.GetInstance().LoadListBox();
            this.Close();
        }
    }
}
