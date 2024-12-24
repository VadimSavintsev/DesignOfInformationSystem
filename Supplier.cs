using System;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
 
namespace Project
{
    public class Supplier : SupplierShort
    {
        [JsonInclude]
        private string address;
        [JsonInclude]
        private string email;
 
        public Supplier(int?id, string name, string address, string phoneNumber, string email, string inn, string ogrn)
            : base(id, name, phoneNumber, inn, ogrn)
        {
            SetAddress(address);
            SetEmail(email);
        }
 
        public static bool ValidateAddress(string address)
        {
            return !string.IsNullOrEmpty(address) && Regex.IsMatch(address, "^[А-Яа-я\\s-]+,\\s*[А-Яа-я\\s,.\\-]+,\\s*\\d+$");
        }
 
        public static bool ValidateEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"(.+)@(.+)\.(.+)");
        }
 
        public string GetAddress()
        {
            return address;
        }
 
        public void SetAddress(string address)
        {
            if (ValidateAddress(address))
            {
                this.address = address;
            }
            else
            {
                throw new ArgumentException("Адрес должен содержать через запятую город, улицу и дом с номером");
            }
        }
 
        public string GetEmail()
        {
            return email;
        }
 
        public void SetEmail(string email)
        {
            if (ValidateEmail(email))
            {
                this.email = email;
            }
            else
            {
                throw new ArgumentException("Неверно введён адрес электронной почты");
            }
        }
 
        public string ToFullString()
        {
            return $"ID: {id}, Name: {name}, Address: {address}, Phone: {phoneNumber}, Email: {email}, INN: {inn}, OGRN: {ogrn}";
        }
    }
}
