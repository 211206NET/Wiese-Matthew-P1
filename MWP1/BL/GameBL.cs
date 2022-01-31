//Business Library
/*
This serves the functions of the business needs, performing regular functions that the management needs done.
*/
namespace BL;
public class GameBL : IGameBL
{

    private IGameRepo _dl;

    public GameBL(IGameRepo repo)
    {
        _dl = repo;
    }

    //------------------------------------------------------------------------------\\
    //<>                                Games                                    <>\\
    //------------------------------------------------------------------------------\\

    public List<Game> GetAllGames()
    {
        return _dl.GetAllGames();
    }
    public object AddGame(object entity)
    {
        return _dl.AddGame(entity);
    }
    public object ChangeGameInfo(object entity)//(int GameIndex, string name, string city, string state)
    {
        return _dl.ChangeGameInfo(entity);
    }
    public async Task<Game> GetGameByIdAsync(int GameId)
    {
        return await _dl.GetGameByIdAsync(GameId);
    }

    public bool IsDuplicate(Game Game)
    {
        return _dl.IsDuplicate(Game);
    }

    public void Delete(object entity)
    {
        _dl.Delete(entity);
    }
}
