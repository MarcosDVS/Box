using Data.Context;
using Data.Model;

namespace Authentication;

public interface IUserAccountService
{
    Usuario? GetByUserName(string userName);
}
public class UserAccountService : IUserAccountService
{
    //private List<UserAccount> _users;

    //public UserAccountService()
    //{
       // _users = new List<UserAccount>
        //{
        //    new UserAccount{UserName = "admin", Password = "123", Role = "Administrator"},
        //    new UserAccount{UserName = "user", Password = "user", Role = "User"}
        //};
    //}

    #region Constructor y mienbro privado
    private MyDbContext _database;

    public UserAccountService(MyDbContext database)
    {
        _database = database;
    }
    #endregion
    public Usuario? GetByUserName(string userName)
    {
        return _database.Usuarios.FirstOrDefault(x => x.Email == userName);
    }
}
