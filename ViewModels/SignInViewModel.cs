using FitAgent.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FitAgent.Services;

namespace FitAgent.ViewModels
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
                if (_userName == value) return;
                _userName = value;
                SignInMessageVisible = false;
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
                if (_password == value) return;
                _password = value;
                SignInMessageVisible = false;
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
            set
            {
                if (_entryAsPassword == value) return;
                _entryAsPassword = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PasswordIconCode));
                OnPropertyChanged(nameof(PasswordToggleDescription));
            }
        }

        // Both the icon and accessibility description follow the same visibility state.
        public string PasswordIconCode => EntryAsPassword
            ? FontHelper.CLOSED_EYE_ICON : FontHelper.OPEN_EYE_ICON;

        public string PasswordToggleDescription => EntryAsPassword
            ? AppMessages.ShowPassword : AppMessages.HidePassword;


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
            NavigateToSignUpCommand = new Command(OnNavigateToSignUp);
        }


        private bool CanSignIn()
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(UserPassword);
        }

        private void OnSignIn()
        {
            if (!CanSignIn()) return;
            UserName = AuthInput.NormalizeEmail(UserName);
            SignInMessageVisible = true;

            // The data service checks credentials; the view model updates the screen.
            if (_db.isExist(UserName, UserPassword))
            {
                LoginMessage = AppMessages.SignInSuccess;
                SignInMessageColor = Colors.LightGreen;
            }
            else
            {
                LoginMessage = AppMessages.SignInError;
                SignInMessageColor = Colors.OrangeRed;
            }
        }

        private void OnTogglePassword()
        {
            EntryAsPassword = !EntryAsPassword;
        }

        // Shell can keep pages alive, so clear temporary credentials when leaving.
        public void ResetSensitiveState()
        {
            UserPassword = string.Empty;
            EntryAsPassword = true;
            SignInMessageVisible = false;
        }
        public ICommand NavigateToSignUpCommand { get; }

        private async void OnNavigateToSignUp()
        {
            await Shell.Current.GoToAsync("//SignUp");
        }
    }
}
