using Microsoft.EntityFrameworkCore;

namespace DL;

public class EFRepo : IStoreRepo
{
    private CSDBContext _context;

    public EFRepo(CSDBContext context)
    {
        _context = context;
    }


    public List<Store> GetAllStores()
    {
        // throw new NotImplementedException();
        // List<Store> allResto = _context.Stores.Select(r => r).ToList();
        // return allResto;
        return _context.Stores.Select(r => r).ToList();
    }

    public object AddStore(object entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        return entity;
    }

    public object ChangeStoreInfo(object entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        // _context.Update(entity);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        return entity;
    }

    public async Task<List<Store>> GetAllStoresAsync()
    {
        return await _context.Stores
        //.Include(r => r.Reviews)
        .AsNoTracking()
        .Select(r=>r)
        .ToListAsync();

        // _context.StoreFronts
        // .Include(s => s.Inventories)
        // .ThenInclude(i => i.Item)
        // .Select(s => s)
        // .ToList();
    }

    public async Task<Store?> GetStoreByIdAsync(int rtoreId)
    {
        return await _context.Stores
        //.Include("Reviews")
        .FirstOrDefaultAsync(r => r.StoreID == rtoreId);
    }

    public List<Store> SearchStores(string searchTerm)
    {
        return _context.Stores.Where(x => x.StoreName.ToLower().Contains(searchTerm.ToLower()) ||
        x.State.ToLower().Contains(searchTerm.ToLower()) ||
        x.City.ToLower().Contains(searchTerm.ToLower()))
        .ToList();
    }


    public bool IsDuplicate(Store rtore)
    {
        /*
            In order to check for duplicate,
            I'm going to query for the first record that matches the name, city, and state of the rtore we've been given
        */

        Store? dupe = _context.Stores.FirstOrDefault(r => r.StoreName == rtore.StoreName && r.City == rtore.City && r.State == rtore.State);

        return dupe != null;
        /*
            if(dupe == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        */
    }

    public void Delete(object entity){
        _context.Remove(entity);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }
}