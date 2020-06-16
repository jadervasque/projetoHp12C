using System;
using HP12C.Classes;
using HP12C.Scripts;
using Noesis.Javascript;

namespace HP12C.Funcoes
{
    internal class FuncoesG : FuncoesBase
    {

        public void btn_12x(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.Financeiras.n = (_memoria.xd * 12).ToString("N50");
            _memoria.xs = _memoria.Financeiras.n;
            SetResultado();
        }

        public void bti_12d(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.Financeiras.i = (_memoria.xd / 12).ToString("N50");
            _memoria.xs = _memoria.Financeiras.i;
            SetResultado();
        }

        public void btchs_date(Memoria memoria, string tag)
        {
            _memoria = memoria;
            try
            {
                int[] dy = DateCheck(_memoria.yd);
                try
                {
                    DateTime datay = new DateTime(dy[2], dy[1], dy[0]);
                }
                catch (Exception)
                {
                    throw;
                }
                JavascriptContext context = new JavascriptContext();
                context.SetParameter("day", dy[0]);
                context.SetParameter("month", dy[1]);
                context.SetParameter("year", dy[2]);
                context.SetParameter("x", _memoria.xd);
                context.SetParameter("dmy", _memoria.DMY);
                context.Run(JavaScripts.DATE);
                PilhaUp();
                _memoria.xs = context.GetParameter("xx1").ToString();
                SetResultado();
            }
            catch (Exception)
            {
                _memoria.Error = 8;
                SetResultado(false);
            }
        }

        public void btyx_rx(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.xs = Math.Sqrt(_memoria.xd).ToString();
            SetResultado();
        }

        public void bt1x_ex(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.xs = (1 / _memoria.xd).ToString();
            SetResultado();
        }

        public void btpt_ln(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.xs = Math.Log(_memoria.xd).ToString();
            SetResultado();
        }

        public void btpd_frac(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.xs = (_memoria.xd - Math.Floor(_memoria.xd)).ToString();
            SetResultado();
        }

        public void btp_intg(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.xs = Math.Floor(_memoria.xd).ToString();
            SetResultado();

        }

        public void bteex_dys(Memoria memoria, string tag)
        {
            _memoria = memoria;
            try
            {
                int[] dx = DateCheck(_memoria.xd);
                int[] dy = DateCheck(_memoria.yd);
                try
                {
                    DateTime datax = new DateTime(dx[2], dx[1], dx[0]);
                    DateTime datay = new DateTime(dy[2], dy[1], dy[0]);
                }
                catch (Exception)
                {
                    throw;
                }
                JavascriptContext context = new JavascriptContext();
                context.SetParameter("dx", dx[0]);
                context.SetParameter("mx", dx[1]);
                context.SetParameter("yx", dx[2]);
                context.SetParameter("dy", dy[0]);
                context.SetParameter("my", dy[1]);
                context.SetParameter("yy", dy[2]);
                context.Run(JavaScripts.DYS);
                _memoria.xs = context.GetParameter("xx1").ToString();
                _memoria.ys = context.GetParameter("yy1").ToString();
                SetResultado();
            }
            catch (Exception)
            {
                _memoria.Error = 8;
                SetResultado(false);
            }
        }

        public void bt4_dmy(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.DMY = true;
        }

        public void bt5_mdy(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.DMY = false;
        }

        public void bt3_nf(Memoria memoria, string tag)
        {
            _memoria = memoria;
            bool flag1 = (_memoria.xd - Math.Floor(_memoria.xd)) == 0;
            bool flag2 = _memoria.xd > 0;
            if (!flag1 || !flag2)
            {
                _memoria.Error = 0;
                return;
            }
            double numero = _memoria.xd;
            for (double i = (numero - 1); i > 1; i--)
                numero = numero * i;
            _memoria.xs = numero.ToString();
            SetResultado();
        }

        public void bt7_beg(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.BEGIN = 1;
        }

        public void bt8_end(Memoria memoria, string tag)
        {
            _memoria = memoria;
            _memoria.BEGIN = 0;
        }
    }
}
