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
        MedicineRepository _medicineRepository;
        public MedicineService()
        {
            _medicineRepository = new MedicineRepository();
        }
        public void AddMedicine(Medicine newMedicine)
        {
            _medicineRepository.AddMedicine(newMedicine);
        }
        public List<Medicine> GetAllMedicines()
        {
            return _medicineRepository.GetAllMedicines();
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            _medicineRepository.DeleteMedicine(medicineForDeleting);
        }
        public void SetMedicineList(List<Medicine> newMedicineList)
        {
            _medicineRepository.SetMedicineList(newMedicineList);
        }
        public void SaveInFile()
        {
            _medicineRepository.saveInFile();
        }
        public void LoadFromFile()
        {
            _medicineRepository.loadFromFile();
        }
        public Medicine FindMedicineById(int id)
        {
            return _medicineRepository.FindMedicineById(id);
        }
        public void DeleteReplacementMedicine(Medicine replacementMedicine)
        {
            _medicineRepository.DeleteReplacementMedicine(replacementMedicine);
        }
        public bool MedicineCommentExists()
        {
            return _medicineRepository.MedicineCommentExists();
        }
        public List<Medicine> GetAllMedicinesWithComment()
        {
            return _medicineRepository.GetAllMedicinesWithComment();
        }
    }
}
