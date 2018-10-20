using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
  public class BuscaDeProdutosViewModel
  {
    public IList<Produto> Produtos;

    public string Pesquisa;

    public BuscaDeProdutosViewModel(IList<Produto> produtos)
    {
      this.Produtos = produtos;
    }
  }
}
