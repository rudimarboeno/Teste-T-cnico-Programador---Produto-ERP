using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeradorTxt
{
    public class GeradorArquivoV2 : GeradorArquivoBase
    {
        public override void Gerar(List<Empresa> empresas, string outputPath)
        {
            var sb = new StringBuilder();
            Contador = new ContadorLinhas();

            foreach (var emp in empresas)
            {
                EscreverTipo00(sb, emp);
                Contador.Registrar("00");

                foreach (var doc in emp.Documentos)
                {
                    var somaItens = doc.Itens.Sum(i => i.Valor);

                    if (somaItens != doc.Valor)
                        throw new Exception($"O documento {doc.Numero} possui valor inválido.");

                    EscreverTipo01(sb, doc);
                    Contador.Registrar("01");

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
            }

            Contador.EscreverResumo(sb);

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            sb.Append("02").Append("|")
              .Append(item.NumeroItem).Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }

        protected void EscreverTipo03(StringBuilder sb, CategoriaItem categoria)
        {
            sb.Append("03").Append("|")
              .Append(categoria.NumeroCategoria).Append("|")
              .Append(categoria.DescricaoCategoria).AppendLine();
        }
    }
}