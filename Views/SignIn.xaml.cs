using FitAgent.ViewModels;

namespace FitAgent.Views;

public partial class SignIn : ContentPage
{
    public SignIn()
    {
        InitializeComponent();
        BindingContext = new SignInViewModel();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // The page forwards its lifecycle event; the view model owns the reset.
        if (BindingContext is SignInViewModel viewModel)
            viewModel.ResetForm();
    }
}