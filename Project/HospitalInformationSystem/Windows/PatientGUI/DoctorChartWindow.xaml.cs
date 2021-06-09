using HospitalInformationSystem.Model;
using Model;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for DoctorChartWindow.xaml
    /// </summary>
    public partial class DoctorChartWindow : Window
    {
        private Doctor _selectedDoctor;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public DoctorChartWindow(Doctor doctor)
        {
            InitializeComponent();
            _selectedDoctor = doctor;
            LoadBarChart();
        }
        private void LoadBarChart()
        {
            DoctorRatingPair pair1 = new DoctorRatingPair(1, 0);
            DoctorRatingPair pair2 = new DoctorRatingPair(2, 0);
            DoctorRatingPair pair3 = new DoctorRatingPair(3, 0);
            DoctorRatingPair pair4 = new DoctorRatingPair(4, 0);
            DoctorRatingPair pair5 = new DoctorRatingPair(5, 0);

            GetDoctorRatings(pair1, pair2, pair3, pair4, pair5);

            CreateChartColumns(pair1, pair2, pair3, pair4, pair5);

            Labels = new[] { "", "", "", "", "" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        private void CreateChartColumns(DoctorRatingPair pair1, DoctorRatingPair pair2, DoctorRatingPair pair3, DoctorRatingPair pair4, DoctorRatingPair pair5)
        {
            SeriesCollection = new SeriesCollection(new ColumnSeries
            {
                Title = pair1.Rating.ToString(),
                Values = new ChartValues<int> { pair1.NumberOfRatings }
            })
            {
                new ColumnSeries
                {
                    Title = pair1.Rating.ToString(),
                    Values = new ChartValues<int> { pair1.NumberOfRatings }
                },

                new ColumnSeries
                {
                    Title = pair2.Rating.ToString(),
                    Values = new ChartValues<int> { pair2.NumberOfRatings }
                },

                new ColumnSeries
                {
                    Title = pair3.Rating.ToString(),
                    Values = new ChartValues<int> { pair3.NumberOfRatings }
                },

                new ColumnSeries
                {
                    Title = pair4.Rating.ToString(),
                    Values = new ChartValues<int> { pair4.NumberOfRatings }
                },

                new ColumnSeries
                {
                    Title = pair5.Rating.ToString(),
                    Values = new ChartValues<int> { pair5.NumberOfRatings }
                }
            };
        }
        private void GetDoctorRatings(DoctorRatingPair pair1, DoctorRatingPair pair2, DoctorRatingPair pair3, DoctorRatingPair pair4, DoctorRatingPair pair5)
        {
            foreach (var rating in _selectedDoctor.Ratings)
            {
                switch (rating)
                {
                    case 1:
                        pair1.NumberOfRatings++;
                        break;
                    case 2:
                        pair2.NumberOfRatings++;
                        break;
                    case 3:
                        pair3.NumberOfRatings++;
                        break;
                    case 4:
                        pair4.NumberOfRatings++;
                        break;
                    case 5:
                        pair5.NumberOfRatings++;
                        break;
                }
            }
        }
    }
}
