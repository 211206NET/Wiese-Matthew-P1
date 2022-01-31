//Business Library
/*
This serves the functions of the business needs, performing regular functions that the management needs done.
*/
namespace BL;
public class CSBL : IBL
{

    private IRepo _dl;

    public CSBL(IRepo repo)
    {
        _dl = repo;
    }

    //------------------------------------------------------------------------------\\
    //<>                                Stores                                    <>\\
    //------------------------------------------------------------------------------\\
    /*
    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }
    public void AddStore(Store storeToAdd)
    {
        _dl.AddStore(storeToAdd);
    }
    public void ChangeStoreInfo(Store changeStoreInfo)//(int storeIndex, string name, string city, string state)
    {
       _dl.ChangeStoreInfo(changeStoreInfo);
    }
    public async Task<Store> GetStoreByIdAsync(int storeId)
    {
        return await _dl.GetStoreByIdAsync(storeId);
    }
    */
    
    //------------------------------------------------------------------------------\\
    //<>                               Customers                                  <>\\
    //------------------------------------------------------------------------------\\

    public List<Customers> GetAllCustomers()
    {
        return _dl.GetAllCustomers();
    }
    public async Task<Customers> GetCustomerByIdAsync(int customerId)
    {
        return await _dl.GetCustomerByIdAsync(customerId);
    }
    public void AddCustomer(Customers addCust)//int custNum, string userName, string pass, bool employee)
    {
        _dl.AddCustomer(addCust);//custNum, userName, pass, employee);
    }

    public void ChangeUserInfo(Customers changeCustomerInfo)
    {
        _dl.ChangeUserInfo(changeCustomerInfo);
    }

    public bool Login(string userName, string password)
    {
        return _dl.Login(userName, password);
    }

    //------------------------------------------------------------------------------\\
    //<>                               Inventory                                  <>\\
    //------------------------------------------------------------------------------\\

    public List<Inventory> GetAllInventory()
    {
        return _dl.GetAllInventory();
    }

    public List<Inventory> GetAllInventoryByStore(int storeId)
    {
        return _dl.GetAllInventoryByStore(storeId);
    }

    public async Task<Inventory> GetAllInventoryByStoreAsync(int storeId)
    {
        return await _dl.GetAllInventoryByStoreAsync(storeId);
    }

    public void AddInventory(Inventory invToAdd)
    {
        _dl.AddInventory(invToAdd);
    }
    public void ChangeInventory(int invIndex, int qtyToChange)//int invIndex, int itemIndex, int itemQty)
    {
        _dl.ChangeInventory(invIndex, qtyToChange);
    }

    //------------------------------------------------------------------------------\\
    //<>                              Carried Items                               <>\\
    //------------------------------------------------------------------------------\\

    //AddCarried
    public List<ProdDetails> GetAllCarried()
    {
        return _dl.GetAllCarried();
    }
    public async Task<ProdDetails> GetCarriedByIdAsync(int carriedId)
    {
        return await _dl.GetCarriedByIdAsync(carriedId);
    }
    //public void AddCarried(ProdDetails addDetails)
    public void AddCarried(ProdDetails itemNew)//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
        _dl.AddCarried(itemNew);
    }
    //public void AddCarried(ProdDetails addDetails)
    public void ChangeCarried(ProdDetails changeCarriedItem)//int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        _dl.ChangeCarried(changeCarriedItem);//itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
    }

    //------------------------------------------------------------------------------\\
    //<>                              Line Items                                  <>\\
    //------------------------------------------------------------------------------\\

    public List<LineItems> GetAllLineItem()
    {
        return _dl.GetAllLineItem();
    }

    public async Task<LineItems> GetLineitemsByIdAsync(int lineitemId)
    {
        return await _dl.GetLineitemsByIdAsync(lineitemId);
    }

    public void AddLineItem(LineItems newLI)//(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax)
    {
        _dl.AddLineItem(newLI);//(apn, name, qty, costPerItem, salesTax);
    }

    public void FinalizeLineItem(LineItems finalLineItem)
    {
        _dl.FinalizeLineItem(finalLineItem);
    }

    //------------------------------------------------------------------------------\\
    //<>                                Orders                                    <>\\
    //------------------------------------------------------------------------------\\

    public List<Orders> GetAllOrders()
    {
        return _dl.GetAllOrders();
    }

    public List<Orders> GetAllOrderByStore(int storeId)
    {
        return _dl.GetAllOrderByStore(storeId);
    }

    public async Task<Orders> GetOrderByIdAsync(int orderId)
    {
        return await _dl.GetOrderByIdAsync(orderId);
    }

    //Add a new order to order history
    public void AddOrder(Orders orderItems)
    {
        _dl.AddOrder(orderItems);
    }

    public void PlaceOrder(int orderId)
    {
        _dl.PlaceOrder(orderId);
    }

    public void FinalizeOrder(Orders finalDetails)
    {
        _dl.FinalizeOrder(finalDetails);
    }

    //Omni
    public void OmniDelete(int whatTable, int idToDelete)
    {
        _dl.OmniDelete(whatTable, idToDelete);
    }

}
