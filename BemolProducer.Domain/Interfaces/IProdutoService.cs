using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemolProducer.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync(int skip = 0, int limit = 50);
        Task CreateAsync(Produto obj);
    }
}
