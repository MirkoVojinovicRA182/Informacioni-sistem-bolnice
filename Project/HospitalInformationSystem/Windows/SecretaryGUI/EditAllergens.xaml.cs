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

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for EditAllergens.xaml
    /// </summary>
    public partial class EditAllergens : Window
    {
        Patient patient = null;
        public EditAllergens(Patient p)
        {
            InitializeComponent();
            patient = p;
            allergensDataGrid.ItemsSource = p.GetMedicalRecord().AllergensList;
            
        }

        private void allergensDataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }
    }
}
