using DocesLu.Model.Doces;

namespace DocesLu.Data
{
    public class DocesRepository : IDocesRepository
    {
        private readonly ConnectionContext _context;

        public DocesRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Doces doce)
        {
            _context.Doces.Add(doce);
            _context.SaveChanges();
        }

        public Doces GetById(int id)
        {
            var doce = _context.Doces.Find(id);
            if (doce == null)
                throw new Exception($"Doce com ID {id} não encontrado.");
            return doce;
        }

        public void Delete(Doces doce)
        {
            _context.Doces.Remove(doce);
            _context.SaveChanges();
        }

        public List<Doces> GetAll()
        {
            return _context.Doces.ToList();
        }
    }
}
