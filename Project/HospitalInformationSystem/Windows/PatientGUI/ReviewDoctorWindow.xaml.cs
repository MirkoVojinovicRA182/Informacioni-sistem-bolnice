using HospitalInformationSystem.Controller;
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
        private Appointment _appointmentForReviewing;
        private Patient _loggedInPatient;
        public ReviewDoctorWindow(Appointment appointment, Patient patient)
        {
            InitializeComponent();
            _appointmentForReviewing = appointment;
            _loggedInPatient = patient;
            LoadQuestionsComboBoxes();
            LoadRatingComboBox();
        }
        private void LoadRatingComboBox()
        {
            comboBoxRating.ItemsSource = new List<int>() { 1, 2, 3, 4, 5 };
        }
        private void LoadQuestionsComboBoxes()
        {
            List<AnswersHospitalSurvey> answers = new List<AnswersHospitalSurvey>();
            answers.AddRange(new List<AnswersHospitalSurvey> { AnswersHospitalSurvey.Veoma_zadovoljni, AnswersHospitalSurvey.Zadovoljni, AnswersHospitalSurvey.Nezadovoljni });
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
            DoctorReview review = CreateReview();
            _appointmentForReviewing.HasBeenReviewed = true;
            _appointmentForReviewing.doctor.Ratings.Add((int)comboBoxRating.SelectedItem);
            _loggedInPatient.DoctorReviews.Add(review);
        }
        private DoctorReview CreateReview()
        {
            DoctorReview review = new DoctorReview();
            review.Answers = GetAnswers();
            review.Rating = (int)comboBoxRating.SelectedItem;
            review.Doctor = _appointmentForReviewing.doctor;
            return review;
        }
        public List<AnswersHospitalSurvey> GetAnswers()
        {
            return new List<AnswersHospitalSurvey> { (AnswersHospitalSurvey)comboBoxQuestion1.SelectedItem, (AnswersHospitalSurvey)comboBoxQuestion2.SelectedItem,
                                                    (AnswersHospitalSurvey)comboBoxQuestion3.SelectedItem, (AnswersHospitalSurvey)comboBoxQuestion4.SelectedItem };
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_appointmentForReviewing.Patient).Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientAppointmentCRUDOperationsWindow.getInstance(_appointmentForReviewing.Patient).Show();
        }
    }
}
