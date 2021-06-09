using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalInformationSystem.Controller
{
    class MedicineController
    {
        private MedicineService _medicineService;
        private static MedicineController instance = null;
        public static MedicineController GetInstance()
        {
            if (instance == null)
                instance = new MedicineController();
            return instance;
        }
        private MedicineController()
        {
            _medicineService = new MedicineService();
        }

        public void AddMedicine(Medicine newMedicine)
        {
            _medicineService.AddMedicine(newMedicine);
            AllergenController.Instance.AddAllergen(new Allergen(newMedicine.Name));
            foreach(MedicineIngredient me in newMedicine.Ingredients)
                AllergenController.Instance.AddAllergen(new Allergen(me.Name));
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            _medicineService.DeleteMedicine(medicineForDeleting);
        }
        public ObservableCollection<Medicine> GetAllMedicines()
        {
            return _medicineService.GetAllMedicines();
        }
        /*public void SetMedicineList(List<Medicine> newMedicineList)
        {
            medicineService.SetMedicineList(newMedicineList);
        }*/
        public void Serialization()
        {
            _medicineService.Serialization();
        }
        public ObservableCollection<object> LoadAll()
        {
            return _medicineService.LoadAll();
        }
        public Medicine FindMedicineById(int id)
        {
            return _medicineService.FindMedicineById(id);
        }
        public void DeleteReplacementMedicine(string replacementMedicine)
        {
            _medicineService.DeleteReplacementMedicine(replacementMedicine);
        }
        public bool MedicineCommentExists()
        {
            return _medicineService.MedicineCommentExists();
        }
        public ObservableCollection<Medicine> GetAllMedicinesWithComment()
        {
            return _medicineService.GetAllMedicinesWithComment();
        }
    }
}
