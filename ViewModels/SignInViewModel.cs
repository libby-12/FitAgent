using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SignInAssignment.Services;

namespace SignInAssignment.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private DBMockup _db;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
                ((Command)SignInCommand).ChangeCanExecute();
            }
        }

        private string _password;
        public string UserPassword
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ((Command)SignInCommand).ChangeCanExecute();
            }
        }

        private string _loginMessage;
        public string LoginMessage
        {
            get => _loginMessage;
            set { _loginMessage = value; OnPropertyChanged(); }
        }

        private bool _signInMessageVisible;
        public bool SignInMessageVisible
        {
            get => _signInMessageVisible;
            set { _signInMessageVisible = value; OnPropertyChanged(); }
        }

        private Color _signInMessageColor;
        public Color SignInMessageColor
        {
            get => _signInMessageColor;
            set { _signInMessageColor = value; OnPropertyChanged(); }
        }

        private bool _entryAsPassword = true;
        public bool EntryAsPassword
        {
            get => _entryAsPassword;
            set { _entryAsPassword = value; OnPropertyChanged(); }
        }

        private string _passwordIconCode = "\ue8f4"; // קוד יוניקוד של אייקון עין סגורה (לפי המצגת של המורה)
        public string PasswordIconCode
        {
            get => _passwordIconCode;
            set { _passwordIconCode = value; OnPropertyChanged(); }
        }


        public ICommand SignInCommand { get; }
        public ICommand TogglePasswordCommand { get; }

        public SignInViewModel()
        {
            _userName = string.Empty;
            _password = string.Empty;
            _loginMessage = string.Empty;
            _signInMessageColor = Colors.Transparent;

            _db = new DBMockup();

            SignInCommand = new Command(OnSignIn, CanSignIn);
            TogglePasswordCommand = new Command(OnTogglePassword);
        }


        private bool CanSignIn()
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(UserPassword);
        }

        private void OnSignIn()
        {
            SignInMessageVisible = true;

            bool userExists = false;
            foreach (var user in _db.Users)
            {
                if (user.UEmail == UserName && user.UPassword == UserPassword)
                {
                    userExists = true;
                    break;
                }
            }

            if (userExists)
            {
                LoginMessage = "התחברת בהצלחה!";
                SignInMessageColor = Colors.LightGreen;
            }
            else
            {
                LoginMessage = "שם משתמש או סיסמה שגויים";
                SignInMessageColor = Colors.OrangeRed;
            }
        }

        private void OnTogglePassword()
        {
            EntryAsPassword = !EntryAsPassword;
            PasswordIconCode = EntryAsPassword ? "\ue8f4" : "\ue8f5";
        }
    }
}
