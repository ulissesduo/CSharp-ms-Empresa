using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Oracle.Entity;
using WebAPI_Oracle.Service;

namespace WebAPI_Oracle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet("{id}")]
        public ActionResult<Empresa> GetEmpresaById(int id) 
        {
            var empresa = _empresaService.GetEmpresaById(id);
            if (empresa == null) return NotFound("$Empresa não encontrada!");
            return empresa;
        }

        [HttpGet]
        public ActionResult<List<Empresa>> GetEmpresaList() 
        {
            return Ok(_empresaService.GetAllEmpresa());
        }

        [HttpPost]
        public ActionResult<Empresa> CreateNewEmpresa(Empresa empresa) 
        {

            var novaEmpresa = _empresaService.CreateEmpresa(empresa);
            return CreatedAtAction(nameof(GetEmpresaById), new { id = novaEmpresa.Id }, novaEmpresa);


        }

        [HttpPut("{id}")]
        public ActionResult<Empresa> UpdateEmpresa(int id, Empresa empresa)
        {
            var existingEmpresa = _empresaService.GetEmpresaById(id);
            if (existingEmpresa == null) return NotFound();

            var updatedEmpresa = _empresaService.UpdateEmpresa(id, empresa);

            return Ok(updatedEmpresa);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmpresa(int id) 
        {
            var existingEmpresa = _empresaService.GetEmpresaById(id);
            if (existingEmpresa == null) return NotFound();
            
            _empresaService.DeleteEmpresa(id);
            return NoContent();

        }
    }
}
