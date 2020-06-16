using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using HP12C.Classes;
using HP12C.Controles;
using HP12C.Funcoes;
using HP12C.Messages;

namespace HP12C
{
    public partial class FrmHP12C : Form
    {
        private string _arquivo = Path.Combine(Path.GetTempPath(), "memoria.sav");
        private EnumChave _chave = EnumChave.isento;
        private Memoria _memorias = new Memoria();
        private List<Botao> _botoesFinanceiros;
        private List<Botao> _botoesOperadores;
        private List<Botao> _botoesNumeros;
        private MemFin _mf = new MemFin();
        private MemArm _ma = new MemArm();
        private Point _positionCalc;

        public FrmHP12C()
        {
            InitializeComponent();
            VincularMemorias();
            SetMemorias();
            SetBotoes();
            _ma.MouseDown += new MouseEventHandler(EventoMouseDown);
            _ma.MouseMove += new MouseEventHandler(EventoMouseMove);
            _mf.MouseDown += new MouseEventHandler(EventoMouseDown);
            _mf.MouseMove += new MouseEventHandler(EventoMouseMove);
            _ma.Visible = false;
            _mf.Visible = false;
            Controls.Add(_ma);
            Controls.Add(_mf);
            _ma.BringToFront();
            _mf.BringToFront();
            _ma.Location = new Point(966, 229);
            _mf.Location = new Point(580, 101);
        }

        private void LoadSave()
        {
            string file = _arquivo;
            if (File.Exists(file))
                try
                {
                    using (Memoria listofa = new Memoria())
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(Memoria));
                        string newFile = Path.Combine(string.Format("{0}.temp", file));
                        File.Copy(file, newFile, true);
                        FileStream aFile = new FileStream(newFile, FileMode.Open);
                        byte[] buffer = new byte[aFile.Length];
                        aFile.Read(buffer, 0, (int)aFile.Length);
                        MemoryStream stream = new MemoryStream(buffer);
                        _memorias = (Memoria)formatter.Deserialize(stream);
                    }
                }
                catch (Exception ex) { /* ShowMessage.Erro(ex); */ }
        }

        private void SaveFile()
        {
            try
            {
                _memorias.Digitando = false;
                _memorias.Resultado = false;
                _memorias.Error = -1;
                string path = _arquivo;
                FileStream outFile = File.Create(path);
                XmlSerializer formatter = new XmlSerializer(typeof(Memoria));
                formatter.Serialize(outFile, _memorias);
            }
            catch (Exception ex) { ShowMessage.Erro(ex); }
        }

        private void VincularMemorias()
        {
            _memorias.Armazenamento = _ma;
            _memorias.Financeiras = _mf;
            pnCalculator.Controls.AddRange(_memorias.GetLabels());
        }

        private void SetMemorias()
        {
            memx.Text = _memorias.xs;
            memy.Text = _memorias.ys;
            memz.Text = _memorias.zs;
            memt.Text = _memorias.ts;
        }

        private void SetBotoes()
        {
            _botoesFinanceiros = new List<Botao>()
            {
                btn, bti, btpv, btpmt, btfv
            };
            _botoesOperadores = new List<Botao>()
            {
                btmais, btmenos, btdivide, btvezes
            };
            _botoesNumeros = new List<Botao>()
            {
                bt0, bt1, bt2, bt3, bt4, bt5, bt6, bt7, bt8, bt9
            };
        }

        private void BotaoClick(object sender, EventArgs e = null)
        {
            try
            {
                Botao botao = (Botao)sender;
                bool flagPonto;
                bool flagOperador;
                {
                    bool flag1 = _memorias.Armazenamento.PontoPressed;
                    bool flag2 = !_botoesOperadores.Exists(t => t.Name == botao.Name);
                    bool flag3 = !_botoesNumeros.Exists(t => t.Name == botao.Name);
                    flagPonto = flag1 && flag2 && flag3;
                }
                {
                    bool flag1 = _memorias.Armazenamento.Operador != EnumOperador.Isento;
                    bool flag2 = !_botoesNumeros.Exists(t => t.Name == botao.Name);
                    flagOperador = flag1 && flag2;
                }
                if (flagPonto || flagOperador)
                {
                    _memorias.Armazenamento.PontoPressed = false;
                    _memorias.Armazenamento.Operador = EnumOperador.Isento;
                    _chave = EnumChave.isento;
                    SetChaves();
                    SetMemorias();
                    return;
                }
                if (_memorias.Error == -1)
                {
                    if (!_botoesFinanceiros.Exists(t => t.Name == botao.Name))
                        _memorias.ChamarResultadoFin = false;
                    try
                    {
                        bool flag1 = botao.N != EnumN.isento && _chave == EnumChave.isento;
                        bool flag2 = botao.F != EnumF.isento && _chave == EnumChave.F;
                        bool flag3 = botao.G != EnumG.isento && _chave == EnumChave.G;
                        bool flag4 = botao.STO != EnumSTO.isento && _chave == EnumChave.STO;
                        bool flag5 = botao.RCL != EnumRCL.isento && _chave == EnumChave.RCL;
                        if (flag1 | flag2 | flag3 | flag4 | flag5)
                            Funcoes(botao);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage.Erro(ex);
                    }
                }
                else
                    _memorias.Error = -1;
                {
                    bool flag1 = !_memorias.Armazenamento.PontoPressed;
                    bool flag2 = _memorias.Armazenamento.Operador == EnumOperador.Isento;
                    if (flag1 && flag2)
                        _chave = EnumChave.isento;
                }
                SetChaves();
                SetMemorias();
            }
            catch (Exception ex)
            {
                ShowMessage.Erro(ex);
            }
        }

        private void Funcoes(Botao botao)
        {
            try
            {
                string metodo;
                switch (_chave)
                {
                    case EnumChave.isento:
                        metodo = botao.N.ToString();
                        FuncoesN funcaoN = new FuncoesN();
                        funcaoN.GetType().GetMethod(metodo).Invoke(funcaoN, new object[] { _memorias, botao.Tag });
                        break;
                    case EnumChave.F:
                        metodo = botao.F.ToString();
                        FuncoesF funcaoF = new FuncoesF();
                        funcaoF.GetType().GetMethod(metodo).Invoke(funcaoF, new object[] { _memorias, botao.Tag });
                        break;
                    case EnumChave.G:
                        metodo = botao.G.ToString();
                        FuncoesG funcaoG = new FuncoesG();
                        funcaoG.GetType().GetMethod(metodo).Invoke(funcaoG, new object[] { _memorias, botao.Tag });
                        break;
                    case EnumChave.STO:
                        metodo = botao.STO.ToString();
                        FuncoesSTO funcaoSTO = new FuncoesSTO();
                        funcaoSTO.GetType().GetMethod(metodo).Invoke(funcaoSTO, new object[] { _memorias, botao.Tag });
                        break;
                    case EnumChave.RCL:
                        metodo = botao.RCL.ToString();
                        FuncoesRCL funcaoRCL = new FuncoesRCL();
                        funcaoRCL.GetType().GetMethod(metodo).Invoke(funcaoRCL, new object[] { _memorias, botao.Tag });
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChaveClick(object sender, EventArgs e = null)
        {
            try
            {
                if (_memorias.Error == -1)
                {
                    switch (((Botao)sender).Chave)
                    {
                        case EnumChave.F:
                        case EnumChave.G:
                        case EnumChave.STO:
                        case EnumChave.RCL:
                            _chave = ((Botao)sender).Chave;
                            break;
                    }
                    SetChaves();
                }
                else
                {
                    _memorias.Error = -1;
                    SetChaves();
                    SetMemorias();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.Erro(ex);
            }
        }

        private void SetChaves()
        {
            try
            {
                _memorias.SetF(false);
                _memorias.SetG(false);
                Control.ControlCollection controls = pnCalculator.Controls;
                foreach (Control item in controls)
                    if (item.GetType() == typeof(Botao))
                        ((Botao)item).DesativarFuncoes();
                if (_chave != EnumChave.isento)
                {
                    if (_chave == EnumChave.F)
                        _memorias.SetF(true);
                    else if (_chave == EnumChave.G)
                        _memorias.SetG(true);
                    foreach (Control item in controls)
                        if (item.GetType() == typeof(Botao))
                            SetChaveBotao((Botao)item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetChaveBotao(Botao botao)
        {
            try
            {
                switch (_chave)
                {
                    case EnumChave.F:
                        if (botao.F != EnumF.isento)
                            botao.AtivarF();
                        break;
                    case EnumChave.G:
                        if (botao.G != EnumG.isento)
                            botao.AtivarG();
                        break;
                    case EnumChave.STO:
                        if (botao.STO != EnumSTO.isento)
                            if (botao.Tag.ToString() == "Ponto" && (_memorias.Armazenamento.PontoPressed || _memorias.Armazenamento.Operador != EnumOperador.Isento))
                                break;
                            else if (_botoesOperadores.Exists(t => t.Name == botao.Name) && _memorias.Armazenamento.Operador != EnumOperador.Isento)
                                break;
                            else
                                botao.AtivarStoRcl();
                        break;
                    case EnumChave.RCL:
                        if (botao.RCL != EnumRCL.isento)
                            botao.AtivarStoRcl();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btPilhaAutomatica_Click(object sender, EventArgs e)
        {
            gx.Visible = !gx.Visible;
            gy.Visible = !gy.Visible;
            gz.Visible = !gz.Visible;
            gt.Visible = !gt.Visible;
        }

        private void btArmazenamento_Click(object sender, EventArgs e)
        {
            _ma.Visible = !_ma.Visible;
        }

        private void btFinanceiras_Click(object sender, EventArgs e)
        {
            _mf.Visible = !_mf.Visible;
        }

        private void bton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EventoMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _positionCalc = e.Location;
            }
        }

        private void EventoMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (sender.GetType() == typeof(Panel))
            {
                ((Panel)sender).Left += e.X - _positionCalc.X;
                ((Panel)sender).Top += e.Y - _positionCalc.Y;
            }
            else if (sender.GetType() == typeof(MemArm))
            {
                ((MemArm)sender).Left += e.X - _positionCalc.X;
                ((MemArm)sender).Top += e.Y - _positionCalc.Y;
            }
            else if (sender.GetType() == typeof(MemFin))
            {
                ((MemFin)sender).Left += e.X - _positionCalc.X;
                ((MemFin)sender).Top += e.Y - _positionCalc.Y;
            }
        }

        Point _posicao;
        private void btPequena_Click(object sender, EventArgs e)
        {
            string compacta = "&Compacta";
            string expandida = "&Expandida";
            ToolStripMenuItem botao = (ToolStripMenuItem)sender;
            if (botao.Text == compacta)
            {
                WindowState = FormWindowState.Normal;
                _posicao = panel2.Location;
                panel2.Location = new Point(13, 38);
                Size = new Size(643, 690);
                botao.Text = expandida;
                btFinanceiras.Visible = false;
                btArmazenamento.Visible = false;
                if (_mf.Visible)
                    btFinanceiras_Click(null, null);
                if (_ma.Visible)
                    btArmazenamento_Click(null, null);
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                panel2.Location = _posicao;
                botao.Text = compacta;
                btFinanceiras.Visible = true;
                btArmazenamento.Visible = true;
            }
        }
    }
}
