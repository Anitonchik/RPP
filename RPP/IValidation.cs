using Microsoft.Extensions.Localization;

namespace RPP;

internal interface IValidation
{
    void Validate(IStringLocalizer<Messages> localizer);
}
