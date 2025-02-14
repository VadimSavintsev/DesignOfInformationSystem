using System.Text;

namespace OOP_new
{
    public interface SupplierController
    {
        public void Handle(string name, string address, string phoneNumber, string email, string inn, string ogrn);
        public static void ValidateData(string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            StringBuilder errors = new StringBuilder();

            if (!SupplierShort.ValidateName(name))
            {
                errors.AppendLine("Название должно содержать русские буквы, пробелы и дефис.\n");
            }
            if (!Supplier.ValidateAddress(address))
            {
                errors.AppendLine("Адрес должен содержать через запятую город, улицу и дом с номером.\n");
            }
            if (!SupplierShort.ValidatePhoneNumber(phoneNumber))
            {
                errors.AppendLine("Телефон должен содержать цифры и быть записан в формате: +7(***)***-**-**.\n");
            }
            if (!Supplier.ValidateEmail(email))
            {
                errors.AppendLine("Неверно введён адрес электронной почты.\n");
            }
            if (!SupplierShort.ValidateInn(inn))
            {
                errors.AppendLine("ИНН должен содержать 10 цифр.\n");
            }
            if (!SupplierShort.ValidateOgrn(ogrn))
            {
                errors.AppendLine("ОГРН должен содержать 13 цифр.\n");
            }

            if (errors.Length > 0)
            {
                throw new ArgumentException(errors.ToString());
            }
        }
    }
}
