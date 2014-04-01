using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class UsersModel: Model
    {
        private HashSet<string> mUsers = new HashSet<string>();

        public UsersModel()
        {
        }

        public HashSet<string> Users
        {
            get { return mUsers; }
        }

        public void Add(string user)
        {
            mUsers.Add(user);
        }

        public void Remove(string user)
        {
            mUsers.Remove(user);
        }

        public bool Find(string user)
        {
            return mUsers.Contains(user);
        }
    }
}
