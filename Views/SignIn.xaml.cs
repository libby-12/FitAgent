using SignInAssignment.ViewModels;

namespace SignInAssignment.Views;

public partial class SignIn : ContentPage
{
	public SignIn()
	{
		InitializeComponent();

        BindingContext = new SignInViewModel();
    }
}