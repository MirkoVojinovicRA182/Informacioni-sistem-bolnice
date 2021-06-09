using HospitalInformationSystem.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    class AllergensService
    {
        private AllergensRepository _allergensFile;

        public AllergensService()
        {
            _allergensFile = new AllergensRepository();
        }

        public ObservableCollection<Allergen> GetAllergens()
        {
            if (_allergensFile.allergens == null)
                _allergensFile.allergens = new ObservableCollection<Allergen>();
            return _allergensFile.allergens;
        }

        public void SetAllergens(ObservableCollection<Allergen> allergenList)
        {
            _allergensFile.allergens = allergenList;
        }

        public void AddAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (_allergensFile.allergens == null)
                _allergensFile.allergens = new ObservableCollection<Allergen>();

            foreach (Allergen allergen in _allergensFile.allergens)
            {
                if (allergen.Name == newAllergen.Name)
                    return;
            }
            _allergensFile.allergens.Add(newAllergen);
        }

        public void SaveInFile()
        {
            _allergensFile.saveInFile();
        }

        public void LoadFromFile()
        {
            _allergensFile.loadFromFile();
        }
    }
}
