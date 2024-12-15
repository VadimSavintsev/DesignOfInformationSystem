using System.Collections.Generic;
using Project;

namespace Program
{
    public interface SupplierStrategy
    {
        //Чтение всех значений из файла
        List<Supplier> ReadAllValues();

        // Запись всех значений в файл
        void WriteAllValues(List<Supplier> suppliers);
    }
}
