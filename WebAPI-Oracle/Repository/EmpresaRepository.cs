using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI_Oracle.Data;
using WebAPI_Oracle.Entity;

namespace WebAPI_Oracle.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DataContext _context;

        public EmpresaRepository(DataContext context)
        {
            _context = context;
        }
        public List<Empresa> listaEmpresas()
        {
            return _context.Empresa.AsNoTracking().ToList();
        }

        public Empresa atualizarEmpresa(int id, Empresa empresa)
        {
            var existingEmpresa = _context.Empresa.Find(id);
            if (existingEmpresa == null) return null;

            existingEmpresa.NomeEmpresa = empresa.NomeEmpresa;
            existingEmpresa.SetorAtividade = empresa.SetorAtividade;
            existingEmpresa.CNPJ = empresa.CNPJ;

            _context.SaveChanges();
            return existingEmpresa;
        }

        public Empresa buscaEmpresa(int id)
        {
            return _context.Empresa.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Empresa criarEmpresa(Empresa empresa)
        {
            _context.Empresa.Add(empresa);
            _context.SaveChanges();
            return empresa;
        }

        public void deleteEmpresa(int id)
        {
            var empresa = _context.Empresa.Find(id);
            if (empresa == null) 
            {
                return;
            }
            _context.Empresa.Remove(empresa);
            _context.SaveChanges();
        }
    }
}
