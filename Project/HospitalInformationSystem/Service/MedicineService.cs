using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    class MedicineService
    {
        List<Medicine> medicineList;
        public MedicineService()
        {
            medicineList = new List<Medicine>();
        }

        public void AddMedicine(Medicine newMedicine)
        {
            medicineList.Add(newMedicine);
        }
        public List<Medicine> GetAllMedicines()
        {
            return medicineList;
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            if (medicineForDeleting == null)
                return;
            if (medicineList != null)
                if (this.medicineList.Contains(medicineForDeleting))
                    this.medicineList.Remove(medicineForDeleting);
        }
        public void ChangeMedicine(Medicine oldMedicine, Medicine newMedicine)
        {
            oldMedicine.Id = newMedicine.Id;
            oldMedicine.Name = newMedicine.Name;
            oldMedicine.Type = newMedicine.Type;
            oldMedicine.Purpose = newMedicine.Purpose;
            oldMedicine.WayOfUse = newMedicine.WayOfUse;
            oldMedicine.ReplacementMedicine = newMedicine.ReplacementMedicine;
            oldMedicine.Ingredients = newMedicine.Ingredients;
        }
    }
}
