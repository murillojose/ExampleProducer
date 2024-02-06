namespace BemolProducer.Domain
{
    public class ProdutoDatabaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? ProdutoCollectionName { get; set; }
        public string? UserCollectionName { get; set; }
    }
}
