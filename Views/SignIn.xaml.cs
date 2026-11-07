using FitAgent.ViewModels;

namespace FitAgent.Views;

public partial class SignIn : ContentPage
{
	public SignIn()
	{
		InitializeComponent();

        BindingContext = new SignInViewModel();
    }
}