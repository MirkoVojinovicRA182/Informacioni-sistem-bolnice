﻿using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
using System.Collections.Generic;
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
            foreach (Medicine medicine in GetAllMedicines())
            {
                if (medicine.ReplacementMedicine!= null && medicine.ReplacementMedicine.Equals(replacementMedicine))
                {
                    medicine.ReplacementMedicine = null;
                    break;
                }
            }
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
