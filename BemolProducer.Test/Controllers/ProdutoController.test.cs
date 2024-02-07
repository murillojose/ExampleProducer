using AutoFixture;
using AutoMapper;
using BemolProducer.Controllers;
using BemolProducer.Domain;
using BemolProducer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Moq;
using NUnit.Framework;

namespace BemolProducer.Test.Controllers
{
    public class ProdutoControllerTest
    {
        private ProdutosController _produtosController;
        private Mock<IProdutoService> _produtoService;
        private Mock<QueueClient> _queueClient;
        private Mock<IMapper> _mapper;
        private Fixture _fixture;
        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>();
            _queueClient = new Mock<QueueClient>("connectionString", "queueName");
            _produtoService = new Mock<IProdutoService>();
            _produtosController = new ProdutosController(_produtoService.Object, _queueClient.Object, _mapper.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetProdutos_ShouldBeSucces()
        {
            var expectedProdutos = _fixture.Create<IEnumerable<Produto>>();

            _produtoService.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedProdutos);

            var result = await _produtosController.GetProdutos();

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        
    }
}
