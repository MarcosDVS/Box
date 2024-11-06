

using Data.Context;
using Data.Model;
using Data.Request;
using Data.Response;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Services
{

    public class ClienteServices : IClienteServices
    {
        private readonly IMyDbContext dbContext;

        public ClienteServices(IMyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Método crear
        public async Task<Result> Crear(ClienteRequest request)
        {
            try
            {
                var cliente = Cliente.Crear(request);
                dbContext.Clientes.Add(cliente);
                await dbContext.SaveChangesAsync();
                return new Result() { Message = "ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Modificar
        public async Task<Result> Modificar(ClienteRequest request)
        {
            try
            {
                var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (cliente == null)
                    return new Result() { Message = "No se encontró el cliente", Success = false };

                if (cliente.Modificar(request))
                    await dbContext.SaveChangesAsync();

                return new Result() { Message = "Ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Eliminar
        public async Task<Result> Eliminar(ClienteRequest request)
        {
            try
            {
                var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (cliente == null)
                    return new Result() { Message = "No se encontró el cliente", Success = false };

                dbContext.Clientes.Remove(cliente);
                await dbContext.SaveChangesAsync();

                return new Result() { Message = "Ok", Success = true };
            }
            catch (Exception e)
            {
                return new Result() { Message = e.Message, Success = false };
            }
        }

        // Método Listar o consultar
        public async Task<Result<List<ClienteResponse>>> Consultar(string filtro)
        {
            try
            {
                var clientes = await dbContext.Clientes
                    //.Include(c => c.Cashier)
                    .Where(c =>
                        (c.Nombre + " " + c.Apellidos + " " + c.Direccion + " " + c.Telefono + " " + c.Cedula + " " + c.Correo)
                        .ToLower()
                        .Contains(filtro.ToLower())
                    )
                    .Select(c => c.ToResponse())
                    .ToListAsync();

                return new Result<List<ClienteResponse>>()
                {
                    Message = "Ok",
                    Success = true,
                    Data = clientes
                };
            }
            catch (Exception e)
            {
                return new Result<List<ClienteResponse>>()
                {
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task CrearCliente()
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(u => u.Nombre == "Cliente");

            if (cliente == null)
            {
                cliente = new Cliente
                {
                    Nombre = "Cliente",
                    Apellidos = "Frecuente",
                    Cedula = "00000000000",
                    Direccion = "Desconocida",
                    Telefono = "Desconocido",
                    Correo = "Desconocido" // Asigna un valor para el límite de crédito según tus requisitos
                };

                dbContext.Clientes.Add(cliente);
                await dbContext.SaveChangesAsync();
            }
        }

      
    }
}
