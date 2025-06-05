namespace WebAPI_Oracle.Entity
{
    public class Empresa
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string CNPJ { get; set; }
        public string SetorAtividade { get; set; }

        public Empresa() {  }

        public Empresa(int id, string nomeEmpresa, string cnpj, string setorAtividade)
        {
            Id = id;
            NomeEmpresa = nomeEmpresa;
            CNPJ = cnpj;
            SetorAtividade = setorAtividade;
        }







    }
}
