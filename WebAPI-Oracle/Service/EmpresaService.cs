using WebAPI_Oracle.Entity;
using WebAPI_Oracle.Repository;

namespace WebAPI_Oracle.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repository;
        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        public List<Empresa> GetAllEmpresa()
        {
            return _repository.listaEmpresas();
        }

        public Empresa GetEmpresaById(int id)
        {
            return _repository.buscaEmpresa(id);
        }


        public Empresa CreateEmpresa(Empresa product)
        {

            return _repository.criarEmpresa(product);
        }

        public void DeleteEmpresa(int id)
        {
            var empresa = _repository.buscaEmpresa(id);
            if (empresa == null) return;
            _repository.deleteEmpresa(empresa.Id);

            throw new NotImplementedException();
        }

        
        public Empresa UpdateEmpresa(int id, Empresa empresa)
        {
            var existingEmpresa = _repository.buscaEmpresa(id);
            if (empresa == null) return null;

            return _repository.atualizarEmpresa(id, empresa);
        }
    }
}
