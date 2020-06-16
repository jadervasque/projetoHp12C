using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HP12C.Classes;

namespace HP12C.Controles
{
    public partial class MemArm : UserControl
    {
        private Armazenamento _tx0 = new Armazenamento(0, false);
        private Armazenamento _tx1 = new Armazenamento(1, false);
        private Armazenamento _tx2 = new Armazenamento(2, false);
        private Armazenamento _tx3 = new Armazenamento(3, false);
        private Armazenamento _tx4 = new Armazenamento(4, false);
        private Armazenamento _tx5 = new Armazenamento(5, false);
        private Armazenamento _tx6 = new Armazenamento(6, false);
        private Armazenamento _tx7 = new Armazenamento(7, false);
        private Armazenamento _tx8 = new Armazenamento(8, false);
        private Armazenamento _tx9 = new Armazenamento(9, false);
        private Armazenamento _txp0 = new Armazenamento(0, true);
        private Armazenamento _txp1 = new Armazenamento(1, true);
        private Armazenamento _txp2 = new Armazenamento(2, true);
        private Armazenamento _txp3 = new Armazenamento(3, true);
        private Armazenamento _txp4 = new Armazenamento(4, true);
        private Armazenamento _txp5 = new Armazenamento(5, true);
        private Armazenamento _txp6 = new Armazenamento(6, true);
        private Armazenamento _txp7 = new Armazenamento(7, true);
        private Armazenamento _txp8 = new Armazenamento(8, true);
        private Armazenamento _txp9 = new Armazenamento(9, true);
        private List<Armazenamento> _memorias;
        private bool _pontoPressed = false;
        private EnumOperador _operador = EnumOperador.Isento;

        internal EnumOperador Operador
        {
            get
            {
                return _operador;
            }

            set
            {
                _operador = value;
            }
        }

        public bool PontoPressed
        {
            get
            {
                return _pontoPressed;
            }

            set
            {
                _pontoPressed = value;
            }
        }

        public MemArm()
        {
            InitializeComponent();
            ListMemorias();
            Zerar();
        }

        private void ListMemorias()
        {
            _memorias = new List<Armazenamento>()
            {
                _tx0, _tx1, _tx2, _tx3, _tx4, _tx5, _tx6, _tx7, _tx8, _tx9,
                _txp0, _txp1, _txp2, _txp3, _txp4, _txp5, _txp6, _txp7, _txp8, _txp9
            };
        }

        public void Zerar()
        {
            foreach (Armazenamento item in _memorias)
                item.Valor = "0";
            SetTextBoxes();
        }

        public dynamic GetMemoria(int memoria, bool ponto, bool returnString = true)
        {
            if (memoria < 0 || memoria > 9)
                throw new Exception("Memória inexistente.");
            if (returnString)
                return _memorias.Find(t => t.Name == memoria && t.Ponto == ponto).Valor;
            else
                return Convert.ToDouble(_memorias.Find(t => t.Name == memoria && t.Ponto == ponto).Valor);
        }

        public void SetMemoria(int memoria, bool ponto, string valor)
        {
            if (memoria < 0 || memoria > 9)
                throw new Exception("Memória inexistente.");
            _memorias.Find(t => t.Name == memoria && t.Ponto == ponto).Valor = valor;
            SetTextBoxes();
        }

        private void SetTextBoxes()
        {
            List<TextBox> textBoxes = new List<TextBox>();
            GetTextBoxes(painel.Controls, ref textBoxes);
            foreach (var item in textBoxes)
            {
                bool ponto = item.Name.Contains("p");
                int memoria = Convert.ToInt32(item.Name.Replace("tx", "").Replace("p", ""));
                try
                {
                    item.Text = Calculos.SetFormato(_memorias.Find(t => t.Name == memoria && t.Ponto == ponto).Valor, false, -1, 2);
                }
                catch (Exception)
                {
                    item.Text = string.Format(" ERROR {0}", 9);
                }
                Calculos.SetColor(item);
            }
        }

        private void GetTextBoxes(Control.ControlCollection controls, ref List<TextBox> lista)
        {
            foreach (Control item in controls)
                if (item.GetType() == typeof(TextBox))
                    lista.Add((TextBox)item);
                else if (item.GetType() == typeof(FlowLayoutPanel) || item.GetType() == typeof(GroupBox))
                    GetTextBoxes(item.Controls, ref lista);
        }
    }

    internal class Armazenamento
    {
        public string Valor { get; set; }
        public int Name { get; set; }
        public bool Ponto { get; set; }

        public Armazenamento(int name, bool ponto)
        {
            Name = name;
            Ponto = ponto;
        }
    }
}
