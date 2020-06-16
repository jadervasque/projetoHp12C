using System;
using HP12C.Classes;

namespace HP12C.Funcoes
{
    internal class FuncoesRCL : FuncoesBase
    {
        public void numero(Memoria memoria, string tag)
        {
            _memoria = memoria;
            PilhaUp();
            int mem = Convert.ToInt32(tag);
            bool ponto = _memoria.Armazenamento.PontoPressed;
            _memoria.xs = _memoria.Armazenamento.GetMemoria(mem, ponto);
            SetResultado();
        }

        public void financeira(Memoria memoria, string tag)
        {
            _memoria = memoria;
            SetResultado();
            PilhaUp();
            switch (Convert.ToInt32(tag))
            {
                case 0:
                    _memoria.xs = _memoria.Financeiras.n;
                    break;
                case 1:
                    _memoria.xs = _memoria.Financeiras.i;
                    break;
                case 2:
                    _memoria.xs = _memoria.Financeiras.pv;
                    break;
                case 3:
                    _memoria.xs = _memoria.Financeiras.pmt;
                    break;
                case 4:
                    _memoria.xs = _memoria.Financeiras.fv;
                    break;
            }
        }
    }
}
