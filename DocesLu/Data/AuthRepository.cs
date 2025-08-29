using DocesLu.Model.User;

namespace DocesLu.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ConnectionContext _context;

        public AuthRepository(ConnectionContext context)
        {
            _context = context;
        }

        public Auth GetByUsername(string username)
        {
            return _context.Auths.FirstOrDefault(u => u.UserName == username);
        }

        public bool Exists(string username)
        {
            return _context.Auths.Any(u => u.UserName == username);
        }

        public void Add(Auth user)
        {
            _context.Auths.Add(user);
            _context.SaveChanges();
        }
    }
}
