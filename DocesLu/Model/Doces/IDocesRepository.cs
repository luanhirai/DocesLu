namespace DocesLu.Model.Doces
{
    public interface IDocesRepository
    {
        void Add(Doces doce);

        Doces GetById(int id);

        void Delete(Doces doce);

        List<Doces> GetAll();
    }
}
