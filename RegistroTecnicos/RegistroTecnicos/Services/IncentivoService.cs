using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class IncentivoService
{
    private readonly Contexto _contexto;

    public IncentivoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Incentivos.AnyAsync(t => t.IncentivoId == id);
    }

    private async Task<bool> Insertar(Incentivos incentivo)
    {
        _contexto.Incentivos.Add(incentivo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Incentivos incentivo)
    {
        _contexto.Incentivos.Update(incentivo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Incentivos incentivo)
    {
        if (!await Existe(incentivo.IncentivoId))
            return await Insertar(incentivo);
        else
            return await Modificar(incentivo);
    }

    public async Task<bool> Eliminar(int id)
    {
        var Incentivos = await _contexto.Incentivos
            .Where(t => t.IncentivoId == id).ExecuteDeleteAsync();
        return Incentivos > 0;
    }

    public async Task<Incentivos?> Buscar(int id)
    {
        return await _contexto.Incentivos.AsNoTracking().
            FirstOrDefaultAsync(t => t.IncentivoId == id);
    }

    public async Task<List<Incentivos>> Listar(Expression<Func<Incentivos, bool>> criterio)
    {
        return _contexto.Incentivos.AsNoTracking()
            .Where(criterio)
            .ToList();
    }


    public async Task<Incentivos>? BuscarDescripcion(string descripcion)
    {
        return await _contexto.Incentivos.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Descripcion == descripcion);
    }

}


