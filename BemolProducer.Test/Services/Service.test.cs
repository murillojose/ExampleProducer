using AutoFixture;
using BemolProducer.Domain;
using BemolProducer.Domain.Interfaces;
using BemolProducer.Service;
using Moq;
using NUnit.Framework;

namespace BemolProducer.Test
{
    public class ServiceTest
    {
        private Fixture _fixture;
        private Mock<IProdutoRepository> _mockedRepository;
        private ProdutoService _produtoService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockedRepository = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_mockedRepository.Object);
        }

        [Test]
        public async Task GetAllProducts_Should_Be_Success()
        {
            IEnumerable<Produto> produtos = _fixture.Create<IEnumerable<Produto>>();
            _mockedRepository.Setup(r => r.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(produtos);

            var result =  await _produtoService.GetAllAsync(0,0);
            
            Assert.IsNotNull(result);

            Assert.IsTrue(produtos.SequenceEqual(result));
        }

        [Test]
        public async Task CreateAsync_Should_Be_Success()
        {
            Produto produto = _fixture.Create<Produto>();

            await _produtoService.CreateAsync(produto);

            _mockedRepository.Verify(r => r.SaveAsync(produto), Times.Once);
        }

    }
}