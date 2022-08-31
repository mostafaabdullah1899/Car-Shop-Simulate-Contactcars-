using Grade_Project_.Models;
using System.Text.RegularExpressions;

namespace Grade_Project_.Repository
{
    public class UsersRepository : IUsers
    {
        Cars_Entity context;
        public UsersRepository(Cars_Entity context)
        {
            this.context = context;
        }

        public Users GetById(int id)
        {

            return this.context.Users.FirstOrDefault(user => user.Id == id);

        }
        public List<Users> GetAll()
        {
            return context.Users.Where(user => user.Is_Admin==false).ToList();
        }

        public bool IsUserValid(Users user, out string validationMessage)
        {
            if (!isUserNameValid(user.UserName))
            {
                validationMessage = "invalid User Name, user name must be between 7 to 12 characters and must be start with alhpetical numbers ";
                return false;
            }
            if (!isPhoneNumberValid(user.PhoneNumber))
            {
                validationMessage = "invalid phone number ";
                return false;
            }
            if (!isValidPassword(user.PasswordHash))
            {
                validationMessage = "invalid pasword , password must have at least one number ,one lower case and one upper case letter  ";
                return false;
            }
            validationMessage = " valid user data";
            return true;
        }
        private bool isUserNameValid(string userName)
        {
            Regex regex = new Regex(@"^[A-Za-z][A-Za-z0-9_]{7,12}$");
            if (regex.IsMatch(userName))
                return true;
            return false;

        }
        private bool isPhoneNumberValid(string phoneNumber)
        {
            Regex regex = new Regex(@"^01[1+2+5][0-9]{8}$");
            if (regex.IsMatch(phoneNumber))
                return true;
            return false;


        }
        private bool isValidPassword(string password)
        {
            //(?=.*?[#?!@$%^&*-])
            Regex reges = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,20}$");
            if (reges.IsMatch(password))
                return true;
            return false;

        }

        public void Insert(Users Users)
        {
            this.context.Users.Add(Users);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            this.context.Remove(this.GetById(id));
            this.context.SaveChanges();
        }

        public void Edit(int id, Users newUser)
        {
            Users user = this.GetById(id);
            if (user != null)
            {
                user.UserName = newUser.UserName;
                user.PhoneNumber = newUser.PhoneNumber;
                user.PasswordHash = newUser.PasswordHash;
                user.User_Address = newUser.User_Address;
                user.Is_Admin = newUser.Is_Admin;
                user.Is_Active = newUser.Is_Active;
                this.context.Users.Update(user);
                this.context.SaveChanges();

            }
        }
        public void blockAndUnblockUser(int id)
        {
            Users user =GetById(id);
            if(user.Is_Active==true)
            {
                user.Is_Active = false;
                context.SaveChanges();
            }
            else
            {
                user.Is_Active = true;
                context.SaveChanges();
            }
            
        }
        public Users Login(string userName, string password)
        {
            return this.context.Users.FirstOrDefault(user => user.UserName == userName && user.PasswordHash == password);
        }
    }
}
