using SignInAssignment.ViewModels;

namespace SignInAssignment.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
    }
}