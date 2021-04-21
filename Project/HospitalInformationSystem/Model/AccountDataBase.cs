using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AccountDataBase
    {
        public static AccountDataBase instance;

        private List<Account> accounts;

        public static AccountDataBase getInstance()
        {
            if (instance == null)
                instance = new AccountDataBase();
            return instance;
        }
        private AccountDataBase()
        {

        }

        public List<Account> GetAccount()
        {
            if (accounts == null)
                accounts = new List<Account>();
            return accounts;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetAccount(List<Account> newAccount)
        {
            RemoveAllAccount();
            foreach (Account oAccount in newAccount)
                AddAccount(oAccount);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddAccount(Account newAccount)
        {
            if (newAccount == null)
                return;
            if (this.accounts == null)
                this.accounts = new List<Account>();
            if (!this.accounts.Contains(newAccount))
                this.accounts.Add(newAccount);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveAccount(Account oldAccount)
        {
            if (oldAccount == null)
                return;
            if (this.accounts != null)
                if (this.accounts.Contains(oldAccount))
                    this.accounts.Remove(oldAccount);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllAccount()
        {
            if (accounts != null)
                accounts.Clear();
        }

    }
}
