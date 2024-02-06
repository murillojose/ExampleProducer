using BemolProducer.Domain;
using BemolProducer.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BemolProducer.Infra.Data
{
    public class MongoContext
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public MongoContext(IOptions<ProdutoDatabaseSettings> produtoServices)
        {            
            mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
            database = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);
        }

        public IMongoCollection<Produto> Produto
        {
            get
            {
                return database.GetCollection<Produto>("Produtos");
            }
        }
    }
}
