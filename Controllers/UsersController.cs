using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grade_Project_.Models;
using Grade_Project_.Repository;

namespace Grade_Project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUsers usersRepos;

        public UsersController(IUsers users)
        {
            this.usersRepos = users;
        }


        [HttpPost]
        [Route("[action]")]

        public IActionResult AddUser(string userName, string phoneNumber, bool Is_Admin, string password, string User_Address, bool Is_Active)
        {
            Users user = new Users();
            user.UserName = userName;
            user.PhoneNumber = phoneNumber;
            user.Is_Admin = Is_Admin;
            user.PasswordHash = password;
            user.User_Address = User_Address;
            user.Is_Active = Is_Active;

            string validationMessage;
            if (!usersRepos.IsUserValid(user, out validationMessage))
                return Ok(validationMessage);
            if (ModelState.IsValid)
            {
                try
                {
                    usersRepos.Insert(user);
                    return Ok("User registered successfully ...");
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }


            }
            return BadRequest();
        }


        [HttpGet("{id:int}")]
        public IActionResult getUser(int id)
        {
            try
            {
                Users user = usersRepos.GetById(id);
                if (user != null)
                    return Ok(user);
                return Ok($"user with id {id} not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        public IActionResult getAllUsers()
        {
            try
            {
                List<Users> users = usersRepos.GetAll();
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {

            try
            {
                this.usersRepos.Delete(id);
                return Ok("deleted successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] Users newUser)
        {
            if (ModelState.IsValid)
            {
                Users oldUser = usersRepos.GetById(id);
                oldUser.UserName = newUser.UserName;
                oldUser.PasswordHash = newUser.PasswordHash;
                oldUser.User_Address = newUser.User_Address;
                oldUser.PhoneNumber = newUser.PhoneNumber;
                oldUser.Is_Active = newUser.Is_Active;
                oldUser.Is_Admin = newUser.Is_Admin;
                return StatusCode(StatusCodes.Status204NoContent, "Data saved");
            }
            return BadRequest("Data Not Valid");
        }

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult Login([FromBody] string userName, [FromBody] string password)
        //{
        //    if (userName == null || password == null)
        //        return new BadRequestResult();
        //    else
        //    {
        //        Users user = usersRepos.Login(userName, password);
        //        if (user != null)
        //            return Ok();
        //        return Ok(204);
        //    }


        //}


    }
}
