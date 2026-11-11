using FitAgent.ViewModels;

namespace FitAgent.Views;

public partial class SignUp : ContentPage
{
    public SignUp()
    {
        InitializeComponent();
        var viewModel = new SignUpViewModel();

        // Forward the Toolkit validation result to the ViewModel.
        EmailValidator.SetBinding(
            CommunityToolkit.Maui.Behaviors.ValidationBehavior.IsValidProperty,
            new Binding(nameof(SignUpViewModel.IsEmailValid),
                mode: BindingMode.OneWayToSource, source: viewModel));

        BindingContext = viewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // The page forwards its lifecycle event; the view model owns the reset.
        if (BindingContext is SignUpViewModel viewModel)
            viewModel.ResetForm();
    }
}