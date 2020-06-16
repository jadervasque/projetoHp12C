using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HP12C.Properties;

namespace HP12C.Messages
{
    internal class MessageYesNo : Form
    {
        private bool result = false;
        private IContainer components = null;
        private PictureBox imgMensagem;
        private Panel panel1;
        private Button btSim;
        private Label txMessage;
        private Button btNao;
        private FlowLayoutPanel flowLayoutPanel1;

        public MessageYesNo(string mensagem)
        {
            InitializeComponent();
            txMessage.Text = mensagem;
            if (txMessage.PreferredHeight > 73)
            {
                txMessage.Height = txMessage.PreferredHeight;
                Height = txMessage.Height + 106;
            }
            if (txMessage.PreferredWidth <= 290)
                return;
            txMessage.Width = txMessage.PreferredWidth;
            Width = txMessage.Width + 124;
        }

        private void btSim_Click(object sender, EventArgs e)
        {
            result = true;
            Close();
        }

        private void btNao_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool ShowQuestion()
        {
            int num = (int)ShowDialog();
            return result;
        }

        private void MessageYesNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            imgMensagem = new PictureBox();
            panel1 = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btNao = new Button();
            btSim = new Button();
            txMessage = new Label();
            ((ISupportInitialize)imgMensagem).BeginInit();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            imgMensagem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            imgMensagem.Image = Resources.question;
            imgMensagem.Location = new Point(14, 9);
            imgMensagem.Margin = new Padding(0, 0, 0, 5);
            imgMensagem.Name = "imgMensagem";
            imgMensagem.Size = new Size(60, 73);
            imgMensagem.SizeMode = PictureBoxSizeMode.Zoom;
            imgMensagem.TabIndex = 0;
            imgMensagem.TabStop = false;
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 90);
            panel1.Name = "panel1";
            panel1.Size = new Size(398, 50);
            panel1.TabIndex = 1;
            flowLayoutPanel1.Anchor = AnchorStyles.None;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(btSim);
            flowLayoutPanel1.Controls.Add(btNao);
            flowLayoutPanel1.Location = new Point(73, 5);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(252, 40);
            flowLayoutPanel1.TabIndex = 2;
            btNao.BackColor = SystemColors.Control;
            btNao.Location = new Point(131, 0);
            btNao.Margin = new Padding(5, 0, 5, 0);
            btNao.Name = "btNao";
            btNao.Size = new Size(116, 40);
            btNao.TabIndex = 1;
            btNao.Text = "&Não";
            btNao.UseVisualStyleBackColor = true;
            btNao.Click += new EventHandler(btNao_Click);
            btSim.BackColor = SystemColors.Control;
            btSim.Location = new Point(5, 0);
            btSim.Margin = new Padding(5, 0, 5, 0);
            btSim.Name = "btSim";
            btSim.Size = new Size(116, 40);
            btSim.TabIndex = 0;
            btSim.Text = "&Sim";
            btSim.UseVisualStyleBackColor = true;
            btSim.Click += new EventHandler(btSim_Click);
            txMessage.Location = new Point(89, 9);
            txMessage.Margin = new Padding(10);
            txMessage.MaximumSize = new Size(476, 594);
            txMessage.Name = "txMessage";
            txMessage.Padding = new Padding(0, 10, 10, 10);
            txMessage.Size = new Size(290, 73);
            txMessage.TabIndex = 2;
            txMessage.Text = "txMessage";
            txMessage.TextAlign = ContentAlignment.MiddleLeft;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(398, 140);
            Controls.Add(txMessage);
            Controls.Add(panel1);
            Controls.Add(imgMensagem);
            Font = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MaximumSize = new Size(600, 700);
            MinimizeBox = false;
            Name = "MessageYesNo";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Confirmação";
            KeyDown += new KeyEventHandler(MessageYesNo_KeyDown);
            ((ISupportInitialize)imgMensagem).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
