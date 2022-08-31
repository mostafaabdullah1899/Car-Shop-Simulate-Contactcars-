using Grade_Project_.Models;

namespace Grade_Project_.Repository
{
    public interface IUsers
    {
        void Insert(Users Users);
        void Edit(int id, Users Users);
        void Delete(int id);
        void blockAndUnblockUser(int id);
        List<Users> GetAll();
        Users GetById(int id);
        bool IsUserValid(Users user, out string validationMessage);
        Users Login(string userName, string password);
    }
}
