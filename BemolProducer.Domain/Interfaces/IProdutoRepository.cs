using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemolProducer.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task SaveAsync(Produto entity);
        Task UpdateAsync(Produto entity);
        Task<IEnumerable<Produto>> GetAllAsync(int skip = 0, int limit = 50);
        Task<Produto> GetByIdAsync(ObjectId id);
        Task DeleteAsync(ObjectId id);
    }
}
