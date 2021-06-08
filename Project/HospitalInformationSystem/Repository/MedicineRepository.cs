using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace HospitalInformationSystem.Repository
{
    public class MedicineRepository : IRepository, IFind
    {
        private static List<Medicine> _medicineList;
        public MedicineRepository()
        {
            _medicineList = new List<Medicine>();
        }
        public void loadFromFile()
        {
            if (File.Exists("Medicine.dat"))
            {
                FileStream fs = new FileStream("Medicine.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    SetMedicineList((List<Medicine>)formatter.Deserialize(fs));
                }
                catch (SerializationException e)
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public void saveInFile()
        {
            FileStream fs = new FileStream("Medicine.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _medicineList);
            }
            catch (SerializationException e)
            {

                throw;
            }
            finally
            {
                fs.Close();
            }
        }
        public List<Medicine> GetAllMedicines()
        {
            return _medicineList;
        }
        public void SetMedicineList(List<Medicine> newMedicineList)
        {
            _medicineList.Clear();
            foreach (Medicine newMedicine in newMedicineList)
                _medicineList.Add(newMedicine);
        }
        /*public Medicine FindMedicineById(int id)
        {
            foreach (Medicine med in _medicineList)
            {
                if (med.Id == id)
                    return med;
            }
            return null;
        }*/
        public bool MedicineCommentExists()
        {
            foreach (Medicine medicine in _medicineList)
            {
                if (!string.Equals(medicine.Comment, "") && medicine.Comment != null)
                    return true;
            }
            return false;
        }
        public List<Medicine> GetAllMedicinesWithComment()
        {
            List<Medicine> medicinesWithComment = new List<Medicine>();
            foreach (Medicine medicine in _medicineList)
            {
                if (!string.Equals(medicine.Comment, "") && medicine.Comment != null)
                    medicinesWithComment.Add(medicine);
            }
            return medicinesWithComment;
        }
        public void AddMedicine(Medicine newMedicine)
        {
            _medicineList.Add(newMedicine);
        }
        public void DeleteMedicine(Medicine medicineForDeleting)
        {
            _medicineList.Remove(medicineForDeleting);
        }

        public object FindById(int id)
        {
            foreach (Medicine med in _medicineList)
            {
                if (med.Id == id)
                    return med;
            }
            return null;
        }
    }
}
