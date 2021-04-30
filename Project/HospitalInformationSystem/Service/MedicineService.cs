using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
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
        MedicineRepository medicineRepository;
        public MedicineService()
        {
            medicineList = new List<Medicine>();
            medicineRepository = new MedicineRepository();
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
        public void SetMedicineList(List<Medicine> newMedicineList)
        {
            RemoveAllMedicines();
            foreach (Medicine newMedicine in newMedicineList)
                AddMedicine(newMedicine);
        }
        public void RemoveAllMedicines()
        {
            if (medicineList != null)
                medicineList.Clear();
        }
        public void SaveInFile()
        {
            medicineRepository.saveInFile();
        }

        public void LoadFromFile()
        {
            medicineRepository.loadFromFile();
        }
        public Medicine FindMedicineUsingId(int id)
        {
            foreach(Medicine med in medicineList)
            {
                if (med.Id == id)
                    return med;
            }
            return null;
        }
        public void FindReplacementMedicineAndDeleteThem(Medicine replacementMedicine)
        {
            foreach(Medicine medicine in medicineList)
            {
                if (Medicine.Equals(medicine.ReplacementMedicine, replacementMedicine))
                {
                    medicine.ReplacementMedicine = null;
                    break;
                }
            }
        }
    }
}
