using CommunityToolkit.Maui.Behaviors;
using FitAgent.Helper;

namespace FitAgent.Behaviors;

public sealed class TrimmedEmailValidationBehavior : EmailValidationBehavior
{
    protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
    {
        return base.ValidateAsync(AuthInput.NormalizeEmail(value), token);
    }
}
