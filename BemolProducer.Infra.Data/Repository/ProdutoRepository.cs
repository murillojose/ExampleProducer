using BemolProducer.Domain;
using BemolProducer.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BemolProducer.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MongoContext _mongoContext;
        private readonly IOptions<ProdutoDatabaseSettings> _produtoServices;

        public ProdutoRepository(IOptions<ProdutoDatabaseSettings> produtoServices)
        {
            _produtoServices = produtoServices;
            _mongoContext = new MongoContext(_produtoServices);
        }

        public async Task SaveAsync(Produto entity)
        {
            await _mongoContext.Produto.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Produto entity)
        {
            await _mongoContext.Produto.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync(int skip = 0, int limit = 50)
        {
            return await _mongoContext.Produto.Find(x => true).Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(ObjectId id)
        {
            return await _mongoContext.Produto.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _mongoContext.Produto.DeleteOneAsync(x => x.Id.Equals(id));
        }
    }
}
