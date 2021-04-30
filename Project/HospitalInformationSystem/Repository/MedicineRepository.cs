using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
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
    class MedicineRepository : IRepository
    {
        public void loadFromFile()
        {
            if (File.Exists("Medicine.dat"))
            {
                FileStream fs = new FileStream("Medicine.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MedicineController.GetInstance().SetMedicineList((List<Medicine>)formatter.Deserialize(fs));
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
                formatter.Serialize(fs, MedicineController.GetInstance().GetAllMedicines());
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
}
