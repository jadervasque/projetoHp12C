using System;
using HP12C.Classes;

namespace HP12C.Funcoes
{
    internal class FuncoesN : FuncoesBase
    {
        public void btfinanceira(Memoria memoria, string tag)
        {
            _memoria = memoria;
            int memoriaAtual = Convert.ToInt32(tag);
            if (_memoria.ChamarResultadoFin)
            {
                _inicioPeriodo = Convert.ToBoolean(_memoria.BEGIN);
                ChamarCalculo(memoriaAtual);
            }
            else
            {
                switch (memoriaAtual)
                {
                    case 0:
                        _memoria.Financeiras.n = _memoria.xds;
                        break;
                    case 1:
                        _memoria.Financeiras.i = _memoria.xds;
                        break;
                    case 2:
                        _memoria.Financeiras.pv = _memoria.xds;
                        break;
                    case 3:
                        _memoria.Financeiras.pmt = _memoria.xds;
                        break;
                    case 4:
                        _memoria.Financeiras.fv = _memoria.xds;
                        break;
                }
                _memoria.ChamarResultadoFin = true;
            }
            SetResultado();
        }

        public void btchs(Memoria memoria, string tag)
        {
            _memoria = memoria;
            if (Math.Abs(_memoria.xd) > 9999999999)
                _memoria.xs = (_memoria.xd * (-1)).ToString("N50");
            else if (_memoria.xs.Contains(" "))
                _memoria.xs = _memoria.xs.Replace(" ", "-");
            else
                _memoria.xs = _memoria.xs.Replace("-", " ");
        }

        public void btxy(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = Math.Pow(Convert.ToDouble(_memoria.yd), Convert.ToDouble(_memoria.xd));
            _memoria.xs = resultado.ToString();
            PilhaDown();
            SetResultado();
        }

        public void bt1x(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double x = _memoria.xd;
            if (x == 0)
            {
                _memoria.Error = 0;
                SetResultado(false);
            }
            else
            {
                _memoria.xs = (1 / x).ToString();
                SetResultado();
            }
        }

        public void btpt(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = (_memoria.xd / _memoria.yd) * 100;
            _memoria.xs = resultado.ToString();
            SetResultado();
        }

        public void btpd(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = ((_memoria.xd - _memoria.yd) / _memoria.yd) * 100;
            _memoria.xs = resultado.ToString();
            SetResultado();
        }

        public void btp(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = _memoria.xd * (_memoria.yd / 100);
            _memoria.xs = resultado.ToString();
            SetResultado();
        }

        public void btr(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double temp = _memoria.xd;
            _memoria.xs = _memoria.yds;
            _memoria.ys = _memoria.zds;
            _memoria.zs = _memoria.tds;
            _memoria.ts = temp.ToString();
            SetResultado();
        }

        public void btxty(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double temp = _memoria.xd;
            _memoria.xs = _memoria.yds;
            _memoria.ys = temp.ToString();
            SetResultado();
        }

        public void btclx(Memoria memoria, string tag)
        {
            _memoria = memoria;
            SetResultado(false);
            _memoria.xs = "0";
        }

        public void btenter(Memoria memoria, string tag)
        {
            _memoria = memoria;
            SetResultado(false);
            PilhaUp();
        }

        public void btnumero(Memoria memoria, string tag)
        {
            _memoria = memoria;
            int numero = Convert.ToInt32(tag);
            if (_memoria.Digitando)
            {
                _memoria.AddXString(numero.ToString());
            }
            else if (_memoria.Resultado)
            {
                PilhaUp();
                _memoria.xs = string.Format(" {0}", numero.ToString());
                SetDigitando();
            }
            else
            {
                _memoria.xs = string.Format(" {0}", numero.ToString());
                SetDigitando();
            }
        }

        public void btSomar(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = _memoria.yd + _memoria.xd;
            _memoria.xs = resultado.ToString();
            PilhaDown();
            SetResultado();
        }

        public void btSubtrair(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = _memoria.yd - _memoria.xd;
            _memoria.xs = resultado.ToString();
            PilhaDown();
            SetResultado();
        }

        public void btDividir(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = _memoria.yd / _memoria.xd;
            _memoria.xs = resultado.ToString();
            PilhaDown();
            SetResultado();
        }

        public void btMultiplicar(Memoria memoria, string tag)
        {
            _memoria = memoria;
            double resultado = _memoria.yd * _memoria.xd;
            _memoria.xs = resultado.ToString("N50");
            PilhaDown();
            SetResultado();
        }

        public void btponto(Memoria memoria, string tag)
        {
            _memoria = memoria;
            if (_memoria.Resultado && !_memoria.Digitando)
            {
                PilhaUp();
                _memoria.xs = " 0,";
                SetDigitando();
            }
            else if (_memoria.Digitando && !_memoria.xs.Contains(","))
            {
                _memoria.AddXString(",");
            }
            else if (!_memoria.Digitando)
            {
                _memoria.xs = " 0,";
                SetDigitando();
            }
        }
    }
}
