using HospitalInformationSystem.Controller;
using Model;
using System.Windows;
using System.Windows.Controls;
using HospitalInformationSystem.Model;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using Syncfusion.Pdf;
using System.Drawing;
using Syncfusion.Pdf.Tables;
using System.Data;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : MetroWindow
    {
        Room room;
        private static ManagerMainWindow instance;

        public static ManagerMainWindow getInstance()
        {
            if (instance == null)
                instance = new ManagerMainWindow();
            return instance;
        }
        private ManagerMainWindow()
        {
            InitializeComponent();
            lightTheme.IsChecked = true;
            RefreshTables();
            if (MedicineCommentsExists())
                MedicineCommentNotificationWindow.GetInstance().ShowDialog();

        }
        private void RefreshTables()
        {
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            detailEquipmentTable.LoadAllUserControlComponents();
            medicineTableUserControl.RefreshTable();
        }
        private bool MedicineCommentsExists() => MedicineController.GetInstance().MedicineCommentExists();
        private void exitMenuItem_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        private void newRoomMenuItem_Click(object sender, RoutedEventArgs e) => OpenNewRoomWindow();
        private void editRoomMenuItem_Click(object sender, RoutedEventArgs e) => OpenEditRoomWindow();
        private void deleteRoomMenuItem_Click(object sender, RoutedEventArgs e) => DeleteRoom();
        private void renovationMenuItem_Click(object sender, RoutedEventArgs e) => OpenRoomRenovationWindow();
        private void newEquipmentMenuItem_Click(object sender, RoutedEventArgs e) => NewEquipment.getInstance().Show();
        private void editEquipmentMenuItem_Click(object sender, RoutedEventArgs e) => OpenEditEquipmentWindow();
        private void deleteEquipmentMenuItem_Click(object sender, RoutedEventArgs e) => DeleteEquipment();
        private void newMedicineMenuItem_Click(object sender, RoutedEventArgs e) => OpenNewMedicineWindow();
        private void editMedicineMenuItem_Click(object sender, RoutedEventArgs e) => OpenEditMedicineWindow();
        private void removeMedicineMenuItem_Click(object sender, RoutedEventArgs e) => DeleteMedicine();
        private void medicineComments_Click(object sender, RoutedEventArgs e) => MedicineWithCommentPreview.GetInstance().Show();
        private void mainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e) => RefreshTables();
        private void staticDynamicTab_SelectionChanged(object sender, SelectionChangedEventArgs e) => RefreshTables();
        private void reportMenuItem_Click(object sender, RoutedEventArgs e) => CreateReport();
        private void mainFrame_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) => CheckShortcut();
        private void OpenNewRoomWindow() => NewRoomWindow.getInstance().Show();
        private void OpenNewMedicineWindow() => NewMedicineWindow.GetInstance().Show();
        private void ShowRoomErrorMessage() => MessageBox.Show("Izaberite prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        private void ShowMedicineErrorMessage() => MessageBox.Show("Izaberite lek!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        private void OpenNewEquipmentWindow() => NewEquipment.getInstance().Show();
        private void ShowEquipmentErrorMessage() => MessageBox.Show("Izaberite opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        private void MenuItem_Click(object sender, RoutedEventArgs e) => this.Close();
        private void lightTheme_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Green");
            darkTheme.IsChecked = false;
        }
        private void darkTheme_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Green");
            lightTheme.IsChecked = false;
        }
        private void OpenEditRoomWindow()
        {
            room = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
            if (room != null)
            {
                if (room.RoomRenovationState.ActivityStatus)
                    RenovationMessageWindow.GetInstance().Show();
                else
                    EditRoomWindow.getInstance(room).Show();
                return;
            }
            ShowRoomErrorMessage();
        }
        private void DeleteRoom()
        {
            if (roomsUserControl.allRoomsTable.SelectedItem != null)
            {
                room = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
                if (string.Equals(room.Name, "Magacin"))
                    return;
                if (!room.RoomRenovationState.ActivityStatus)
                {
                    RoomController.GetInstance().DeleteRoom((Room)this.roomsUserControl.allRoomsTable.SelectedItem);
                    ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
                    MessageBox.Show("Izabrana prostorija je sada obrisana iz sistema.", "Brisanje prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    RenovationMessageWindow.GetInstance().Show();
                return;
            }
            ShowRoomErrorMessage();
        }
        private void OpenRoomRenovationWindow()
        {
            Room selectedRoom = (Room)roomsUserControl.allRoomsTable.SelectedItem;
            if (roomsUserControl.allRoomsTable.SelectedItem != null)
            {
                if (selectedRoom.RoomRenovationState.ActivityStatus)
                    RenovationMessageWindow.GetInstance().ShowDialog();
                else
                    RoomRenovationWindow.GetInstance(selectedRoom).ShowDialog();
                return;
            }
            ShowRoomErrorMessage();
        }
        private void OpenEditMedicineWindow()
        {
            if (medicineTableUserControl.medicineTable.SelectedItem != null)
            {
                EditMedicineWindow.GetInstance((Medicine)medicineTableUserControl.medicineTable.SelectedItem).ShowDialog();
                medicineTableUserControl.RefreshTable();
            }
            ShowMedicineErrorMessage();
        }
        private void DeleteMedicine()
        {
            if (medicineTableUserControl.medicineTable.SelectedItem != null)
            {
                MedicineController.GetInstance().DeleteMedicine((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                MedicineController.GetInstance().DeleteReplacementMedicine((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                medicineTableUserControl.RefreshTable();
                MessageBox.Show("Izabrani lek je sada obrisan iz sistema.", "Brisanje leka", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ShowMedicineErrorMessage();
        }
        private void OpenEditEquipmentWindow()
        {
            if (equipmentTable.equipmentTable.SelectedItem != null)
            {
                EditEquipment.getInstance((Equipment)this.equipmentTable.equipmentTable.SelectedItem).Show();
                return;
            }
            ShowEquipmentErrorMessage();
        }
        private void DeleteEquipment()
        {
            Equipment selectedEquipment = null;
            if (equipmentTable.equipmentTable.SelectedItem != null)
            {
                selectedEquipment = (Equipment)this.equipmentTable.equipmentTable.SelectedItem;
                EquipmentController.getInstance().deleteEquipment(selectedEquipment);
                ManagerMainWindow.getInstance().equipmentTable.refreshTable();
                MessageBox.Show("Izabrana oprema je sada obrisana iz sistema.", "Brisanje opreme", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ShowEquipmentErrorMessage();
        }
        private void CheckShortcut()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R) && Keyboard.IsKeyDown(Key.N)) OpenNewRoomWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R) && Keyboard.IsKeyDown(Key.E)) OpenEditRoomWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R) && Keyboard.IsKeyDown(Key.D)) DeleteRoom();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R) && Keyboard.IsKeyDown(Key.M)) OpenRoomRenovationWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E) && Keyboard.IsKeyDown(Key.N)) OpenNewEquipmentWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E) && Keyboard.IsKeyDown(Key.I)) OpenEditEquipmentWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E) && Keyboard.IsKeyDown(Key.D)) DeleteEquipment();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.M) && Keyboard.IsKeyDown(Key.N)) OpenNewMedicineWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.M) && Keyboard.IsKeyDown(Key.E)) OpenEditMedicineWindow();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.M) && Keyboard.IsKeyDown(Key.D)) DeleteMedicine();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.I)) CreateReport();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O)) this.Close();
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C)) Application.Current.Shutdown();
        }
        private void CreateReport()
        {
            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                table.Columns.Add("Ime");
                table.Columns.Add("Prezime");
                table.Columns.Add("Datum rođenja");
                table.Rows.Add(new string[] { "Ime", "Prezime", "Datum rođenja" });
                foreach (Doctor doctor in DoctorController.getInstance().GetDoctors())
                {
                    table.Rows.Add(new string[] { doctor.Name, doctor.Surname, doctor.DateOfBirth.ToLongDateString() });
                }
                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 0));
                doc.Save("C:\\Users\\Mirko\\Desktop\\Izvestaj.pdf");
                doc.Close(true);
            }
            this.ShowMessageAsync("", "Uspešno ste kreirali izveštaj o zauzetosti lekara");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
            MainWindow.Serialize();
        }
    }
}
