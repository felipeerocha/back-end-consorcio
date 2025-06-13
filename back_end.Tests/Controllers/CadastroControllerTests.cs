using back_end.API.Controllers;
using back_end.Domain.Entities;
using back_end.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Tests.Controllers
{
    public class CadastroControllerTests
    {
        private readonly AppDbContext _context;
        private readonly CadastroController _controller;

        public CadastroControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new CadastroController(_context);

            Seed();
        }

        private void Seed()
        {
            _context.Cotas.Add(new Cotas { Id = 1, NumeroCota = 100, Tipo = "Imóvel", Valor = 50000, Status = "Disponível" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task CreateCadastro_Retorna_CreatedAtActionResult()
        {
            var novoCadastro = new Cadastro
            {
                CotaId = 1,
                NomeUsuario = "João",
                Contato = "joao@email.com",
                Parcelamento = "12x"
            };

            var result = await _controller.CreateCadastro(novoCadastro);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var cadastro = Assert.IsType<Cadastro>(actionResult.Value);
            Assert.Equal("João", cadastro.NomeUsuario);
        }

        [Fact]
        public async Task GetCadastro_Retorna_Cadastro_Existente()
        {
            var cadastro = new Cadastro
            {
                CotaId = 1,
                NomeUsuario = "Maria",
                Contato = "maria@email.com",
                Parcelamento = "10x",
                NumeroCota = 100,
                Tipo = "Imóvel",
                Valor = 50000,
                DataCadastro = DateTime.Now
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            var result = await _controller.GetCadastro(cadastro.Id);

            var okResult = Assert.IsType<ActionResult<Cadastro>>(result);
            Assert.Equal("Maria", okResult.Value.NomeUsuario);
        }

        [Fact]
        public async Task GetCadastro_Retorna_NotFound_Se_Nao_Encontrar()
        {
            var result = await _controller.GetCadastro(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateCadastro_Retorna_BadRequest_Se_CotaInvalida()
        {
            var cadastro = new Cadastro
            {
                CotaId = 999,
                NomeUsuario = "Fulano",
                Contato = "fulano@email.com",
                Parcelamento = "5x"
            };

            var result = await _controller.CreateCadastro(cadastro);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Cota não encontrada.", badRequest.Value);
        }

        [Fact]
        public async Task UpdateCadastro_Retorna_Ok_Com_CadastroAtualizado_Se_Sucesso()
        {

            var cadastro = new Cadastro
            {
                CotaId = 1,
                NomeUsuario = "Carlos",
                Contato = "carlos@email.com",
                Parcelamento = "6x"
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            var cadastroAtualizado = new Cadastro
            {
                NomeUsuario = "Carlos Ricardo",
                Contato = "carlosnovo@email.com"
            };

            var result = await _controller.UpdateCadastro(cadastro.Id, cadastroAtualizado);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedCadastro = Assert.IsType<Cadastro>(okResult.Value);
            Assert.Equal("Carlos Ricardo", updatedCadastro.NomeUsuario);
            Assert.Equal("carlosnovo@email.com", updatedCadastro.Contato);
        }

        [Fact]
        public async Task UpdateCadastro_Retorna_NotFound_Se_Id_Diferente()
        {

            var cadastroParaAtualizar = new Cadastro
            {
                Id = 1,
                CotaId = 1,
                NomeUsuario = "Carlos",
                Contato = "carlos@email.com",
                Parcelamento = "6x"
            };


            var result = await _controller.UpdateCadastro(999, cadastroParaAtualizar);


            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCadastro_Retorna_NoContent_Se_Sucesso()
        {
            var cadastro = new Cadastro
            {
                CotaId = 1,
                NomeUsuario = "Lucas",
                Contato = "lucas@email.com",
                Parcelamento = "8x"
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteCadastro(cadastro.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCadastro_Retorna_NotFound_Se_Cadastro_Inexistente()

        {
            var result = await _controller.DeleteCadastro(1);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
