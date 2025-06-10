using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Oracle.Controllers;
using WebAPI_Oracle.Entity;
using WebAPI_Oracle.Service;

namespace msEmpresa.Tests.Controller
{
    public class EmpresaControllerTests
    {
        private readonly Mock<IEmpresaService> _mockService;
        private readonly EmpresaController _controller;

        public EmpresaControllerTests()
        {
            _mockService = new Mock<IEmpresaService>();
            _controller = new EmpresaController(_mockService.Object);
        }

        [Fact]
        public void GetEmpresaList_ReturnsAllEmpresas()
        {
            var empresas = new List<Empresa>
            {
                new Empresa { Id = 1, NomeEmpresa = "Empresa A" },
                new Empresa { Id = 2, NomeEmpresa = "Empresa B" }
            };

            _mockService.Setup(s => s.GetAllEmpresa()).Returns(empresas);

            var result = _controller.GetEmpresaList();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnList = Assert.IsType<List<Empresa>>(okResult.Value);
            Assert.Equal(2, returnList.Count);
        }
        [Fact]
        public void CreateNewEmpresa_ReturnsCreatedEmpresa()
        {
            var empresa = new Empresa { Id = 1, NomeEmpresa = "Nova Empresa" };
            _mockService.Setup(s => s.CreateEmpresa(It.IsAny<Empresa>())).Returns(empresa);

            var result = _controller.CreateNewEmpresa(empresa);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Empresa>(createdAtResult.Value);
            Assert.Equal(empresa.Id, returnValue.Id);
        }

        [Fact]
        public void UpdateEmpresa_ReturnsUpdatedEmpresa_WhenExists()
        {
            var empresa = new Empresa { Id = 1, NomeEmpresa = "Atualizada" };

            _mockService.Setup(s => s.GetEmpresaById(1)).Returns(empresa);
            _mockService.Setup(s => s.UpdateEmpresa(1, empresa)).Returns(empresa);

            var result = _controller.UpdateEmpresa(1, empresa);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Empresa>(okResult.Value);
            Assert.Equal("Atualizada", returnValue.NomeEmpresa);
        }

        [Fact]
        public void UpdateEmpresa_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.GetEmpresaById(1)).Returns((Empresa)null!);

            var result = _controller.UpdateEmpresa(1, new Empresa());

            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public void DeleteEmpresa_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.GetEmpresaById(1)).Returns((Empresa)null!);

            var result = _controller.DeleteEmpresa(1);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

        }
        [Fact]
        public void DeleteEmpresa_ReturnsNotFound_WhenExists()
        {
            var empresa = new Empresa { Id = 1 };

            _mockService.Setup(s => s.GetEmpresaById(1)).Returns(empresa);

            var result = _controller.DeleteEmpresa(1);

            Assert.IsType<NoContentResult>(result);
           

        }
    }
}
