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
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                SignUpMessageVisible = false;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                SignUpMessageVisible = false;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                SignUpMessageVisible = false;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowEmailError));
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        private string _userPassword = string.Empty;
        public string UserPassword
        {
            get => _userPassword;
            set
            {
                if (_userPassword == value) return;
                _userPassword = value;
                SignUpMessageVisible = false;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        private bool _isEmailValid;
        public bool IsEmailValid
        {
            get => _isEmailValid;
            set
            {
                if (_isEmailValid == value) return;
                _isEmailValid = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowEmailError));
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        public bool ShowEmailError => !string.IsNullOrWhiteSpace(UserName) && !IsEmailValid;

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
            set
            {
                if (_entryAsPassword == value) return;
                _entryAsPassword = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PasswordIconCode));
                OnPropertyChanged(nameof(PasswordToggleDescription));
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone == value) return;
                _phone = value;
                SignUpMessageVisible = false;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        // Both the icon and accessibility description follow the same visibility state.
        public string PasswordIconCode => EntryAsPassword
            ? FontHelper.CLOSED_EYE_ICON : FontHelper.OPEN_EYE_ICON;

        public string PasswordToggleDescription => EntryAsPassword
            ? AppMessages.ShowPassword : AppMessages.HidePassword;

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
                   AuthInput.IsPasswordValid(UserPassword) &&
                   AuthInput.IsPhoneValid(Phone) &&
                   IsEmailValid;
        }

        private void OnSignUp()
        {
            if (!CanSignUp()) return;
            UserName = AuthInput.NormalizeEmail(UserName);
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
                    Phone = AuthInput.NormalizePhone(Phone)
                };

                _db.AddUser(newUser); 

                SignUpMessage = AppMessages.SignUpSuccess;
                SignUpMessageColor = Colors.LightGreen; 
            }
        }

        private void OnTogglePassword()
        {
            EntryAsPassword = !EntryAsPassword;
        }

        // Shell can reuse the page, so clear the form when leaving.
        public void ResetForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            UserName = string.Empty;
            Phone = string.Empty;
            IsEmailValid = false;
            UserPassword = string.Empty;
            EntryAsPassword = true;
            SignUpMessageVisible = false;
        }
        public ICommand NavigateToSignInCommand { get; }

        private async void OnNavigateToSignIn()
        {
            await Shell.Current.GoToAsync("//SignIn");
        }
    }
}
