using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Oracle.Controllers;
using WebAPI_Oracle.Dto;
using WebAPI_Oracle.Entity;
using WebAPI_Oracle.Service;

namespace msEmpresa.Tests.Controller
{
    public class EmpresaControllerTests
    {
        private readonly Mock<IEmpresaService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EmpresaController _controller;

        public EmpresaControllerTests()
        {
            _mockService = new Mock<IEmpresaService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new EmpresaController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetEmpresaList_ReturnsAllEmpresas()
        {
            var empresas = new List<Empresa>
            {
                new Empresa { Id = 1, NomeEmpresa = "Empresa A", CNPJ = "123", SetorAtividade = "Setor A" },
                new Empresa { Id = 2, NomeEmpresa = "Empresa B", CNPJ = "456", SetorAtividade = "Setor B" }
            };

            var empresasDto = new List<EmpresaResponseDTO>
            {
                new EmpresaResponseDTO { Id = 1, NomeEmpresa = "Empresa A", CNPJ = "123", SetorAtividade = "Setor A" },
                new EmpresaResponseDTO { Id = 2, NomeEmpresa = "Empresa B", CNPJ = "456", SetorAtividade = "Setor B" }
            };

            _mockService.Setup(s => s.GetAllEmpresa()).Returns(empresas);
            _mockMapper.Setup(m => m.Map<List<EmpresaResponseDTO>>(empresas)).Returns(empresasDto);

            var result = _controller.GetEmpresaList();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnList = Assert.IsType<List<EmpresaResponseDTO>>(okResult.Value);
            Assert.Equal(2, returnList.Count);
        }

        [Fact]
        public void CreateNewEmpresa_ReturnsCreatedEmpresa()
        {
            var requestDto = new EmpresaRequestDTO
            {
                NomeEmpresa = "Nova Empresa",
                CNPJ = "789",
                SetorAtividade = "Setor C"
            };

            var empresaEntity = new Empresa
            {
                NomeEmpresa = "Nova Empresa",
                CNPJ = "789",
                SetorAtividade = "Setor C"
            };

            var empresaCreated = new Empresa
            {
                Id = 1,
                NomeEmpresa = "Nova Empresa",
                CNPJ = "789",
                SetorAtividade = "Setor C"
            };

            var responseDto = new EmpresaResponseDTO
            {
                Id = 1,
                NomeEmpresa = "Nova Empresa",
                CNPJ = "789",
                SetorAtividade = "Setor C"
            };

            _mockMapper.Setup(m => m.Map<Empresa>(requestDto)).Returns(empresaEntity);
            _mockService.Setup(s => s.CreateEmpresa(empresaEntity)).Returns(empresaCreated);
            _mockMapper.Setup(m => m.Map<EmpresaResponseDTO>(empresaCreated)).Returns(responseDto);

            var result = _controller.CreateNewEmpresa(requestDto);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<EmpresaResponseDTO>(createdAtResult.Value);
            Assert.Equal(responseDto.Id, returnValue.Id);
            Assert.Equal(responseDto.NomeEmpresa, returnValue.NomeEmpresa);
        }

        [Fact]
        public void UpdateEmpresa_ReturnsUpdatedEmpresa_WhenExists()
        {
            var empresaEntity = new Empresa
            {
                Id = 1,
                NomeEmpresa = "Empresa Original",
                CNPJ = "123",
                SetorAtividade = "Setor A"
            };

            var requestDto = new EmpresaRequestDTO
            {
                NomeEmpresa = "Empresa Atualizada",
                CNPJ = "123",
                SetorAtividade = "Setor A"
            };

            var updatedEmpresaEntity = new Empresa
            {
                Id = 1,
                NomeEmpresa = "Empresa Atualizada",
                CNPJ = "123",
                SetorAtividade = "Setor A"
            };

            var responseDto = new EmpresaResponseDTO
            {
                Id = 1,
                NomeEmpresa = "Empresa Atualizada",
                CNPJ = "123",
                SetorAtividade = "Setor A"
            };

            _mockService.Setup(s => s.GetEmpresaById(1)).Returns(empresaEntity);
            _mockMapper.Setup(m => m.Map(requestDto, empresaEntity)).Callback<EmpresaRequestDTO, Empresa>((src, dest) =>
            {
                dest.NomeEmpresa = src.NomeEmpresa;
                dest.CNPJ = src.CNPJ;
                dest.SetorAtividade = src.SetorAtividade;
            });

            _mockService.Setup(s => s.UpdateEmpresa(1, empresaEntity)).Returns(updatedEmpresaEntity);
            _mockMapper.Setup(m => m.Map<EmpresaResponseDTO>(updatedEmpresaEntity)).Returns(responseDto);

            var result = _controller.UpdateEmpresa(1, requestDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmpresaResponseDTO>(okResult.Value);
            Assert.Equal("Empresa Atualizada", returnValue.NomeEmpresa);
        }

        [Fact]
        public void UpdateEmpresa_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.GetEmpresaById(1)).Returns((Empresa)null!);

            var result = _controller.UpdateEmpresa(1, new EmpresaRequestDTO());

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
        public void DeleteEmpresa_ReturnsNoContent_WhenExists()
        {
            var empresa = new Empresa { Id = 1 };

            _mockService.Setup(s => s.GetEmpresaById(1)).Returns(empresa);

            var result = _controller.DeleteEmpresa(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
