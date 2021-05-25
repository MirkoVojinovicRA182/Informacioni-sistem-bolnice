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
    class AccountService
    {
        private AccountRepository _accountRepository;
        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }
        public void SaveInFile()
        {
            _accountRepository.saveInFile();
        }
        public void LoadFromFile()
        {
            _accountRepository.loadFromFile();
        }
        public ObservableCollection<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
        public void AddNewAccount(Account newAccount)
        {
            _accountRepository.AddNewAccount(newAccount);
        }
    }
}
