import re

class BaseSupplier:
    def __init__(self, name, address, phone_number, email, inn, ogrn):
        self.set_name(name)
        self.set_address(address)
        self.set_phone_number(phone_number)
        self.set_email(email)
        self.set_inn(inn)
        self.set_ogrn(ogrn)

    # Статические методы для валидации
    @staticmethod
    def validate_name(name):
        if not isinstance(name, str) or len(name)==0:
            return False
        return True

    @staticmethod
    def validate_address(address):
        if not isinstance(address, str) or not re.fullmatch(r"^[А-Яа-я\s]+,\s*[А-Яа-я\s,.\-]+,\s*\d+$", address):
            return False
        return True

    @staticmethod
    def validate_phone_number(phone_number):
        if not isinstance(phone_number, str) or not re.fullmatch(r'((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}', phone_number):
            return False
        return True

    @staticmethod
    def validate_email(email):
        if not isinstance(email, str) or not re.fullmatch(r'(.+)@(.+)\.(.+)', email):
            return False
        return True

    @staticmethod
    def validate_inn(inn):
        if not isinstance(inn, str) or not inn.isdigit() or len(inn) != 10:
            return False
        return True

    @staticmethod
    def validate_ogrn(ogrn):
        if not isinstance(ogrn, str) or not ogrn.isdigit() or len(ogrn) != 13:
            return False
        return True   

    # Getters
    def get_name(self):
        return self.__name

    def get_address(self):
        return self.__address
    
    def get_phone_number(self):
        return self.__phone_number

    def get_email(self):
        return self.__email

    def get_inn(self):
        return self.__inn

    def get_ogrn(self):
        return self.__ogrn

    # Setters
    def set_name(self, name):
        self.__name = name

    def set_address(self, address):
        self.__address = address

    def set_phone_number(self, phone_number):
        self.__phone_number = phone_number

    def set_email(self, email):
        self.__email = email

    def set_inn(self, inn):
        self.__inn = inn

    def set_ogrn(self, ogrn):
        self.__ogrn = ogrn
