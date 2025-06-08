using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace RPP.DataModels;

internal class BuyerDataModel(string id, string fio, string phoneNumber) : IValidation
{
    public string Id { get; private set; } = id;
    public string FIO { get; private set; } = fio;
    public string PhoneNumber { get; private set; } = phoneNumber;

    public void Validate(IStringLocalizer<Messages> localizer)
    {
        if (Id.IsEmpty())
            throw new ValidationException(string.Format(localizer["ValidationExceptionMessageEmptyField"], "Id"));

        if (!Id.IsGuid())
            throw new ValidationException(string.Format(localizer["ValidationExceptionMessageNotAId"], "Id"));

        if (FIO.IsEmpty())
            throw new ValidationException(string.Format(localizer["ValidationExceptionMessageEmptyField"], "FIO"));

        if (PhoneNumber.IsEmpty())
            throw new ValidationException(string.Format(localizer["ValidationExceptionMessageEmptyField"], "PhoneNumber"));

        if (!Regex.IsMatch(PhoneNumber, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\-]?)?[\d\- ]{7,10}$"))
            throw new ValidationException(localizer["ValidationExceptionMessageIncorrectPhoneNumber"]);
    }
}
