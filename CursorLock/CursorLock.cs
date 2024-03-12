using NonInvasiveKeyboardHookLibrary;
namespace CursorLock
{
    public partial class CursorLock : Form
    {
        int i = 1;
        bool ativado = false;
        int y;
        private KeyboardHookManager keyboardHookManager;
        public CursorLock()
        {
            InitializeComponent();
            keyboardHookManager = new KeyboardHookManager();
            keyboardHookManager.Start();
            keyboardHookManager.RegisterHotkey(NonInvasiveKeyboardHookLibrary.ModifierKeys.Control, (int)Keys.F8, codigoPrincipal);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            y = -1;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            button1.Visible = false;
            label1.Visible = false;
            this.Opacity = 0.01;

            this.MouseClick += selecionarTrava;
        }

        private void selecionarTrava(object sender, MouseEventArgs e)
        {
            this.MouseClick -= selecionarTrava;
            Point posicao = PointToClient(Cursor.Position);
            y = posicao.Y;
            //MessageBox.Show(y.ToString());
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            button1.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Opacity = 1;
            label1.Visible = true;
        }
        private void codigoPrincipal()
        {

            ativado = !ativado;
            if (ativado)
            {
                i = 0;
                this.Invoke((MethodInvoker)delegate
                {
                    this.WindowState = FormWindowState.Minimized;
                });
                while (i == 0)
                {
                    int x = Cursor.Position.X;
                    Cursor.Position = new Point(x, y);
                    Thread.Sleep((int)1); // Velocidade abaixo de 1milisegundo afeta a posição (X) do mouse
                }
            }
            else
            {
                i = 1;
                this.Invoke((MethodInvoker)delegate
                {
                    this.WindowState = FormWindowState.Normal;
                });
            }
        }
    }
}
