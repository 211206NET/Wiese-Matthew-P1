namespace BL;

//Interface for Stores
public interface IBL
{
    //Notice, how we're lacking access modifiers
    //interface members are implicitely public
    //they also lack method body

    //Stores
    List<Store> GetAllStores();
    void AddStore(Store storeToAdd);
    void ChangeStoreInfo(Store changeStoreInfo);
    Task<Store> GetStoreByIdAsync(int StoreId);

    //Customers
    List<Customers> GetAllCustomers();
    Task<Customers> GetCustomerByIdAsync(int customerId);
    void AddCustomer(Customers addCust);
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