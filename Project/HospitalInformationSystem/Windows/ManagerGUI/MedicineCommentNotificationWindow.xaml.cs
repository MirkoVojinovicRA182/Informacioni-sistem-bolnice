using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class MedicineCommentNotificationWindow : Window
    {
        private static MedicineCommentNotificationWindow _instance;
        public static MedicineCommentNotificationWindow GetInstance()
        {
            if (_instance == null)
                _instance = new MedicineCommentNotificationWindow();
            return _instance;
        }
        private MedicineCommentNotificationWindow()
        {
            InitializeComponent();
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void lookCommentButton_Click(object sender, RoutedEventArgs e)
        {
            MedicineWithCommentPreview.GetInstance().Show();
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}
