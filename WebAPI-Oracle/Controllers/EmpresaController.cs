using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Oracle.Dto;
using WebAPI_Oracle.Entity;
using WebAPI_Oracle.Service;

namespace WebAPI_Oracle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;

        public EmpresaController(IEmpresaService empresaService, IMapper mapper)
        {
            _empresaService = empresaService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<EmpresaResponseDTO> GetEmpresaById(int id)
        {
            var empresa = _empresaService.GetEmpresaById(id);
            if (empresa == null)
                return NotFound("Empresa não encontrada!");

            var empresaDto = _mapper.Map<EmpresaResponseDTO>(empresa);
            return Ok(empresaDto);
        }

        [HttpGet]
        public ActionResult<List<EmpresaResponseDTO>> GetEmpresaList()
        {
            var empresas = _empresaService.GetAllEmpresa();
            var empresasDto = _mapper.Map<List<EmpresaResponseDTO>>(empresas);
            return Ok(empresasDto);
        }

        [HttpPost]
        public ActionResult<EmpresaResponseDTO> CreateNewEmpresa([FromBody] EmpresaRequestDTO empresaRequestDto)
        {
            if (empresaRequestDto == null)
                return BadRequest("Dados inválidos");

            var empresaEntity = _mapper.Map<Empresa>(empresaRequestDto);
            var novaEmpresa = _empresaService.CreateEmpresa(empresaEntity);

            var empresaResponseDto = _mapper.Map<EmpresaResponseDTO>(novaEmpresa);
            return CreatedAtAction(nameof(GetEmpresaById), new { id = empresaResponseDto.Id }, empresaResponseDto);
        }

        [HttpPut("{id}")]
        public ActionResult<EmpresaResponseDTO> UpdateEmpresa(int id, [FromBody] EmpresaRequestDTO empresaRequestDto)
        {
            var existingEmpresa = _empresaService.GetEmpresaById(id);
            if (existingEmpresa == null)
                return NotFound();

            _mapper.Map(empresaRequestDto, existingEmpresa);  // map updated fields onto existing entity
            var updatedEmpresa = _empresaService.UpdateEmpresa(id, existingEmpresa);

            var empresaResponseDto = _mapper.Map<EmpresaResponseDTO>(updatedEmpresa);
            return Ok(empresaResponseDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmpresa(int id)
        {
            var existingEmpresa = _empresaService.GetEmpresaById(id);
            if (existingEmpresa == null)
                return NotFound();

            _empresaService.DeleteEmpresa(id);
            return NoContent();
        }
    }
}
