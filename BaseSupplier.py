class BaseSupplier:
    def __init__(self, name, address, phone_number, email, inn, ogrn):
        self.set_name = name
        self.set_address = address
        self.set_phone_number = phone_number
        self.set_email = email
        self.set_inn = inn
        self.set_ogrn = ogrn

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