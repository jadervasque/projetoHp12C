using HP12C.Properties;
using System;
using System.Drawing;

namespace HP12C.Messages
{
    public class ShowMessage
    {
        public static void Exc(string mensagem)
        {
            Bitmap bitmap = new Bitmap(Resources.alert);
            MessageOk messageOk = new MessageOk(mensagem, bitmap);
            messageOk.Text = "Atenção";
            int num = (int)messageOk.ShowDialog();
        }

        public static void Ok(string mensagem)
        {
            Bitmap bitmap = new Bitmap(Resources.confirm);
            MessageOk messageOk = new MessageOk(mensagem, bitmap);
            messageOk.Text = "Confirmação";
            int num = (int)messageOk.ShowDialog();
        }

        public static void Info(string mensagem)
        {
            Bitmap bitmap = new Bitmap(Resources.info);
            MessageOk messageOk = new MessageOk(mensagem, bitmap);
            messageOk.Text = "Informação";
            int num = (int)messageOk.ShowDialog();
        }

        public static void Erro(string mensagem)
        {
            Bitmap bitmap = new Bitmap(Resources.error);
            MessageOk messageOk = new MessageOk(mensagem, bitmap);
            messageOk.Text = "Erro";
            int num = (int)messageOk.ShowDialog();
        }

        public static void Erro(Exception ex)
        {
            Bitmap bitmap = new Bitmap(Resources.error);
            MessageOk messageOk = new MessageOk(ex.Message, bitmap);
            messageOk.Text = "Erro";
            int num = (int)messageOk.ShowDialog();
        }

        public static bool SimNao(string mensagem)
        {
            return new MessageYesNo(mensagem).ShowQuestion();
        }
    }
}
