//Business Library
/*
This serves the functions of the business needs, performing regular functions that the management needs done.
*/
namespace BL;
public class ClayStoreBL : IStoreBL
{

    private IStoreRepo _dl;

    public ClayStoreBL(IStoreRepo repo)
    {
        _dl = repo;
    }

    //------------------------------------------------------------------------------\\
    //<>                                Stores                                    <>\\
    //------------------------------------------------------------------------------\\

    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }
    public object AddStore(object entity)
    {
        return _dl.AddStore(entity);
    }
    public object ChangeStoreInfo(object entity)//(int storeIndex, string name, string city, string state)
    {
        return _dl.ChangeStoreInfo(entity);
    }
    public async Task<Store> GetStoreByIdAsync(int storeId)
    {
        return await _dl.GetStoreByIdAsync(storeId);
    }
    
    public List<Store> SearchStores(string searchTerm)
    {
        return _dl.SearchStores(searchTerm);
    }

    public bool IsDuplicate(Store store)
    {
        return _dl.IsDuplicate(store);
    }

    public void Delete(object entity)
    {
        _dl.Delete(entity);
    }
}
