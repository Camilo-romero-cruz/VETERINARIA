using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using VETERINARIA_DB.Models;
namespace VETERINARIA_DB.Clases
{
    public class ClasesCliente
    {



            private readonly VeterinariaDbContext _context;

            public ClasesCliente(VeterinariaDbContext context)
            {
                _context = context;
            }

            public string Guardar(Cliente dato)
            {
                try
                {
                    _context.Clientes.Add(dato);
                    _context.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }

            public string EliminarCliente(int id)
            {
                try
                {
                    var Cliente = _context.Clientes.Find(id);

                    if (Cliente == null)
                        return "La Cliente no existe";

                    _context.Clientes.Remove(Cliente);
                    _context.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }

            public Cliente MostrarClientePorId(int id)
            {
                try
                {
                    return _context.Clientes.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public List<Cliente> MostrarClientes()
            {
                try
                {
                    return _context.Clientes.ToList();
                }
                catch (Exception)
                {
                    return new List<Cliente>();
                }
            }
            public string ActualizarCliente(Cliente dato)
            {
                try
                {
                    _context.Clientes.Update(dato);
                    _context.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }


        }
    }

