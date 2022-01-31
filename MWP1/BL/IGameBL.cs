namespace BL;

public interface IGameBL
{
    //Game
    List<Game> GetAllGames();
    object AddGame(object entity);
    object ChangeGameInfo(object entity);
    Task<Game> GetGameByIdAsync(int GameId);    bool IsDuplicate(Game Game);
    void Delete(object entity);
}