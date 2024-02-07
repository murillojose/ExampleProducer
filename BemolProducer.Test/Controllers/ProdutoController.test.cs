using AutoFixture;
using AutoMapper;
using BemolProducer.Controllers;
using BemolProducer.Domain;
using BemolProducer.Domain.DTOs;
using BemolProducer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Transaction;
using Microsoft.Azure.ServiceBus;
using Moq;
using NUnit.Framework;
using Queue.Interface;

namespace BemolProducer.Test.Controllers
{
    public class ProdutoControllerTest
    {
        private ProdutosController _produtosController;
        private Mock<IProdutoService> _produtoService;
        private Mock<IQueueClientWrapper> _queueClient;
        private Mock<IMapper> _mapper;
        private Fixture _fixture;
        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>();
            _queueClient = new Mock<IQueueClientWrapper>();
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

        [Test]
        public async Task PostProduto_ReturnsCreatedProduct()
        {
            // Arrange
            var produto = _fixture.Create<Produto>();
            var produtoDTO = _fixture.Create<ProdutoDTO>();
            _mapper.Setup(mapper => mapper.Map<ProdutoDTO>(produto)).Returns(produtoDTO);

            // Act
            var result = await _produtosController.PostProduto(produto);

            // Assert
            var createdResult = result as OkObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);

            var createdProduto = createdResult.Value as Produto;
            Assert.IsNotNull(createdProduto);
            Assert.AreEqual(produto.Id, createdProduto.Id);
            Assert.AreEqual(produto.Nome, createdProduto.Nome);

            _produtoService.Verify(service => service.CreateAsync(produto), Times.Once);
            _queueClient.Verify(queue => queue.SendAsync(It.IsAny<Message>()), Times.Once);
        }
    }
}
