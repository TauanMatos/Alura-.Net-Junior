using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
  public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
  {
    private readonly ICategoriaRepository CategoriaRepository;
    public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
    {
      this.CategoriaRepository = categoriaRepository;
    }

    public IList<Produto> GetProdutos()
    {
      return dbSet.Include(p => p.Categoria).ToList();
    }

    public IList<Produto> GetProdutosByName(string Name)
    {
      return dbSet.Include(p => p.Categoria).Where(p => p.Nome.Contains(Name)).ToList();
    }

    public async Task SaveProdutos(List<Livro> livros)
    {
      foreach (var livro in livros)
      {
        await this.CategoriaRepository.AddItem(livro.Categoria);
        if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
        {
          var categoria = await this.contexto.Set<Categoria>().Where(c => c.Nome.Equals(livro.Categoria)).SingleOrDefaultAsync();
          dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
        }
      }
      await contexto.SaveChangesAsync();
    }
  }

  public class Livro
  {
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string Categoria { get; set; }
    public string Subcategoria { get; set; }
    public decimal Preco { get; set; }
  }
}
