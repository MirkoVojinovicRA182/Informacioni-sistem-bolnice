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
    class AccountController
    {
        private AccountService _accountService;
        private static AccountController _instance = null;
        public static AccountController GetInstance()
        {
            if (_instance == null)
                _instance = new AccountController();
            return _instance;
        }
        private AccountController()
        {
            _accountService = new AccountService();
        }
        public void SaveInFile()
        {
            _accountService.SaveInFile();
        }
        public void LoadFromFile()
        {
            _accountService.LoadFromFile();
        }
        public ObservableCollection<Account> GetAllAccounts()
        {
            return _accountService.GetAllAccounts();
        }
        public void AddNewAccount(Account newAccount)
        {
            _accountService.AddNewAccount(newAccount);
        }
    }
}
