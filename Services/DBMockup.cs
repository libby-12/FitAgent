using FitAgent.Helper;
using FitAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitAgent.Services
{
    public class DBMockup
    {
        private static List<User> _users = new List<User>()
        {
            new User { FirstName = "Tomer", LastName = "Marty", UEmail = "admin@mail.com", UPassword = "admin", Phone = "050-1111111" },
            new User { FirstName = "Amir", LastName = "Mayo", UEmail = "user1@mail.com", UPassword = "pass1", Phone = "050-2222222" },
            new User { FirstName = "Galit", LastName = "Nuvy", UEmail = "user2@mail.com", UPassword = "pass2", Phone = "050-3333333" }
        };

        public List<User> Users
        {
            get { return _users; }
        }

        public DBMockup()
        {
            // בנאי ריק, כי הנתונים כבר מופעלים למעלה
        }

        public void AddUser(User newUser)
        {
            newUser.UEmail = AuthInput.NormalizeEmail(newUser.UEmail);
            _users.Add(newUser);
        }

        // Registration checks only the email; sign-in checks email and password together.
        public bool EmailExists(string uEmail)
        {
            return _users.Any(u => AuthInput.EmailsMatch(u.UEmail, uEmail));
        }

        public bool isExist(string uEmail, string uPass)
        {
            return _users.Any(u => AuthInput.EmailsMatch(u.UEmail, uEmail) && u.UPassword == uPass);
        }
    }
}
