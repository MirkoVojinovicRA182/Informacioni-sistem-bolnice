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
    /// Interaction logic for ReviewHospitalWindow.xaml
    /// </summary>  
    public partial class ReviewHospitalWindow : Window
    {
        private Patient _loggedInPatient;
        public ReviewHospitalWindow(Patient patient)
        {
            InitializeComponent();
            LoadQuestionsComboBoxes();
            LoadRatingComboBox();
            this._loggedInPatient = patient;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResetNumberOfFinishedAppointments();
            _loggedInPatient.Activity.HospitalReviewTime = DateTime.Now;
            this.Close();
            PatientMainWindow.GetInstance(_loggedInPatient).Show();
        }
        private void CollectAnswers()
        {
            HospitalReview review = CreateReview();
            _loggedInPatient.HospitalReviews.Add(review);
        }
        private HospitalReview CreateReview()
        {
            HospitalReview review = new HospitalReview();
            review.Answers = GetAnswers();
            review.Rating = (int)comboBoxRating.SelectedItem;
            return review;
        }
        private void LoadQuestionsComboBoxes()
        {
            List<AnswersHospitalSurvey> answers = new List<AnswersHospitalSurvey>();
            answers.AddRange(new List<AnswersHospitalSurvey> { AnswersHospitalSurvey.Veoma_zadovoljni, AnswersHospitalSurvey.Zadovoljni, AnswersHospitalSurvey.Nezadovoljni });
            comboBoxQuestion1.ItemsSource = answers;
            comboBoxQuestion2.ItemsSource = answers;
            comboBoxQuestion3.ItemsSource = answers;
            comboBoxQuestion4.ItemsSource = answers;
            comboBoxQuestion5.ItemsSource = answers;
            comboBoxQuestion6.ItemsSource = answers;
            comboBoxQuestion7.ItemsSource = answers;
        }
        private void LoadRatingComboBox()
        {
            comboBoxRating.ItemsSource = new List<int>() { 1, 2, 3, 4, 5 };
        }
        public List<AnswersHospitalSurvey> GetAnswers()
        {
            return new List<AnswersHospitalSurvey> { (AnswersHospitalSurvey)comboBoxQuestion1.SelectedItem, (AnswersHospitalSurvey)comboBoxQuestion2.SelectedItem,
                                                    (AnswersHospitalSurvey)comboBoxQuestion3.SelectedItem, (AnswersHospitalSurvey)comboBoxQuestion4.SelectedItem,
                                                    (AnswersHospitalSurvey)comboBoxQuestion5.SelectedItem, (AnswersHospitalSurvey)comboBoxQuestion6.SelectedItem,
                                                    (AnswersHospitalSurvey)comboBoxQuestion3.SelectedItem };
        }
        private void ResetNumberOfFinishedAppointments()
        {
            _loggedInPatient.Activity.NumberOfFinishedAppointmentsSinceReview = 0;
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_loggedInPatient).Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_loggedInPatient).Show();
        }
    }
}
