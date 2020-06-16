using System;
using HP12C.Classes;

namespace HP12C.Funcoes
{
    internal class FuncoesSTO : FuncoesBase
    {
        public void operador(Memoria memoria, string tag)
        {
            _memoria = memoria;
            switch (tag)
            {
                case "Somar":
                    _memoria.Armazenamento.Operador = EnumOperador.Somar;
                    break;
                case "Subtrair":
                    _memoria.Armazenamento.Operador = EnumOperador.Subtrair;
                    break;
                case "Multiplicar":
                    _memoria.Armazenamento.Operador = EnumOperador.Multiplicar;
                    break;
                case "Dividir":
                    _memoria.Armazenamento.Operador = EnumOperador.Dividir;
                    break;
            }
        }

        public void numero(Memoria memoria, string tag)
        {
            _memoria = memoria;
            int mem = Convert.ToInt32(tag);
            bool ponto = _memoria.Armazenamento.PontoPressed;
            double valor = _memoria.xd;
            double sto = _memoria.Armazenamento.GetMemoria(mem, ponto, false);
            switch (_memoria.Armazenamento.Operador)
            {
                case EnumOperador.Somar:
                    valor = sto + valor;
                    break;
                case EnumOperador.Subtrair:
                    valor = sto - valor;
                    break;
                case EnumOperador.Multiplicar:
                    valor = sto * valor;
                    break;
                case EnumOperador.Dividir:
                    valor = sto / valor;
                    break;
            }
            _memoria.Armazenamento.SetMemoria(mem, ponto, valor.ToString("N50"));
            SetResultado();
        }

        
    }
}
