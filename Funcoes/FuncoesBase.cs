using System;
using HP12C.Classes;

namespace HP12C.Funcoes
{
    internal class FuncoesBase
    {
        protected int _n = 0;
        protected int _i = 1;
        protected int _pv = 2;
        protected int _pmt = 3;
        protected int _fv = 4;
        protected int _decimais;
        protected bool _compostof = false;
        protected bool _inicioPeriodo;
        protected double _interpolacao = 50;
        protected Memoria _memoria;

        protected void PilhaUp(int qtd = 1)
        {
            for (int i = 0; i < qtd; i++)
            {
                _memoria.ts = _memoria.zds;
                _memoria.zs = _memoria.yds;
                _memoria.ys = _memoria.xds;
            }
        }

        protected void PilhaDown(int qtd = 1)
        {
            for (int i = 0; i < qtd; i++)
            {
                _memoria.ys = _memoria.zds;
                _memoria.zs = _memoria.tds;
            }
        }

        protected void SetResultado(bool status = true)
        {
            _memoria.Digitando = false;
            _memoria.Resultado = status;
            _memoria.Armazenamento.PontoPressed = false;
            _memoria.Armazenamento.Operador = EnumOperador.Isento;
        }

        protected void SetDigitando()
        {
            _memoria.Digitando = true;
            _memoria.Resultado = false;
        }

        protected void ChamarResultadoFin(bool status = true)
        {
            _memoria.ChamarResultadoFin = status;
        }

        public void ponto(Memoria memoria, string tag)
        {
            memoria.Armazenamento.PontoPressed = true;
        }

        protected double Arredondamento(double val, int decs)
        {
            if (decs > 11)
            {
                return val;
            }
            var scale = Math.Pow(10, decs);
            return Math.Round(Math.Abs(val) * scale) / scale * BinarySgn(val);
        }

        protected double BinarySgn(double val)
        {
            return (val >= 0 ? 1 : -1);
        }

        protected int[] DateCheck(double date)
        {
            double y, day, month, year;
            y = Math.Round(Math.Abs(date) * 1000000);
            day = Math.Round(y / 1000000) % 100;
            month = Math.Round(y / 10000) % 100;
            year = Math.Round(y % 10000);
            int daymax = 31;
            if (!_memoria.DMY)
            {
                var tmp = day;
                day = month;
                month = tmp;
            }
            if (month == 4 || month == 6 || month == 9 || month == 11)
                daymax = 30;
            else if (month == 2)
            {
                daymax = 28;
                if ((year % 4) == 0 && (((year % 100) != 0) || ((year % 400) == 0)))
                    daymax = 29;
            }
            if (day <= 0 || day > daymax || year <= 0 || year > 9999 || month <= 0 || month > 12)
                throw new Exception();
            return new int[] { (int)day, (int)month, (int)year };
        }

        protected void ChamarCalculo(int memoriaAtual)
        {
            double[] memorias = new double[5];
            memorias[0] = _memoria.Financeiras.nd;
            memorias[1] = _memoria.Financeiras.id;
            memorias[2] = _memoria.Financeiras.pvd;
            memorias[3] = _memoria.Financeiras.pmtd;
            memorias[4] = _memoria.Financeiras.fvd;
            int erro = CalcularFinanceiras(memoriaAtual, ref memorias);
            if (erro == -1)
            {
                double resultado = memorias[memoriaAtual];
                switch (memoriaAtual)
                {
                    case 0:
                        _memoria.Financeiras.n = resultado.ToString("N50");
                        break;
                    case 1:
                        _memoria.Financeiras.i = resultado.ToString("N50");
                        break;
                    case 2:
                        _memoria.Financeiras.pv = resultado.ToString("N50");
                        break;
                    case 3:
                        _memoria.Financeiras.pmt = resultado.ToString("N50");
                        break;
                    case 4:
                        _memoria.Financeiras.fv = resultado.ToString("N50");
                        break;
                }
                PilhaUp();
                _memoria.xs = resultado.ToString("N50");
                ChamarResultadoFin(false);
            }
            else
            {
                _memoria.Error = 5;
                SetResultado(false);
            }
        }

        protected int CalcularFinanceiras(int memoriaAtual, ref double[] memorias)
        {
            bool erro = false;
            if (memoriaAtual == 0)
                erro = erro || memorias[_i] <= -100; // i <= -100
            else if (memoriaAtual == 2)
                erro = erro || memorias[_i] <= -100; // i <= -100
            else if (memoriaAtual == 3)
            {
                erro = erro || memorias[_i] <= -100; // i <= -100
                erro = erro || memorias[_n] == 0; // n = 0
            }
            else if (memoriaAtual == 4)
                erro = erro || memorias[_i] <= -100; // i <= -100
            if (erro)
                return 5;
            double primeiroNPV;
            double segundoNPV;
            double interpolacaoChute;
            double primeiroChute;
            double segundoChute;
            var salvo = memorias[memoriaAtual];
            var interacao = _interpolacao;
            var limite = 0.000000000125;
            double limiteOrdem = 0;
            if (memoriaAtual != _pv)
                limiteOrdem += Math.Abs(memorias[_pv]);
            if (memoriaAtual != _pmt)
                limiteOrdem += Math.Abs(memorias[_pmt]);
            if (memoriaAtual != _n && memoriaAtual != _pmt)
                limiteOrdem += Math.Abs(memorias[_n] * memorias[_pmt]);
            if (memoriaAtual != _fv)
                limiteOrdem += Math.Abs(memorias[_fv]);
            if (limiteOrdem > 0)
                limite *= limiteOrdem;
            if (memoriaAtual == _n || memoriaAtual == _i || limiteOrdem <= 0)
                segundoChute = 1;
            else
                segundoChute = limiteOrdem;
            interpolacaoChute = 0;
            while (--interacao >= 0)
            {
                primeiroChute = segundoChute;
                segundoChute = interpolacaoChute;
                memorias[memoriaAtual] = primeiroChute;
                if (memorias[_i] <= -100)
                    break;
                primeiroNPV = CalcularNPV(memoriaAtual == 0, memorias);
                memorias[memoriaAtual] = segundoChute;
                if (memorias[_i] <= -100)
                    break;
                segundoNPV = CalcularNPV(memoriaAtual == 0, memorias);
                if (Math.Abs(segundoNPV) < limite)
                {
                    if (memoriaAtual == 0)
                    {
                        if ((segundoChute - Math.Floor(segundoChute)) > 0.003)
                            memorias[memoriaAtual] = Math.Floor(memorias[memoriaAtual]) + 1;
                        else
                            memorias[memoriaAtual] = Math.Floor(memorias[memoriaAtual]);
                    }
                    return -1;
                }
                var interpolacaoB = (segundoNPV - primeiroNPV) / (segundoChute - primeiroChute);
                interpolacaoChute = primeiroNPV - primeiroChute * interpolacaoB;
                interpolacaoChute /= -interpolacaoB;
                interpolacaoChute = ResolverInfinito(interpolacaoChute);
            }
            memorias[memoriaAtual] = salvo;
            return 5;
        }

        protected double CalcularNPV(bool is_n, double[] memorias)
        {
            double n = memorias[_n];
            double i = memorias[_i];
            double pv = memorias[_pv];
            double pmt = memorias[_pmt];
            double fv = memorias[_fv];
            if (n == Math.Floor(n) || is_n)
                return pv +
                    (1 + (i / 100) * (_inicioPeriodo ? 1 : 0)) * pmt * ComputarPmtLimite(i, n) +
                    fv * Math.Pow(1 + (i / 100), -n);
            else if (!_compostof)
                return pv * (1 + ((i / 100) * (n - Math.Floor(n)))) +
                    (1 + (i / 100) * (_inicioPeriodo ? 1 : 0)) * pmt * ComputarPmtLimite(i, Math.Floor(n)) +
                    fv * Math.Pow(1 + (i / 100), -Math.Floor(n));
            else
                return pv * Math.Pow(1 + (i / 100), (n - Math.Floor(n))) +
                    (1 + (i / 100) * (_inicioPeriodo ? 1 : 0)) * pmt * ComputarPmtLimite(i, Math.Floor(n)) +
                    fv * Math.Pow(1 + (i / 100), -Math.Floor(n));
        }

        protected double ComputarPmtLimite(double i, double n)
        {
            if (Math.Abs(i) < 0.00000001)
                return n;
            else
                return (1 - Math.Pow(1 + (i / 100), -n)) / (i / 100);
        }

        protected double ResolverInfinito(double val)
        {
            if (val > Math.Pow(10, 95))
                val = Math.Pow(10, 95);
            else if (val < -Math.Pow(10, 95))
                val = -Math.Pow(10, 95);
            return val;
        }
    }
}
