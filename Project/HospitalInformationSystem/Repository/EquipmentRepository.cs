using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    class EquipmentRepository: IRepository
    {
        List<Equipment> _equipmentList;
        public EquipmentRepository()
        {
            _equipmentList = new List<Equipment>();
        }
        public void saveInFile()
        {
            FileStream fs = new FileStream("Equipment.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _equipmentList);
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

        public void loadFromFile()
        {
            if (File.Exists("Equipment.dat"))
            {
                FileStream fs = new FileStream("Equipment.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    setEquipment((List<Equipment>)formatter.Deserialize(fs));
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
        public void addNewEquipment(Equipment equipment)
        {
            _equipmentList.Add(equipment);
        }
        public List<Equipment> getStaticEquipment()
        {
            List<Equipment> staticEquipment = new List<Equipment>();
            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.Type == TypeOfEquipment.Static)
                    staticEquipment.Add(equipment);
            }
            return staticEquipment;
        }
        public List<Equipment> getDynamicEquipment()
        {
            List<Equipment> dynamicEquipment = new List<Equipment>();
            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.Type == TypeOfEquipment.Dynamic)
                    dynamicEquipment.Add(equipment);
            }
            return dynamicEquipment;
        }
        public List<Equipment> getEquipment()
        {
            return _equipmentList;
        }
        public void setEquipment(List<Equipment> equipment)
        {
            _equipmentList.Clear();
            foreach (Equipment eq in equipment)
                _equipmentList.Add(eq);
        }
        public void deleteEquipment(Equipment equipment)
        {
            _equipmentList.Remove(equipment);
        }
        public string getEquipmentNameById(string id)
        {
            String foundedName = "";
            foreach (Equipment eq in _equipmentList)
            {
                if (string.Equals(eq.Id, id))
                    foundedName = eq.Name;
            }
            return foundedName;
        }
        public string getEquipmentIdByName(string name)
        {
            String foundedId = "";
            foreach (Equipment eq in _equipmentList)
            {
                if (string.Equals(eq.Name, name))
                    foundedId = eq.Id;
            }
            return foundedId;
        }
        public TypeOfEquipment getEquipmentTypeById(string id)
        {
            TypeOfEquipment type = TypeOfEquipment.Static;
            foreach (Equipment eq in _equipmentList)
            {
                if (string.Equals(eq.Id, id))
                    type = eq.Type;
            }
            return type;
        }
        public Equipment findEquipmentById(string id)
        {
            foreach (Equipment eq in _equipmentList)
            {
                if (string.Equals(eq.Id, id))
                    return eq;
            }
            return null;
        }
    }
}
