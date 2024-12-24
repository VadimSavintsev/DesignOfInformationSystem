using System;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
 
namespace Project
{
    public class SupplierShort
    {
        [JsonInclude]
        [YamlMember(Alias = "id")]
        protected int? id;
        [JsonInclude]
        [YamlMember(Alias = "name")]
        protected string name;
        [JsonInclude]
        [YamlMember(Alias = "phoneNumber")]
        protected string phoneNumber;
        [JsonInclude]
        [YamlMember(Alias = "inn")]
        protected string inn;
        [JsonInclude]
        [YamlMember(Alias = "ogrn")]
        protected string ogrn;

        public SupplierShort(int? id, string name, string phoneNumber, string inn, string ogrn)
        {
            SetId(id);
            SetName(name);
            SetPhoneNumber(phoneNumber);
            SetInn(inn);
            SetOgrn(ogrn);
        }

        public int? Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
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
 
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (ValidateName(value))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException("Название должно содержать только русские символы, пробелы и дефисы");
                }
            }
        }
 
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (ValidatePhoneNumber(value))
                {
                    phoneNumber = value;
                }
                else
                {
                    throw new ArgumentException("Телефон должен содержать цифры и быть записан в формате: +7 (***) ***-**-**");
                }
            }
        }
 
        public string Inn
        {
            get
            {
                return inn;
            }
            set
            {
                if (ValidateInn(value))
                {
                    inn = value;
                }
                else
                {
                    throw new ArgumentException("ИНН должен содержать 10 цифр");
                }
            }
        }
     
        public string Ogrn
        {
            get
            {
                return ogrn;
            }
            set
            {
                if (ValidateOgrn(value))
                {
                    ogrn = value;
                }
                else
                {
                    throw new ArgumentException("ОГРН должен содержать 13 цифр");
                }
            }
        }

        public override bool Equals(object obj)
        {
           if (obj == null || GetType() != obj.GetType())
           {
               return false;
           }
           Supplier other = (Supplier)obj;
           return inn == other.inn && ogrn == other.ogrn;
        }
 
        public string ToShortString()
        {
            return $"ID: {id}, Name: {name}, Phone: {phoneNumber}, INN: {inn}, OGRN: {ogrn}";
        }
    }
}
