using AutoMapper;
using BemolProducer.Domain;
using BemolProducer.Domain.DTOs;
using BemolProducer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace BemolProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly QueueClient _queueClient;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService, QueueClient queueClient, IMapper mapper)
        {
            _produtoService = produtoService;
            _queueClient = queueClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _produtoService.GetAllAsync();
        }

        [HttpPost]
        public async Task<Produto> PostProduto(Produto produto)
        {
            //Cria o produto na base
            if (produto != null)
            {
               await _produtoService.CreateAsync(produto);
            }    

            //Mapeando produto para DTO
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            // Convert a mensagem em JSON
            string produtoJson = JsonConvert.SerializeObject(produtoDTO);

            // Envia a mensagem para a fila
            var message = new Message(System.Text.Encoding.UTF8.GetBytes(produtoJson));           
            await _queueClient.SendAsync(message);

            return produto;            
        }
    }
}
