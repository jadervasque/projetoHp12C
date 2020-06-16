using System;
using System.Windows.Forms;
using HP12C.Classes;

namespace HP12C.Controles
{
    public partial class MemFin : UserControl
    {
        private string _n = "0";
        private string _i = "0";
        private string _pv = "0";
        private string _pmt = "0";
        private string _fv = "0";
        private bool _chamarResultado = false;

        public string n
        {
            get
            {
                return _n;
            }

            set
            {
                _n = value;
                SetTextBoxes();
                _chamarResultado = true;
            }
        }

        public string i
        {
            get
            {
                return _i;
            }

            set
            {
                _i = value;
                SetTextBoxes();
                _chamarResultado = true;
            }
        }

        public string pv
        {
            get
            {
                return _pv;
            }

            set
            {
                _pv = value;
                SetTextBoxes();
                _chamarResultado = true;
            }
        }

        public string pmt
        {
            get
            {
                return _pmt;
            }

            set
            {
                _pmt = value;
                SetTextBoxes();
                _chamarResultado = true;
            }
        }

        public string fv
        {
            get
            {
                return _fv;
            }

            set
            {
                _fv = value;
                SetTextBoxes();
                _chamarResultado = true;
            }
        }

        public double nd
        {
            get { return Convert.ToDouble(_n); }
        }

        public double id
        {
            get { return Convert.ToDouble(_i); }
        }

        public double pvd
        {
            get { return Convert.ToDouble(_pv); }
        }

        public double pmtd
        {
            get { return Convert.ToDouble(_pmt); }
        }

        public double fvd
        {
            get { return Convert.ToDouble(_fv); }
        }

        public bool ChamarResultado
        {
            get
            {
                return _chamarResultado;
            }

            set
            {
                _chamarResultado = value;
            }
        }

        public MemFin()
        {
            InitializeComponent();
            Zerar();
        }

        public void Zerar()
        {
            Zerar(painel.Controls);
            SetTextBoxes();
        }

        private void Zerar(ControlCollection controls)
        {
            _n = "0";
            _i = "0";
            _pv = "0";
            _pmt = "0";
            _fv = "0";
        }

        private void SetTextBoxes()
        {
            txn.Text = Calculos.SetFormato(_n, false, -1, 2);
            txi.Text = Calculos.SetFormato(_i, false, -1, 4);
            txpv.Text = Calculos.SetFormato(_pv, false, -1, 2);
            txpmt.Text = Calculos.SetFormato(_pmt, false, -1, 2);
            txfv.Text = Calculos.SetFormato(_fv, false, -1, 2);
            Calculos.SetColor(txn);
            Calculos.SetColor(txi);
            Calculos.SetColor(txpv);
            Calculos.SetColor(txpmt);
            Calculos.SetColor(txpv);
        }
    }
}
