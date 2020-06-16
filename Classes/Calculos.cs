using System;
using System.Windows.Forms;

namespace HP12C.Classes
{
    public class Calculos
    {
        public static string SetFormato(string memoria, bool isx, int Error, int QtdCasas)
        {
            if (isx && Error >= 0)
                return string.Format(" ERROR {0}", Error);
            if (memoria.Contains("E+"))
            {
                string v1 = memoria.Split('E')[0];
                string v2 = memoria.Split('+')[1];

            }
            memoria = memoria.Replace(" ", "");
            string valor = ToDecimal(memoria).ToString(string.Format("N{0}", QtdCasas));
            if (valor.Replace("-", "").Replace(".", "").Replace(",", "").Length > 10)
            {
                string[] str = valor.Split(',');
                int qtd = 10 - str[0].Replace(".", "").Length;
                qtd = qtd < 0 ? 0 : qtd;
                valor = ToDecimal(memoria).ToString(string.Format("N{0}", qtd));
            }
            if (Math.Abs(ToDouble(valor)) > 9999999999)
            {
                decimal v1 = ToDecimal(valor);
                int v2 = v1.ToString().Split(',')[0].Replace("-", "").Length;
                string s1 = "1";
                int q1 = 0;
                for (int i = 0; i < (v2 - 1); i++)
                {
                    s1 += "0";
                    q1++;
                }
                decimal v3 = v1 / ToDecimal(s1);
                int qtdCasas = QtdCasas > 6 ? 6 : QtdCasas;
                string s2 = v3.ToString(string.Format("N{0}", qtdCasas));
                int espaco = 13/*total*/ - q1.ToString().Length/*final*/ - s2.Split(',')[0].Replace("-", "").Length/*número*/ - 1/*vírgula*/ - qtdCasas;
                for (int i = 0; i < espaco; i++)
                    s2 += " ";
                s2 += q1.ToString();
                valor = s2;
            }
            if (valor.Replace("-", "").Replace(".", "").Replace(",", "").Replace(" ", "").Length > 10)
            {
                int qtd = 10;
                qtd += valor.Contains(",") ? 1 : 0;
                qtd += valor.Contains(" ") ? 1 : 0;
                qtd += valor.Contains("-") ? 1 : 0;
                if (valor.Contains("."))
                    foreach (var letra in valor)
                        if (letra == '.')
                            qtd += 1;
                if (qtd > valor.Length)
                    qtd -= qtd - valor.Length;
                valor = valor.Substring(0, qtd);
            }
            valor = valor.Contains("-") ? valor : string.Format(" {0}", valor);
            return valor;
        }

        public static double ToDouble(string valor)
        {
            try
            {
                return Convert.ToDouble(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ToDecimal(string valor)
        {
            try
            {
                return Convert.ToDecimal(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetColor(TextBox text)
        {
            if (text.Text == " 0,00" || text.Text == " 0,0000")
                text.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Regular);
            else
                text.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold);
        }
    }
}
