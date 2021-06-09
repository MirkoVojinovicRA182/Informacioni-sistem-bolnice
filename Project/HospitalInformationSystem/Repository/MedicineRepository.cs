using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace HospitalInformationSystem.Repository
{
    public class MedicineRepository : IFactory, IFindEntities, IDeleteEntity, IAddEntity
    {
        public static ObservableCollection<Medicine> _medicineList;
        public MedicineRepository()
        {
            _medicineList = new ObservableCollection<Medicine>();
        }
        public static ObservableCollection<Medicine> GetAllMedicinesWithComment()
        {
            ObservableCollection<Medicine> medicinesWithComment = new ObservableCollection<Medicine>();
            foreach (Medicine medicine in _medicineList)
            {
                if (medicine.Comment != null)
                    medicinesWithComment.Add(medicine);
            }
            return medicinesWithComment;
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
        public ObservableCollection<object> GetAllEntities()
        {
            return new ObservableCollection<object> (_medicineList);
        }

        public void PermanentlyDelete(object entity)
        {
            _medicineList.Remove((Medicine)entity);
        }

        public void Serialization()
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

        public ObservableCollection<object> LoadAll()
        {
            if (File.Exists("Medicine.dat"))
            {
                FileStream fs = new FileStream("Medicine.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    _medicineList.Clear();
                    foreach (Medicine newMedicine in (ObservableCollection<Medicine>)formatter.Deserialize(fs))
                        _medicineList.Add(newMedicine);
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
            return new ObservableCollection<object>(_medicineList);
        }
        public bool SpecificEntityAttributeExists()
        {
            foreach (Medicine medicine in _medicineList)
            {
                if (medicine.Comment != null)
                    return true;
            }
            return false;
        }
        public ObservableCollection<object> GetEntitiesByComplexCondition()
        {
            ObservableCollection<Medicine> medicinesWithComment = new ObservableCollection<Medicine>();
            foreach (Medicine medicine in _medicineList)
            {
                if (medicine.Comment != null)
                    medicinesWithComment.Add(medicine);
            }
            return new ObservableCollection<object>(medicinesWithComment);
        }

        public void AddNew(object entity)
        {
            _medicineList.Add((Medicine)entity);
        }
    }
}
