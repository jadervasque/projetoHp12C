using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HP12C.Messages
{
    internal class MessageOk : Form
    {
        private IContainer components = null;
        private PictureBox imgMensagem;
        private Panel panel1;
        private Button btOk;
        private Label txMessage;

        public MessageOk(string mensagem, Image image)
        {
            InitializeComponent();
            txMessage.Text = mensagem;
            imgMensagem.Image = image;
            if (txMessage.PreferredHeight > 73)
            {
                txMessage.Height = txMessage.PreferredHeight;
                Height = txMessage.Height + 106;
            }
            if (txMessage.PreferredWidth <= 289)
                return;
            txMessage.Width = txMessage.PreferredWidth;
            Width = txMessage.Width + 124;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MessageOk_KeyDown(object sender, KeyEventArgs e)
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
            btOk = new Button();
            txMessage = new Label();
            ((ISupportInitialize)imgMensagem).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            imgMensagem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            imgMensagem.Location = new Point(14, 9);
            imgMensagem.Margin = new Padding(0, 0, 0, 5);
            imgMensagem.Name = "imgMensagem";
            imgMensagem.Size = new Size(60, 73);
            imgMensagem.SizeMode = PictureBoxSizeMode.Zoom;
            imgMensagem.TabIndex = 0;
            imgMensagem.TabStop = false;
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(btOk);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 90);
            panel1.Name = "panel1";
            panel1.Size = new Size(397, 50);
            panel1.TabIndex = 1;
            btOk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btOk.BackColor = SystemColors.ButtonFace;
            btOk.Location = new Point(140, 5);
            btOk.Margin = new Padding(214, 10, 214, 10);
            btOk.Name = "btOk";
            btOk.Size = new Size(116, 40);
            btOk.TabIndex = 0;
            btOk.Text = "&Ok";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += new EventHandler(btOk_Click);
            txMessage.Location = new Point(89, 9);
            txMessage.Margin = new Padding(10);
            txMessage.MaximumSize = new Size(476, 594);
            txMessage.Name = "txMessage";
            txMessage.Padding = new Padding(0, 10, 10, 10);
            txMessage.Size = new Size(289, 73);
            txMessage.TabIndex = 2;
            txMessage.Text = "txMessage";
            txMessage.TextAlign = ContentAlignment.MiddleLeft;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(397, 140);
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
            Name = "MessageOk";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            KeyDown += new KeyEventHandler(MessageOk_KeyDown);
            ((ISupportInitialize)imgMensagem).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
