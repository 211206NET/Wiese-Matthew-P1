using Microsoft.EntityFrameworkCore;

namespace DL;

public class EFGameRepo : IGameRepo
{
    private CSDBContext _context;

    public EFGameRepo(CSDBContext context)
    {
        _context = context;
    }


    public List<Game> GetAllGames()
    {
        // throw new NotImplementedException();
        // List<Game> allResto = _context.Games.Select(r => r).ToList();
        // return allResto;
        return _context.Games.Select(r => r).ToList();
    }

    public object AddGame(object entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        return entity;
    }

    public object ChangeGameInfo(object entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        // _context.Update(entity);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        return entity;
    }

    public async Task<List<Game>> GetAllGamesAsync()
    {
        return await _context.Games
        //.Include(r => r.Reviews)
        .AsNoTracking()
        .Select(r=>r)
        .ToListAsync();

        // _context.GameFronts
        // .Include(s => s.Inventories)
        // .ThenInclude(i => i.Item)
        // .Select(s => s)
        // .ToList();
    }

    public async Task<Game?> GetGameByIdAsync(int rtoreId)
    {
        return await _context.Games
        //.Include("Reviews")
        .FirstOrDefaultAsync(r => r.GameID == rtoreId);
    }

    public bool IsDuplicate(Game rtore)
    {
        /*
            In order to check for duplicate,
            I'm going to query for the first record that matches the name, city, and state of the rtore we've been given
        */

        Game? dupe = _context.Games.FirstOrDefault(r => r.GameID == rtore.GameID);

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