class Supplier(
    private var id: Int?=null,
    private var name: String,
    private var address: String,
    private var phoneNumber: String,
    private var email: String,
    private var inn: String,
    private var ogrn: String
)
{
    fun getId(): Int? {
        return id
    }

    fun setId(id: Int?) {
        this.id = id
    }

    fun getName(): String {
        return name
    }

    fun setName(name: String) {
        this.name = name
    }

    fun getAddress(): String {
        return address
    }

    fun setAddress(address: String) {
        this.address = address
    }

    fun getPhoneNumber(): String {
        return phoneNumber
    }

    fun setPhoneNumber(phoneNumber: String) {
        this.phoneNumber = phoneNumber
    }

    fun getEmail(): String {
        return email
    }

    fun setEmail(email: String) {
        this.email = email
    }

    fun getInn(): String {
        return inn
    }

    fun setInn(inn: String) {
        this.inn = inn
    }

    fun getOgrn(): String {
        return ogrn
    }

    fun setOgrn(ogrn: String) {
        this.ogrn = ogrn
    }
}

fun main() {
    val supplier = Supplier(
        name = "Example Supplier",
        address = "456 Supplier St",
        phoneNumber = "+0987654321",
        email = "supplier@example.com",
        inn = "0987654321",
        ogrn = "0987654321098"
    )

    println(supplier.getId())
    println(supplier.getName())
    println(supplier.getAddress())
    supplier.setEmail("newemail@example.com")
    println(supplier.getEmail())
}
