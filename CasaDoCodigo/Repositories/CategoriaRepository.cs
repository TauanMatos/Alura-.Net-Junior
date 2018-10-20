using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
  public interface ICategoriaRepository
  {
    Task AddItem(string Nome);
  }
  public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
  {
    public CategoriaRepository(ApplicationContext contexto) : base(contexto)
    {
    }

    public async Task AddItem(string Nome)
    {
      var categoria = await this.dbSet.Where(c => c.Nome.Equals(Nome)).SingleOrDefaultAsync();

      if(categoria == null)
      {
        categoria = new Categoria(Nome);
        await this.dbSet.AddAsync(categoria);
        await this.contexto.SaveChangesAsync();
      }
    }

  }
}
