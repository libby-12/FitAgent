using SignInAssignment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignInAssignment.Services
{
    public class DBMockup
    {
        private List<User> _users;

        public List<User> Users
        {
            get { return _users; }
        }

        public DBMockup()
        {
            _users = new List<User>();
            _users.Add(new User { FirstName = "Tomer", LastName = "Marty", UEmail = "admin@mail.com", UPassword = "admin", Phone = "050-1111111" });
            _users.Add(new User { FirstName = "Amir", LastName = "Mayo", UEmail = "user1@mail.com", UPassword = "pass1", Phone = "050-2222222" });
            _users.Add(new User { FirstName = "Galit", LastName = "Nuvy", UEmail = "user2@mail.com", UPassword = "pass2", Phone = "050-3333333" });
        }

        public void AddUser(User newUser)
        {
            _users.Add(newUser);
        }
    }
}
