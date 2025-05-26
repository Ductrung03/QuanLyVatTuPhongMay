namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateEmail(string email);
        bool ValidatePhoneNumber(string phoneNumber);
        bool ValidateRequired(object value, string fieldName, out string errorMessage);
        bool ValidateStringLength(string value, int minLength, int maxLength, string fieldName, out string errorMessage);
        bool ValidateDateRange(DateTime startDate, DateTime endDate, string fieldName, out string errorMessage);
        bool ValidateNumber(int? value, int min, int max, string fieldName, out string errorMessage);
        Task<List<string>> ValidateModelAsync<T>(T model) where T : class;
    }
} 