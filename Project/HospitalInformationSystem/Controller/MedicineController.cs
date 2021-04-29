using HospitalInformationSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
