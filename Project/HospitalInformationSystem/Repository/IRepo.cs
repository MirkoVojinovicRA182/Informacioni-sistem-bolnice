﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    public interface IRepo
    {
        void GetAll();
        void DeleteOne();
    }
}
