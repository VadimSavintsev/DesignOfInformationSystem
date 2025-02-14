namespace OOP_new
{
    public class SupplierObservable
    {
        private readonly List<SupplierObserver> observers = new List<SupplierObserver>();

        public void AddObserver(SupplierObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(SupplierObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(string action, object data)
        {
            foreach (var observer in observers)
            {
                observer.Update(action, data);
            }
        }
    }
}
