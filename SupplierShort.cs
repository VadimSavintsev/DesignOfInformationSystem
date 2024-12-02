using System;
using System.Text.RegularExpressions;
 
namespace Project
{
    public class SupplierShort
    {
        protected string name;
        protected string phoneNumber;
        protected string inn;
        protected string ogrn;
 
        public SupplierShort(string name, string phoneNumber, string inn, string ogrn)
        {
            SetName(name);
            SetPhoneNumber(phoneNumber);
            SetInn(inn);
            SetOgrn(ogrn);
        }
 
        public static bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[А-Яа-я\\s-]+$");
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && Regex.IsMatch(phoneNumber, @"\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}");
        }
        public static bool ValidateInn(string inn)
        {
            return !string.IsNullOrEmpty(inn) && Regex.IsMatch(inn, @"\d{10}");
        }
        public static bool ValidateOgrn(string ogrn)
        {
            return !string.IsNullOrEmpty(ogrn) && Regex.IsMatch(ogrn, @"\d{13}");
        }
 
        public string GetName()
        {
            return name;
        }
 
        public void SetName(string name)
        {
            if (ValidateName(name))
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentException("Название должно содержать только русские символы");
            }
        }
 
        public string GetPhoneNumber()
        {
            return phoneNumber;
        }
 
        public void SetPhoneNumber(string phoneNumber)
        {
            if (ValidatePhoneNumber(phoneNumber))
            {
                this.phoneNumber = phoneNumber;
            }
            else
            {
                throw new ArgumentException("Телефон должен содержать цифры и быть записан в формате: +7 (***) ***-**-**");
            }
        }
 
        public string GetInn()
        {
            return inn;
        }
 
        public void SetInn(string inn)
        {
            if (ValidateInn(inn))
            {
                this.inn = inn;
            }
            else
            {
                throw new ArgumentException("ИНН должен содержать 10 цифр");
            }
        }
        public string GetOgrn()
        {
            return ogrn;
        }
 
        public void SetOgrn(string ogrn)
        {
            if (ValidateOgrn(ogrn))
            {
                this.ogrn = ogrn;
            }
            else
            {
                throw new ArgumentException("ОГРН должен содержать 13 цифр");
            }
        }
 
        public override string ToString()
        {
            return $"Name: {name}, Phone: {phoneNumber}, INN: {inn}, OGRN: {ogrn}";
        }
    }
}
