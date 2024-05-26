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
        return await _contexto.Tecnicos.AnyAsync(t => t.TecnicoId == id);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Update(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        var Tecnicos = await _contexto.Tecnicos
            .Where(t => t.TecnicoId == id).ExecuteDeleteAsync();
        return Tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _contexto.Tecnicos.AsNoTracking().
            FirstOrDefaultAsync(t => t.TecnicoId == id);
    } 

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _contexto.Tecnicos.AsNoTracking()
            .Where(criterio)
            .ToList();
    }

    public async Task<List<TiposTecnicos>> ListarTiposTecnicos()
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .Select(t => t.TipoTecnico) 
            .Distinct() 
            .ToListAsync();
    }


    public async Task<Tecnicos?> BuscarNombres(string nombre)
    {
        return await _contexto.Tecnicos.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Nombres == nombre);
    }

}
