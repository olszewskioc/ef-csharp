using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Exemplo_Console_Forms
{
    public class Run
    {
        // STAThread: permite que o aplicativo console seja executado em uma thread de interface
        [STAThread] // Necessário para que o programa possa criar janelas
        static void Main(string[] args)
        {
            // Application é a classe que gerencia as janelas
            Application.EnableVisualStyles(); // Habilita o uso de estilos visuais
        
            Application.SetCompatibleTextRenderingDefault(false); // Define o valor padrão para a renderização de texto
        
            Application.Run(new MainForm());
        
        }
    }

    public class MyForm : Form
    {
        public MyForm()
        {
            this.Text = "My Forms";
            this.Size = new Size(300, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label();
            label.Text = "Hello World";
            label.Location = new Point(100, 100);
            label.Name = "label";
            label.Size = new Size(100, 20);
            this.AutoSize = true;

            this.Controls.Add(label);

        }
    }

    #region Calculadora
    public class Calculadora : Form
    {
        private System.ComponentModel.IContainer? components = null;
        private Label labelNum1;
        private Label labelNum2;
        private TextBox textBoxNum1;
        private TextBox textBoxNum2;
        private Button buttonAdd;
        private Button buttonSub;
        private Button buttonMul;
        private Button buttonDiv;
        private Button buttonClear;

        public Calculadora ()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "My Calculator";
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#CDCDCD");
            this.ForeColor = ColorTranslator.FromHtml("#1132FF");


            this.buttonAdd = new Button();
            this.buttonSub = new Button();
            this.buttonMul = new Button();
            this.buttonDiv = new Button();
            this.buttonClear = new Button();
            this.textBoxNum1 = new TextBox();
            this.textBoxNum2 = new TextBox();
            this.labelNum1 = new Label();
            this.labelNum2 = new Label();

            #region Labels

            this.labelNum1.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelNum1.Location = new System.Drawing.Point(100, 30);
            this.labelNum1.Name = "labelNum1";
            this.labelNum1.Size = new System.Drawing.Size(90, 20); // Tamanho do label
            this.labelNum1.Text = "Digite um número: ";    // Define o texto do label
        
            this.Controls.Add(this.labelNum1);

            this.labelNum2.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelNum2.Location = new System.Drawing.Point(250, 30);
            this.labelNum2.Name = "labelNum2";
            this.labelNum2.Size = new System.Drawing.Size(90, 20); // Tamanho do label
            this.labelNum2.Text = "Digite um número: ";    // Define o texto do label
        
            this.Controls.Add(this.labelNum2);

            #endregion

            #region Text Boxes

            this.textBoxNum1.AutoSize = true;
            this.textBoxNum1.Location = new Point(120, 50);
            this.textBoxNum1.Name = "textBoxNum1";
            this.textBoxNum1.Size = new Size(100, 25); // Tamanho do

            this.Controls.Add(this.textBoxNum1);

            this.textBoxNum2.AutoSize = true;
            this.textBoxNum2.Location = new Point(270, 50);
            this.textBoxNum2.Name = "textBoxNum2";
            this.textBoxNum2.Size = new Size(100, 25); // Tamanho do

            this.Controls.Add(this.textBoxNum2);

            #endregion

            #region Buttons

            // Add Button
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.Location = new Point(35, 95);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Text = "Somar";
            this.buttonAdd.Size = new Size(100, 25); // Tamanho do
            this.buttonAdd.BackColor = Color.DarkGreen;
            this.buttonAdd.ForeColor = Color.White;

            this.buttonAdd.Click += new EventHandler(this.ButtonAdd_Click); // Evento para tratar o click do botõ
            
            this.Controls.Add(this.buttonAdd);
            // Sub Button
            this.buttonSub.AutoSize = true;
            this.buttonSub.Location = new Point(140, 95);
            this.buttonSub.Name = "buttonSub";
            this.buttonSub.Text = "Subtrair";
            this.buttonSub.Size = new Size(100, 25); // Tamanho do
            this.buttonSub.BackColor = Color.DarkKhaki;
            this.buttonSub.ForeColor = Color.White;

            this.buttonSub.Click += new EventHandler(this.ButtonSub_Click); // Evento para tratar o click do botõ
            
            this.Controls.Add(this.buttonSub);

            // Mul Button
            this.buttonMul.AutoSize = true;
            this.buttonMul.Location = new Point(250, 95);
            this.buttonMul.Name = "buttonMul";
            this.buttonMul.Text = "Multiplicar";
            this.buttonMul.Size = new Size(100, 25); // Tamanho do
            this.buttonMul.BackColor = Color.DarkRed;
            this.buttonMul.ForeColor = Color.White;

            this.buttonMul.Click += new EventHandler(this.ButtonMul_Click); // Evento para tratar o click do botõ
            
            this.Controls.Add(this.buttonMul);

            // Div Button
            this.buttonDiv.AutoSize = true;
            this.buttonDiv.Location = new Point(360, 95);
            this.buttonDiv.Name = "buttonDiv";
            this.buttonDiv.Text = "Dividir";
            this.buttonDiv.Size = new Size(100, 25); // Tamanho do
            this.buttonDiv.BackColor = Color.DarkCyan;
            this.buttonDiv.ForeColor = Color.White;

            this.buttonDiv.Click += new EventHandler(this.ButtonDiv_Click); // Evento para tratar o click do botõ
            
            this.Controls.Add(this.buttonDiv);}

            #endregion

            #region Click Events

            private void ButtonAdd_Click (object sender, EventArgs e)
            {
                
                try
                {
                    int num1 = Convert.ToInt32(this.textBoxNum1.Text);
                    int num2 = Convert.ToInt32(this.textBoxNum2.Text);
                    MessageBox.Show($"{num1} + {num2} = {num1 + num2}", "Result");
                } catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Error");
                }
            }
            private void ButtonSub_Click (object sender, EventArgs e)
            {

                try
                {
                    int num1 = Convert.ToInt32(this.textBoxNum1.Text);
                    int num2 = Convert.ToInt32(this.textBoxNum2.Text);
                    MessageBox.Show($"{num1} - {num2} = {num1 - num2}", "Result");
                } catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Error");
                }
            }
            private void ButtonMul_Click (object sender, EventArgs e)
            {
                
                try
                {
                    int num1 = Convert.ToInt32(this.textBoxNum1.Text);
                    int num2 = Convert.ToInt32(this.textBoxNum2.Text);
                    MessageBox.Show($"{num1} x {num2} = {num1 * num2}", "Result");
                } catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Error");
                }
            }
            private void ButtonDiv_Click (object sender, EventArgs e)
            {
                
                try
                {
                    int num1 = Convert.ToInt32(this.textBoxNum1.Text);
                    int num2 = Convert.ToInt32(this.textBoxNum2.Text);
                    MessageBox.Show($"{num1} / {num2} = {num1 / num2}", "Result");
                } catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Error");
                }
            }

            #endregion
    }

    #endregion

    #region Main
    public class MainForm : Form 
    {
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private int hoveredIndex = -1;  // Guarda o índice da aba que está em hover
        public MainForm()
        {
            this.Text = "Multi-task System";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            tabControl = new TabControl { Dock = DockStyle.Fill };

            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed; // Permite desenho personalizado
            tabControl.DrawItem += TabControl_DrawItem;
            tabControl.MouseMove += TabControl_MouseMove;
            tabControl.MouseLeave += TabControl_MouseLeave;

            tabPage1 = new TabPage { Text = "Home" };
            tabPage2 = new TabPage { Text = "Calculadora" };

            // Adicionando MyForm na aba 1
            MyForm myForm = new MyForm();
            myForm.TopLevel = false;  // Permite que o Form seja usado dentro de outro container
            myForm.FormBorderStyle = FormBorderStyle.None;  // Remove bordas
            myForm.Dock = DockStyle.Fill;  // Faz o form ocupar toda a TabPage
            tabPage1.Controls.Add(myForm);
            myForm.Show();

            // Adicionando Calculadora na aba 2
            Calculadora calculadora = new Calculadora();
            calculadora.TopLevel = false;
            calculadora.FormBorderStyle = FormBorderStyle.None;
            calculadora.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(calculadora);
            calculadora.Show();

            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);

            this.Controls.Add(tabControl);
        }

        #endregion

        #region Eventos TabControl

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle tabRect = tabControl.GetTabRect(e.Index);
            Brush backBrush;
            Brush textBrush = Brushes.White;

            // Verifica se é a aba em hover
            if (e.Index == hoveredIndex)
            {
                backBrush = new SolidBrush(Color.DarkGray); // Cor quando o mouse está sobre
            }
            else if (e.Index == tabControl.SelectedIndex)
            {
                backBrush = new SolidBrush(Color.Gray); // Cor da aba ativa
            }
            else
            {
                backBrush = new SolidBrush(Color.DimGray); // Cor padrão
            }

            g.FillRectangle(backBrush, tabRect);
            g.DrawString(tabControl.TabPages[e.Index].Text, this.Font, textBrush, tabRect.X + 10, tabRect.Y + 5);
        }

        private void TabControl_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl.TabCount; i++)
            {
                if (tabControl.GetTabRect(i).Contains(e.Location))
                {
                    if (hoveredIndex != i)
                    {
                        hoveredIndex = i;
                        tabControl.Invalidate(); // Redesenha o TabControl
                    }
                    return;
                }
            }
            hoveredIndex = -1;
            tabControl.Invalidate();
        }

        private void TabControl_MouseLeave(object sender, EventArgs e)
        {
            hoveredIndex = -1;
            tabControl.Invalidate();
        }
        #endregion
    }
}