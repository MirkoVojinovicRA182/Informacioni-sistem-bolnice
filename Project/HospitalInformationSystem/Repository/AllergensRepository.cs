using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    class AllergensRepository : IRepository
    {
        public ObservableCollection<Allergen> allergens;

        public AllergensRepository()
        {
            allergens = new ObservableCollection<Allergen>();
        }

        public void loadFromFile()
        {
            if (File.Exists("Allergens.dat"))
            {
                FileStream fs = new FileStream("Allergens.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    allergens = (ObservableCollection<Allergen>)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    throw e;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public void saveInFile()
        {
            FileStream fs = new FileStream("Allergens.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, allergens);
            }
            catch (SerializationException e)
            {
                throw e;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
