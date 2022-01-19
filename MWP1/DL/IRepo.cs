namespace DL;

//What's an interface?
//Interface is a contract, in essence
//It enforces that any type that implements the interface
//must also implement all of the interface's members as public methods
//We use interface to define/enforce a certain set of behavior on a type (such as class)
//This is an example of Abstraction (one of 4 OOP Pillars)
//Other OOP Pillars are Polymorphism, Inheritance, Encapsulation (A PIE!)
//Interface only has methods
public interface IRepo
{
    //Notice, how we're lacking access modifiers
    //interface members are implicitely public
    //they also lack method body

    //Store
    List<Store> GetAllStores();
    void AddStore(Store StoreToAdd);
    void ChangeStoreInfo(Store changeStoreInfo);//(int storeIndex, string name, string city, string state);
    void RemoveStore(int StoreToRemove);
    Task<Store> GetStoreByIdAsync(int StoreId);

    //Customers
    List<Customers> GetAllCustomers();
    Task<Customers> GetCustomerByIdAsync(int customerId);
    void AddCustomer(Customers addCust);//int custNum, string userName, string pass);

    //Inventory
    List<Inventory> GetAllInventory();
    void AddInventory(Inventory invToAdd);
    void ChangeInventory(int invIndex, int qtyToChange);//int invIndex, int apn, int itemQty);    
    void RemoveInventory(int invId);
    //void RemoveOrphanInventory(int storePK);

    //Carried Items
    List<ProdDetails> GetAllCarried();
    Task<ProdDetails> GetCarriedByIdAsync(int carriedId);
    void AddCarried(ProdDetails itemNew);//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void ChangeCarried(ProdDetails changeCarriedItem);//int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    //void AddItem(ProdDetails invToAdd); //int invIndex, 
    void RemoveItem(int apnToRemove);

    //Line Items
    List<LineItems> GetAllLineItem();
    void AddLineItem(LineItems newLI);//(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax);
    void RemoveLineItem(int lineItemIndexToRemove);
    void RemoveOrphanLineItem(int storePK);
    void FinalizeLineItem(LineItems finalLineItem);

    //Orders
    List<Orders> GetAllOrders();
    void AddOrder(Orders orderItems);
    void FinalizeOrder(int orderIndex, Orders finalDetails);
    void DeleteOrders(int ordersToDelete);
}