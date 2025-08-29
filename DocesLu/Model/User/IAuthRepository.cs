namespace DocesLu.Model.User
{
    public interface IAuthRepository
    {
        bool Exists(string username);
        void Add(Auth user);
        Auth GetByUsername(string username);
    }
}
