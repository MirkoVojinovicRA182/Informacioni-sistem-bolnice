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
    class AccountRepository : IRepository
    {
        private ObservableCollection<Account> _allAccounts;
        public AccountRepository()
        {
            _allAccounts = new ObservableCollection<Account>();
        }
        public void loadFromFile()
        {
            if (File.Exists("Accounts.dat"))
            {
                FileStream fs = new FileStream("Accounts.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    SetAllAccounts((ObservableCollection<Account>)formatter.Deserialize(fs));
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
            FileStream fs = new FileStream("Accounts.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _allAccounts);
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
        public ObservableCollection<Account> GetAllAccounts()
        {
            return _allAccounts;
        }
        public void SetAllAccounts(ObservableCollection<Account> accounts)
        {
            _allAccounts.Clear();
            foreach (Account account in accounts)
                _allAccounts.Add(account);
        }
        public void AddNewAccount(Account newAccount)
        {
            _allAccounts.Add(newAccount);
        }
    }
}
