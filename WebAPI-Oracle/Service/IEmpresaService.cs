using WebAPI_Oracle.Entity;

namespace WebAPI_Oracle.Service
{
    public interface IEmpresaService
    {
        List<Empresa> GetAllEmpresa();
        Empresa GetEmpresaById(int id);
        Empresa CreateEmpresa(Empresa product);
        Empresa UpdateEmpresa(int id, Empresa product);
        void DeleteEmpresa(int id);

    }
}
