using System.ComponentModel;
using System.Windows.Forms;
using HP12C.Classes;

namespace HP12C.Controles
{
    [DefaultProperty("TipoPrincial")]

    internal partial class Botao : Button
    {
        const string _configuracoes = " Configurações";
        const string _vazio = "";
        private EnumSTO _sto = EnumSTO.isento;
        private EnumRCL _rcl = EnumRCL.isento;

        [Category(_configuracoes)]
        [DisplayName("Função Normal")]
        public EnumN N { get; set; }

        [Category(_configuracoes)]
        [DisplayName("Função F")]
        public EnumF F { get; set; }

        [Category(_configuracoes)]
        [DisplayName("Função G")]
        public EnumG G { get; set; }

        [Category(_configuracoes)]
        [DisplayName("Função STO")]
        public EnumSTO STO
        {
            get { return _sto; }
            set { _sto = value; }
        }

        [Category(_configuracoes)]
        [DisplayName("Função RCL")]
        public EnumRCL RCL
        {
            get { return _rcl; }
            set { _rcl = value; }
        }

        [Category(_configuracoes)]
        public EnumChave Chave { get; set; }

        public Botao()
        {
            InitializeComponent();
        }

        public void AtivarF()
        {
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange;
        }

        public void AtivarG()
        {
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
        }

        public void AtivarStoRcl()
        {
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = System.Drawing.Color.White;
        }

        public void DesativarFuncoes()
        {
            FlatAppearance.BorderSize = 0;
        }
    }
}
