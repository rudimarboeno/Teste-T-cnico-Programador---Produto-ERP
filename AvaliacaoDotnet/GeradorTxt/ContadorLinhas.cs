using System.Collections.Generic;
using System.Text;

namespace GeradorTxt
{
    public class ContadorLinhas
    {
        private readonly Dictionary<string, int> _contadores = new Dictionary<string, int>();

        public void Registrar(string tipo)
        {
            if (!_contadores.ContainsKey(tipo))
                _contadores[tipo] = 0;

            _contadores[tipo]++;
        }

        public void EscreverResumo(StringBuilder sb)
        {
            int totalLinhas = 0;

            foreach (var item in _contadores)
            {
                sb.Append("09|")
                  .Append(item.Key)
                  .Append("|")
                  .Append(item.Value)
                  .AppendLine();

                totalLinhas += item.Value;
            }

            totalLinhas += _contadores.Count;
            totalLinhas += 1;

            sb.Append("99|")
              .Append(totalLinhas)
              .AppendLine();
        }
    }
}