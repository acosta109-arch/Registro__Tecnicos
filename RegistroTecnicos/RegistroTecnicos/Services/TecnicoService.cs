using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoService
{
    private readonly Contexto _contexto;

    public TecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.tecnicos.AnyAsync(t => t.TecnicosId == id);
    }

    public async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.tecnicos.Update(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicosId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        var Tecnicos = await _contexto.tecnicos
            .Where(t => t.TecnicosId == id).ExecuteDeleteAsync();
        return Tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _contexto.tecnicos.AsNoTracking().
            FirstOrDefaultAsync(t => t.TecnicosId == id);
    } 

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _contexto.tecnicos.AsNoTracking()
            .Where(criterio)
            .ToList();
    }
    public async Task<bool> ExisteTecnicoConNombre(string nombre)
    {
        return await _contexto.tecnicos.AnyAsync(t => t.Nombres.ToLower() == nombre.ToLower());
    }

}
