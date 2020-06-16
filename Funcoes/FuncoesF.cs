using System;
using HP12C.Classes;

namespace HP12C.Funcoes
{
    internal class FuncoesF : FuncoesBase
    {
        public void btn_amort(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _inicioPeriodo = Convert.ToBoolean(_memoria.BEGIN);
            _decimais = _memoria.QtdCasas;
            double[] memorias = new double[5];
            memorias[0] = _memoria.Financeiras.nd;
            memorias[1] = _memoria.Financeiras.id;
            memorias[2] = _memoria.Financeiras.pvd;
            memorias[3] = _memoria.Financeiras.pmtd;
            memorias[4] = _memoria.Financeiras.fvd;
            var requested_n = Convert.ToDouble(_memoria.xd);
            var orig_n = memorias[_n];
            var i = memorias[_i] / 100;
            var pv = Arredondamento(memorias[_pv], _decimais);
            memorias[_pv] = pv;
            var pmt = Arredondamento(memorias[_pmt], _decimais);
            memorias[_pmt] = pmt;
            if (requested_n <= 0 || requested_n != Math.Floor(requested_n) || i <= -1)
            {
                _memoria.Error = 5;
                SetResultado(false);
                return;
            }
            double tot_interest = 0;
            double tot_amort = 0;
            for (var e = 1; e <= requested_n; ++e)
            {
                double interest = Arredondamento(-pv * i, _decimais);
                if (e == 1 && _inicioPeriodo && orig_n <= 0)
                    interest = 0;
                double capital_amortization = pmt - interest;
                tot_interest += interest;
                tot_amort += capital_amortization;
                pv += capital_amortization;
            }
            double x = tot_interest;
            double y = tot_amort;
            double z = requested_n;
            memorias[_n] += requested_n;
            memorias[_pv] += tot_amort;
            _memoria.Financeiras.n = memorias[_n].ToString("N50");
            _memoria.Financeiras.pv = memorias[_pv].ToString("N50");
            PilhaUp(2);
            _memoria.xs = x.ToString();
            _memoria.ys = y.ToString();
            _memoria.zs = z.ToString();
            SetResultado();
        }

        public void bti_int(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double n = _memoria.Financeiras.nd;
            double i = _memoria.Financeiras.id / 100;
            double pv = _memoria.Financeiras.pvd;
            double resultado360 = (n / 360) * pv * i * (-1);
            double resultado365 = (n / 365) * pv * i * (-1);
            PilhaUp(3);
            _memoria.xs = resultado360.ToString();
            _memoria.ys = (_memoria.Financeiras.pvd * (-1)).ToString();
            _memoria.zs = resultado365.ToString();
            SetResultado();
        }

        public void btxty_fin(Memoria memoria, string tag)
        {
            memoria.Financeiras.Zerar();
        }

        public void btclx_reg(Memoria memoria, string tag)
        {
            memoria.Zerar();
        }

        public void btnumero(Memoria memoria, string tag)
        {
            memoria.QtdCasas = Convert.ToInt32(tag);
        }
    }
}
