using BemolProducer.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BemolProducer.Domain
{
    public class Produto : BaseEntity
    {
        [BsonElement("Nome")]
        public string? Nome { get; set; }
    }
}
