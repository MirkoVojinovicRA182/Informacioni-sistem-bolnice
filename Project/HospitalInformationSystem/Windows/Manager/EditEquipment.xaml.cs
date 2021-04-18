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

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {

        private static EditEquipment instance = null;

        public static EditEquipment getInstance(Equipment equipment)
        {
            if (instance == null)
                instance = new EditEquipment(equipment);
            return instance;
        }
        private EditEquipment(Equipment equipment)
        {
            InitializeComponent();
        }
    }
}
