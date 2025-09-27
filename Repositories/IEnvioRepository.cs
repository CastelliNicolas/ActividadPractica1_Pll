using ModeloParcialApi.Models;

namespace ModeloParcialApi.Repositories
{
    public interface IEnvioRepository
    {
        List<Envio>GetAll(string dir, string? estado);
        bool Delete(int Id);
    }
}