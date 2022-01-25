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
    void ChangeStoreInfo(Store changeStoreInfo);
    Task<Store> GetStoreByIdAsync(int StoreId);

    //Customers
    List<Customers> GetAllCustomers();
    Task<Customers> GetCustomerByIdAsync(int customerId);
    void AddCustomer(Customers addCust);//int custNum, string userName, string pass);
    void ChangeUserInfo(Customers changeCustomerInfo);
    bool Login(string userName, string password);

    //Inventory
    List<Inventory> GetAllInventory();
    List<Inventory> GetAllInventoryByStore(int storeId);
    Task<Inventory> GetAllInventoryByStoreAsync(int storeId);
    void AddInventory(Inventory invToAdd);
    void ChangeInventory(int invIndex, int qtyToChange);

    //Carried Items
    List<ProdDetails> GetAllCarried();
    Task<ProdDetails> GetCarriedByIdAsync(int carriedId);
    void AddCarried(ProdDetails itemNew);
    void ChangeCarried(ProdDetails changeCarriedItem);

    //Line Items
    List<LineItems> GetAllLineItem();
    Task<LineItems> GetLineitemsByIdAsync(int lineitemId);
    void AddLineItem(LineItems newLI);
    void FinalizeLineItem(LineItems finalLineItem);

    //Orders
    List<Orders> GetAllOrders();
    List<Orders> GetAllOrderByStore(int storeId);
    Task<Orders> GetOrderByIdAsync(int orderId);
    void AddOrder(Orders orderItems);
    void PlaceOrder(int orderId);
    void FinalizeOrder(Orders finalDetails);

    //Omni
    void OmniDelete(int whatTable, int idToDelete);
}