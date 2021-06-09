using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalInformationSystem.Service
{
    class MedicineService
    {
        IFactory _medicineRepository;
        IFindEntities _findInRepository;
        IDeleteEntity _deleteRepository;
        IAddEntity _addRepository;
        public MedicineService()
        {
            _medicineRepository = new MedicineRepository();
            _findInRepository = new MedicineRepository();
            _deleteRepository = new MedicineRepository();
            _addRepository = new MedicineRepository();
        }
        public void AddMedicine(Medicine newMedicine)
        {
            _addRepository.AddNew(newMedicine);
        }
        public ObservableCollection<Medicine> GetAllMedicines()
        {
            return MedicineRepository._medicineList;
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            _deleteRepository.PermanentlyDelete(medicineForDeleting);
        }
        /*public void SetMedicineList(List<Medicine> newMedicineList)
        {
            _medicineRepository.SetMedicineList(newMedicineList);
        }*/
        public void Serialization()
        {
            _medicineRepository.Serialization();
        }
        public ObservableCollection<object> LoadAll()
        {
            return _medicineRepository.LoadAll();
        }
        public Medicine FindMedicineById(int id)
        {
            return (Medicine)_findInRepository.FindById(id);
        }
        public void DeleteReplacementMedicine(string replacementMedicine)
        {
            foreach (Medicine medicine in GetAllMedicines())
            {
                if (medicine.ReplacementMedicine.Equals(replacementMedicine))
                {
                    medicine.ReplacementMedicine = null;
                    break;
                }
            }
        }
        public bool MedicineCommentExists()
        {
            return _findInRepository.SpecificEntityAttributeExists();
        }
        public ObservableCollection<Medicine> GetAllMedicinesWithComment()
        {
            return MedicineRepository.GetAllMedicinesWithComment();
        }
    }
}
