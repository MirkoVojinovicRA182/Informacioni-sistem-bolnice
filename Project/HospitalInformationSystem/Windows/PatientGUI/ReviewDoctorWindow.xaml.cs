using HospitalInformationSystem.Model;
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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for ReviewWindow.xaml
    /// </summary>
    public partial class ReviewDoctorWindow : Window
    {
        private Appointment appointmentForReviewing;
        public ReviewDoctorWindow(Appointment appointment)
        {
            InitializeComponent();
            appointmentForReviewing = appointment;
            LoadQuestionsComboBoxes();
            LoadRatingComboBox();
        }

        private void LoadRatingComboBox()
        {
            comboBoxRating.ItemsSource = new List<int>() { 1, 2, 3, 4, 5 };
        }

        private void LoadQuestionsComboBoxes()
        {
            List<AnswersDoctorSurvey> answers = new List<AnswersDoctorSurvey>();
            answers.AddRange(new List<AnswersDoctorSurvey> { AnswersDoctorSurvey.Da, AnswersDoctorSurvey.Ne, AnswersDoctorSurvey.Nijedno });
            comboBoxQuestion1.ItemsSource = answers;
            comboBoxQuestion2.ItemsSource = answers;
            comboBoxQuestion3.ItemsSource = answers;
            comboBoxQuestion4.ItemsSource = answers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CollectAnswers();
            this.Close();
        }

        private void CollectAnswers()
        {
            DoctorReview review = new DoctorReview(GetAnswers(), (int)comboBoxRating.SelectedItem, appointmentForReviewing.doctor);
        }

        public List<AnswersDoctorSurvey> GetAnswers()
        {
            return new List<AnswersDoctorSurvey> { (AnswersDoctorSurvey)comboBoxQuestion1.SelectedItem, (AnswersDoctorSurvey)comboBoxQuestion2.SelectedItem,
                                                    (AnswersDoctorSurvey)comboBoxQuestion3.SelectedItem, (AnswersDoctorSurvey)comboBoxQuestion4.SelectedItem };
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(appointmentForReviewing.patient).Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientAppointmentCRUDOperationsWindow.getInstance(appointmentForReviewing.patient).Show();
        }
    }
}
