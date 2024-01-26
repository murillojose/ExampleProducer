

using BemolProducer.Domain;
using BemolProducer.Domain.Interfaces;

namespace BemolProducer.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync(int skip = 0, int limit = 50)
        {
            return await _produtoRepository.GetAllAsync(skip, limit);
        }

        public async Task CreateAsync(Produto obj)
        {
            await _produtoRepository.SaveAsync(obj);
        }
    }
}
