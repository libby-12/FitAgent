using FitAgent.ViewModels;

namespace FitAgent.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
    }
}