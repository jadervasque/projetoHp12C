using System;
using System.Windows.Forms;
using HP12C.Controles;

namespace HP12C.Classes
{
    internal class Memoria : IDisposable
    {
        private int _begin = 0;
        private bool _dmy = true;
        private Label _lbF = new Label();
        private Label _lbG = new Label();
        private Label _lbBegin = new Label();
        private Label _lbDmy = new Label();
        private string _xs = "0";
        private string _ys = "0";
        private string _zs = "0";
        private string _ts = "0";

        public Memoria()
        {
            SetLabels();
        }

        public int BEGIN
        {
            get { return _begin; }
            set
            {
                _begin = value;
                _lbBegin.Visible = Convert.ToBoolean(value);
            }
        }

        public int Error { get; set; } = -1;

        public int QtdCasas { get; set; } = 2;

        public bool DMY
        {
            get { return _dmy; }
            set
            {
                _dmy = value;
                _lbDmy.Visible = value;
            }
        }

        public bool Digitando { get; set; } = false;

        public bool Resultado { get; set; } = false;

        public MemArm Armazenamento { get; set; }

        public MemFin Financeiras { get; set; }

        public bool ChamarResultadoFin
        {
            get
            {
                return Financeiras.ChamarResultado;
            }

            set
            {
                Financeiras.ChamarResultado = value;
            }
        }

        #region Memórias
        public string xs
        {
            get
            {
                if (Digitando)
                    return _xs;
                return SetFormato(xds, true);
            }

            set
            {
                _xs = value;
            }
        }

        public string ys
        {
            get
            {
                return SetFormato(yds);
            }

            set
            {
                _ys = value;
            }
        }

        public string zs
        {
            get
            {
                return SetFormato(zds);
            }

            set
            {
                _zs = value;
            }
        }

        public string ts
        {
            get
            {
                return SetFormato(tds);
            }

            set
            {
                _ts = value;
            }
        }

        public double xd
        {
            get { return Convert.ToDouble(_xs); }
        }

        public double yd
        {
            get { return Convert.ToDouble(_ys); }
        }

        public double zd
        {
            get { return Convert.ToDouble(_zs); }
        }

        public double td
        {
            get { return Convert.ToDouble(_ts); }
        }

        public string xds
        {
            get { return xd.ToString("N50"); }
        }

        public string yds
        {
            get { return yd.ToString("N50"); }
        }

        public string zds
        {
            get { return zd.ToString("N50"); }
        }

        public string tds
        {
            get { return td.ToString("N50"); }
        }
        #endregion

        #region Métodos
        private string SetFormato(string memoria, bool isx = false)
        {
            try
            {
                return Calculos.SetFormato(memoria, isx, Error, QtdCasas);
            }
            catch (Exception)
            {
                Digitando = false;
                Resultado = false;
                xs = "0";
                ys = "0";
                zs = "0";
                ts = "0";
                Error = 9;
                return string.Format(" ERROR {0}", Error);
            }
        }

        public void AddXString(string numero)
        {
            if (_xs.Replace(",", "").Replace("-", "").Replace(" ", "").Length < 10)
            {
                if (Digitando)
                    _xs += numero.ToString();
                else if (!_xs.Contains(",") && xd == 0 && numero == ",")
                    _xs = " 0,";
                else if (numero != ",")
                    _xs += string.Format(" {0}", numero);
            }
        }

        public void Zerar()
        {
            Digitando = false;
            Resultado = false;
            xs = "0";
            ys = "0";
            zs = "0";
            ts = "0";
            Financeiras.Zerar();
            Armazenamento.Zerar();
        }

        private void SetLabels()
        {
            _lbBegin.AutoSize = true;
            _lbBegin.BackColor = System.Drawing.Color.Transparent;
            _lbBegin.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _lbBegin.Location = new System.Drawing.Point(261, 70);
            _lbBegin.Margin = new Padding(5);
            _lbBegin.Name = "_lbBegin";
            _lbBegin.Size = new System.Drawing.Size(37, 13);
            _lbBegin.TabIndex = 40;
            _lbBegin.Text = "BEGIN";
            _lbBegin.Visible = false;

            _lbG.AutoSize = true;
            _lbG.BackColor = System.Drawing.Color.Transparent;
            _lbG.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _lbG.Location = new System.Drawing.Point(245, 70);
            _lbG.Margin = new Padding(5);
            _lbG.Name = "lbG";
            _lbG.Size = new System.Drawing.Size(13, 13);
            _lbG.TabIndex = 39;
            _lbG.Text = "g";
            _lbG.Visible = false;

            _lbF.AutoSize = true;
            _lbF.BackColor = System.Drawing.Color.Transparent;
            _lbF.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _lbF.Location = new System.Drawing.Point(228, 70);
            _lbF.Margin = new Padding(5);
            _lbF.Name = "lbF";
            _lbF.Size = new System.Drawing.Size(13, 13);
            _lbF.TabIndex = 38;
            _lbF.Text = "f";
            _lbF.Visible = false;

            _lbDmy.AutoSize = true;
            _lbDmy.BackColor = System.Drawing.Color.Transparent;
            _lbDmy.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _lbDmy.Location = new System.Drawing.Point(306, 70);
            _lbDmy.Margin = new Padding(5);
            _lbDmy.Name = "lbDmy";
            _lbDmy.Size = new System.Drawing.Size(31, 13);
            _lbDmy.TabIndex = 37;
            _lbDmy.Text = "D.MY";
        }

        public Control[] GetLabels()
        {
            return new Control[] { _lbBegin, _lbF, _lbG, _lbDmy };
        }

        public void SetF(bool status)
        {
            _lbF.Visible = status;
        }

        public void SetG(bool status)
        {
            _lbG.Visible = status;
        }

        public DateTime[] GetDates()
        {
            string str1 = xd.ToString("00.000000");
            int diax = Convert.ToInt32(str1.Replace(",", "").Substring(DMY ? 0 : 2, 2));
            int mesx = Convert.ToInt32(str1.Replace(",", "").Substring(DMY ? 2 : 0, 2));
            int anox = Convert.ToInt32(str1.Replace(",", "").Substring(4, 4));
            string str2 = yd.ToString("00.000000");
            int diay = Convert.ToInt32(str2.Replace(",", "").Substring(DMY ? 0 : 2, 2));
            int mesy = Convert.ToInt32(str2.Replace(",", "").Substring(DMY ? 2 : 0, 2));
            int anoy = Convert.ToInt32(str2.Replace(",", "").Substring(4, 4));
            return new DateTime[]
            {
                GetDate(diax, mesx, anox),
                GetDate(diay, mesy, anoy)
            };
        }

        private DateTime GetDate(int dia, int mes, int ano)
        {
            try
            {
                return new DateTime(ano, mes, dia);
            }
            catch (Exception)
            {
                throw new Exception("Data Incorreta.");
            }
        }

        void IDisposable.Dispose()
        {

        }
        #endregion
    }
}
