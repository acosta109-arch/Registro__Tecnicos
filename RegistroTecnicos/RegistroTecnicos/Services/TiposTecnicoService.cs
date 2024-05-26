using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TiposTecnicoService
{
    private readonly Contexto _contexto;

    public TiposTecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.TiposTecnicos.AnyAsync(t => t.TipoId == id);
    }

    private async Task<bool> Insertar(TiposTecnicos tecnico)
    {
        _contexto.TiposTecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tecnico)
    {
        _contexto.TiposTecnicos.Update(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(TiposTecnicos tecnico)
    {
        if (!await Existe(tecnico.TipoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        var Tipo = await _contexto.TiposTecnicos
            .Where(t => t.TipoId == id).ExecuteDeleteAsync();
        return Tipo > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _contexto.TiposTecnicos.AsNoTracking().
            FirstOrDefaultAsync(t => t.TipoId == id);
    } 

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return _contexto.TiposTecnicos.AsNoTracking()
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

    public async Task<bool> ExisteTipoTecnicoDescripcion(string descripcion)
    {
        return await _contexto.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);
    }


}
