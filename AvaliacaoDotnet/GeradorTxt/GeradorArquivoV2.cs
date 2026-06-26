using System.Text;

namespace GeradorTxt
{
    public class GeradorArquivoV2 : GeradorArquivoBase
    {
        protected override void EscreverItens(StringBuilder sb, Documento doc)
        {
            foreach (var item in doc.Itens)
            {
                EscreverTipo02(sb, item);
                Contador.Registrar("02");

                if (item.Categorias != null)
                {
                    foreach (var categoria in item.Categorias)
                    {
                        EscreverTipo03(sb, categoria);
                        Contador.Registrar("03");
                    }
                }
            }
        }

        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            sb.Append("02").Append("|")
              .Append(item.NumeroItem).Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }

        protected virtual void EscreverTipo03(StringBuilder sb, CategoriaItem categoria)
        {
            sb.Append("03").Append("|")
              .Append(categoria.NumeroCategoria).Append("|")
              .Append(categoria.DescricaoCategoria).AppendLine();
        }
    }
}