using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    class MedicineService
    {
        List<Medicine> medicineList;
        public MedicineService()
        {
            medicineList = new List<Medicine>();
        }

        public void AddMedicine(Medicine newMedicine)
        {
            medicineList.Add(newMedicine);
        }
        public List<Medicine> GetAllMedicines()
        {
            return medicineList;
        }
    }
}
