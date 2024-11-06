
using Data.Request;
using Data.Response;
using Data.Services;

namespace Data.Services
{
    public interface IClienteServices
    {
        Task<Result<List<ClienteResponse>>> Consultar(string filtro);
        Task<Result> Crear(ClienteRequest request);
        Task<Result> Eliminar(ClienteRequest request);
        Task<Result> Modificar(ClienteRequest request);
        Task CrearCliente();
    }
}