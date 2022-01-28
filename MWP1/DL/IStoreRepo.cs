namespace DL;

public interface IStoreRepo
{
    //Store
    List<Store> GetAllStores();
    object AddStore(object entity);
    object ChangeStoreInfo(object entity);
    Task<Store> GetStoreByIdAsync(int StoreId);
    List<Store> SearchStores(string searchTerm);
    bool IsDuplicate(Store store);
    void Delete(object entity);
}