using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Oracle.Entity;
using WebAPI_Oracle.Repository;
using WebAPI_Oracle.Service;

namespace msEmpresa.Tests.Service
{
    public class EmpresaServiceTests
    {
        [Fact]
        public async Task GetAllEmpresa_ReturnListAllEmpresas() 
        {
            var mockRepo = new Mock<IEmpresaRepository>();
            mockRepo.Setup(repo => repo.listaEmpresas()).Returns(new List<Empresa>
            {
                new Empresa { Id = 1, CNPJ="12345", NomeEmpresa="Company", SetorAtividade="Activity Department"},
                new Empresa { Id = 2, CNPJ="46789", NomeEmpresa="Company2", SetorAtividade="Activity Department2"}

            });
            var service = new EmpresaService(mockRepo.Object);
            var result = service.GetAllEmpresa();   

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetById_ReturnsEmpresaById() 
        {
            var mockRepo = new Mock<IEmpresaRepository>();
            var id = 2;
            mockRepo.Setup(repo => repo.buscaEmpresa(id)).Returns(new Empresa { Id = id });
            var service = new EmpresaService(mockRepo.Object);
            var result = service.GetEmpresaById(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task CreateEmpresa_ReturnsCreatedSuccessfully() 
        {
            var mockRepo = new Mock<IEmpresaRepository>();
            var newEmpresa = new Empresa { Id = 1, CNPJ = "1234354", NomeEmpresa = "Company.ltda", SetorAtividade = "Activity"};
            mockRepo.Setup(repo=>repo.criarEmpresa(newEmpresa)).Returns(newEmpresa);

            var service = new EmpresaService(mockRepo.Object);
            var result = service.CreateEmpresa(newEmpresa);

            Assert.NotNull(result);
            Assert.Equal("Company.ltda", result.NomeEmpresa);
            Assert.Equal("Activity", result.SetorAtividade);

        }

        [Fact]
        public async Task UpdateEmpresa_ReturnUpdatedSuccessfully() 
        {
            var mockRepo = new Mock<IEmpresaRepository>();
            var id = 2;
            var updatedEmpresa = new Empresa { Id = 2, CNPJ = "1234354", NomeEmpresa = "Company.ltda", SetorAtividade = "Activity" };
            mockRepo.Setup(repo =>repo.buscaEmpresa(id)).Returns(updatedEmpresa);
            mockRepo.Setup(repo => repo.atualizarEmpresa(id, updatedEmpresa)).Returns(updatedEmpresa);

            var service = new EmpresaService(mockRepo.Object);
            var result = service.UpdateEmpresa(id,updatedEmpresa);
            Assert.NotNull(result);
            Assert.Equal("Company.ltda", result.NomeEmpresa);
            Assert.Equal(2, result.Id);

        }

        [Fact]
        public async Task DeleteEmpresa_ReturnDeletedSuccessfully() 
        {
            // Arrange
            var mockRepo = new Mock<IEmpresaRepository>();
            var id = 1;

            var existingEmpresa = new Empresa { Id = id, NomeEmpresa = "Empresa Teste" };

            mockRepo.Setup(r => r.buscaEmpresa(id)).Returns(existingEmpresa);
            mockRepo.Setup(r => r.deleteEmpresa(id)); 

            var service = new EmpresaService(mockRepo.Object);

            // Act
            service.DeleteEmpresa(id);

            // Assert
            mockRepo.Verify(r => r.buscaEmpresa(id), Times.Once);
            mockRepo.Verify(r => r.deleteEmpresa(id), Times.Once);

        }


    }
}
