using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && 
                   phoneNumber.All(char.IsDigit) && 
                   phoneNumber.Length >= 10 && phoneNumber.Length <= 15;
        }

        public bool ValidateRequired(object value, string fieldName, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                errorMessage = $"{fieldName} không được để trống";
                return false;
            }
            return true;
        }

        public bool ValidateStringLength(string value, int minLength, int maxLength, string fieldName, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrEmpty(value))
            {
                if (minLength > 0)
                {
                    errorMessage = $"{fieldName} không được để trống";
                    return false;
                }
                return true;
            }

            if (value.Length < minLength || value.Length > maxLength)
            {
                errorMessage = $"{fieldName} phải từ {minLength} đến {maxLength} ký tự";
                return false;
            }
            return true;
        }

        public bool ValidateDateRange(DateTime startDate, DateTime endDate, string fieldName, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (startDate >= endDate)
            {
                errorMessage = $"{fieldName}: Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc";
                return false;
            }
            return true;
        }

        public bool ValidateNumber(int? value, int min, int max, string fieldName, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (value == null) return true;

            if (value < min || value > max)
            {
                errorMessage = $"{fieldName} phải từ {min} đến {max}";
                return false;
            }
            return true;
        }

        public async Task<List<string>> ValidateModelAsync<T>(T model) where T : class
        {
            var errors = new List<string>();
            // Thực hiện validation cơ bản dựa trên Data Annotations
            // Có thể sử dụng ModelState.IsValid trong Controller
            return errors;
        }
    }
} 