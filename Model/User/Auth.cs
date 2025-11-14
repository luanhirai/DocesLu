using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocesLu.Model.User
{
    [Table("Auth")]
    public class Auth
    {
        [Key]
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; private set; } = true;

        protected Auth() { }

        public Auth(string username, string password, bool isAdmin = true)
        {
            UserName = username;
            Password = password;
            IsAdmin = isAdmin;
        }
    }

}
