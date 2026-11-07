using FitAgent.ViewModels;

namespace FitAgent.Views;

public partial class SignUp : ContentPage
{
    public SignUp()
    {
        InitializeComponent();
        BindingContext = new SignUpViewModel();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // The page forwards its lifecycle event; the view model owns the reset.
        if (BindingContext is SignUpViewModel viewModel)
            viewModel.ResetSensitiveState();
    }
}