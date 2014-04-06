using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsClient.Bindings
{
    public class Users
    {
        private List<User> mUsers = new List<User>();

        public List<User> User
        {
            get { return mUsers; }
            set { mUsers = value; }
        }
    }
}
