using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    class MedicineController
    {
        private MedicineService medicineService;
        private static MedicineController instance = null;
        public static MedicineController GetInstance()
        {
            if (instance == null)
                instance = new MedicineController();
            return instance;
        }
        private MedicineController()
        {
            medicineService = new MedicineService();
        }

        public void AddMedicine(Medicine newMedicine)
        {
            medicineService.AddMedicine(newMedicine);
        }
        public List<Medicine> GetAllMedicines()
        {
            return medicineService.GetAllMedicines();
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            medicineService.DeleteMedicine(medicineForDeleting);
        }
        public void ChangeMedicine(Medicine oldMedicine, Medicine newMedicine)
        {
            medicineService.ChangeMedicine(oldMedicine, newMedicine);
        }
    }
}
