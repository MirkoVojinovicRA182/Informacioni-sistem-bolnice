using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    class AllergenController
    {
        private static AllergenController instance = null;
        private AllergensService _allergenService;

        public static AllergenController Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AllergenController();
                }
                return instance;
            }
        }

        private AllergenController()
        {
            _allergenService = new AllergensService();
        }

        public void SaveInFile()
        {
            _allergenService.SaveInFile();
        }

        public void LoadFromFile()
        {
            _allergenService.LoadFromFile();
        }

        public void AddAllergen(Allergen newAllergen)
        {
            _allergenService.AddAllergen(newAllergen);
        }

        public ObservableCollection<Allergen> GetAllergens()
        {
            return _allergenService.GetAllergens();
        }


    }
}
