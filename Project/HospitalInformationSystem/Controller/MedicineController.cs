using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using Model;
using System.Collections.Generic;
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
            AllergenController.Instance.AddAllergen(new Allergen(newMedicine.Name));
            foreach(MedicineIngredient me in newMedicine.Ingredients)
                AllergenController.Instance.AddAllergen(new Allergen(me.Name));
        }
        public List<Medicine> GetAllMedicines()
        {
            return medicineService.GetAllMedicines();
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            medicineService.DeleteMedicine(medicineForDeleting);
        }
        public void SetMedicineList(List<Medicine> newMedicineList)
        {
            medicineService.SetMedicineList(newMedicineList);
        }
        public void SaveInFile()
        {
            medicineService.SaveInFile();
        }
        public void LoadFromFile()
        {
            medicineService.LoadFromFile();
        }
        public Medicine FindMedicineById(int id)
        {
            return  medicineService.FindMedicineById(id);
        }
        public void DeleteReplacementMedicine(Medicine replacementMedicine)
        {
            medicineService.DeleteReplacementMedicine(replacementMedicine);
        }
        public bool MedicineCommentExists()
        {
            return medicineService.MedicineCommentExists();
        }
        public List<Medicine> GetAllMedicinesWithComment()
        {
            return medicineService.GetAllMedicinesWithComment();
        }
    }
}
