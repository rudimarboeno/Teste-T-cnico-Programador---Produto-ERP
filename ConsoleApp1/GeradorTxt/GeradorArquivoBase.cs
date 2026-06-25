using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;

namespace GeradorTxt
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase
    {
        protected ContadorLinhas Contador;

        public virtual void Gerar(List<Empresa> empresas, string outputPath)
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
                    {
                        throw new Exception($"O documento {doc.Numero} possui valor inválido.");
                    }

                    EscreverTipo01(sb, doc);
                    Contador.Registrar("01");

                    foreach (var item in doc.Itens)
                    {
                        EscreverTipo02(sb, item);
                        Contador.Registrar("02");
                    }
                }
            }

            Contador.EscreverResumo(sb);
            
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected virtual void EscreverTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00").Append("|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
        }

        protected virtual void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
        }

        protected virtual void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }
    }
}
