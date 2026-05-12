using System.Collections.Generic;

public interface IUserRepository
{
    List<User> GetAll();
    User GetById(int id);
    User GetByUserName(string userName);
    void Add(User user);
    void Update(User user);
    void Delete(int id);
}