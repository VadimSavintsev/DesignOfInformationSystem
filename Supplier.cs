
using System;
using System.Text.RegularExpressions;

	public class Supplier
{
    private int? id;
    private string name;
    private string address;
    private string phoneNumber;
    private string email;
    private string inn;
    private string ogrn;

    public Supplier(string name, string address, string phoneNumber, string email, string inn, string ogrn)
    {
        SetName(name);
        SetAddress(address);
        SetPhoneNumber(phoneNumber);
        SetEmail(email);
        SetInn(inn);
        SetOgrn(ogrn);
    }
    public static bool ValidateName(string name)
    {
        return !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[А-Яа-я\\s-]+$");
    }

    public static bool ValidateAddress(string address)
    {
        return !string.IsNullOrEmpty(address) && Regex.IsMatch(address, "^[А-Яа-я\\s-]+,\\s*[А-Яа-я\\s,.\\-]+,\\s*\\d+$");
    }

    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrEmpty(phoneNumber) && Regex.IsMatch(phoneNumber, @"\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}");
    }

    public static bool ValidateEmail(string email)
    {
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"(.+)@(.+)\.(.+)");
    }

    public static bool ValidateInn(string inn)
    {
        return !string.IsNullOrEmpty(inn) && Regex.IsMatch(inn, @"\d{10}");
    }

    public static bool ValidateOgrn(string ogrn)
    {
        return !string.IsNullOrEmpty(ogrn) && Regex.IsMatch(ogrn, @"\d{13}");
    }

    public int? GetId()
    {
        return id;
    }

    public void SetId(int? id)
    {
        this.id = id;
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
}
	class Program
	{
		public static void Main(string[] args)
		{
			Supplier supplier = new Supplier(
            name: "ИП Иванов-Петров",
            address: "Санкт-Петербург, Невский проспект, 45",
            phoneNumber: "+7 (812) 987-65-43",
            email: "info@kopytairoga.ru",
            inn: "0987654321",
            ogrn: "3210987654321"
        );
        	Console.WriteLine(supplier.GetId());
        	Console.WriteLine(supplier.GetName());
        	Console.WriteLine(supplier.GetAddress());
        	supplier.SetEmail("newemail@example.com");
        	Console.WriteLine(supplier.GetEmail());
        	Console.ReadKey(true);
		}
	}