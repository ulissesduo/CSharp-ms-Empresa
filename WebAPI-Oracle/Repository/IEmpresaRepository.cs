using System.Runtime.InteropServices;
using WebAPI_Oracle.Entity;

namespace WebAPI_Oracle.Repository
{
    public interface IEmpresaRepository
    {
        List<Empresa> listaEmpresas();
        Empresa buscaEmpresa(int id);
        Empresa criarEmpresa(Empresa empresa);
        Empresa atualizarEmpresa(int id, Empresa empresa);
        void deleteEmpresa(int id);
    }
}
