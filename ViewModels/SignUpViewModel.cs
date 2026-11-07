using FitAgent.Helper;
using FitAgent.Models; 
using FitAgent.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FitAgent.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private DBMockup _db;

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); ((Command)SignUpCommand).ChangeCanExecute(); }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); ((Command)SignUpCommand).ChangeCanExecute(); }
        }

        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); ((Command)SignUpCommand).ChangeCanExecute(); }
        }

        private string _userPassword = string.Empty;
        public string UserPassword
        {
            get => _userPassword;
            set { _userPassword = value; OnPropertyChanged(); ((Command)SignUpCommand).ChangeCanExecute(); }
        }

        private bool _isEmailValid;
        public bool IsEmailValid
        {
            get => _isEmailValid;
            set { _isEmailValid = value; OnPropertyChanged(); ((Command)SignUpCommand).ChangeCanExecute(); }
        }

        private string _signUpMessage = string.Empty;
        public string SignUpMessage
        {
            get => _signUpMessage;
            set { _signUpMessage = value; OnPropertyChanged(); }
        }

        private bool _signUpMessageVisible;
        public bool SignUpMessageVisible
        {
            get => _signUpMessageVisible;
            set { _signUpMessageVisible = value; OnPropertyChanged(); }
        }

        private Color _signUpMessageColor = Colors.Transparent;
        public Color SignUpMessageColor
        {
            get => _signUpMessageColor;
            set { _signUpMessageColor = value; OnPropertyChanged(); }
        }

        private bool _entryAsPassword = true;
        public bool EntryAsPassword
        {
            get => _entryAsPassword;
            set { _entryAsPassword = value; OnPropertyChanged(); }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); ((Command)SignUpCommand).ChangeCanExecute(); }
        }

        private string _passwordIconCode = FontHelper.CLOSED_EYE_ICON;
        public string PasswordIconCode
        {
            get => _passwordIconCode;
            set { _passwordIconCode = value; OnPropertyChanged(); }
        }

        public ICommand SignUpCommand { get; }
        public ICommand TogglePasswordCommand { get; }

        public SignUpViewModel()
        {
            _db = new DBMockup();
            SignUpCommand = new Command(OnSignUp, CanSignUp);
            TogglePasswordCommand = new Command(OnTogglePassword);
            NavigateToSignInCommand = new Command(OnNavigateToSignIn);
        }

        private bool CanSignUp()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(UserName) &&
                   !string.IsNullOrWhiteSpace(UserPassword) &&
                   !string.IsNullOrWhiteSpace(Phone) &&
                   IsEmailValid;
        }

        private void OnSignUp()
        {
            SignUpMessageVisible = true;

            if (_db.EmailExists(UserName))
            {
                SignUpMessage = AppMessages.EmailAlreadyExists;
                SignUpMessageColor = Colors.OrangeRed; 
            }
            else
            {
                User newUser = new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    UEmail = UserName,
                    UPassword = UserPassword,
                    Phone = Phone
                };

                _db.AddUser(newUser); 

                SignUpMessage = AppMessages.SignUpSuccess;
                SignUpMessageColor = Colors.LightGreen; 
            }
        }

        private void OnTogglePassword()
        {
            EntryAsPassword = !EntryAsPassword;
            PasswordIconCode = EntryAsPassword ? FontHelper.CLOSED_EYE_ICON : FontHelper.OPEN_EYE_ICON;
        }

        public ICommand NavigateToSignInCommand { get; }

        private async void OnNavigateToSignIn()
        {
            await Shell.Current.GoToAsync("//SignIn");
        }
    }
}
