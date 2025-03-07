using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Common;
using Entity_Forms.Data;
using Entity_Forms.Services;
using Entity_Forms.Models;

namespace Entity_Forms
{
    public class Run
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private System.ComponentModel.IContainer? components = null;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private int hoveredIndex = -1;  // Guarda o índice da aba que está em hover

        public MainForm()
        {
            this.Text = "MULTI-CRUD";
            this.Size = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            tabControl = new TabControl { Dock = DockStyle.Fill };

            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem!;
            tabControl.MouseMove += TabControl_MouseMove!;
            tabControl.MouseLeave += TabControl_MouseLeave!;

            tabPage1 = new TabPage { Text = "Usuários"};
            tabPage2 = new TabPage { Text = "Máquinas"};
            tabPage3 = new TabPage { Text = "Softwares"};

            UserCrud crudUser = new UserCrud();
            crudUser.TopLevel = false;
            crudUser.FormBorderStyle = FormBorderStyle.None;  // Remove bordas
            crudUser.Dock = DockStyle.Fill;  // Faz o form ocupar toda a TabPage
            tabPage1.Controls.Add(crudUser);
            crudUser.Show();

            MaquinaCrud crudMaquina = new MaquinaCrud();
            crudMaquina.TopLevel = false;
            crudMaquina.FormBorderStyle = FormBorderStyle.None;  // Remove bordas
            crudMaquina.Dock = DockStyle.Fill;  // Faz o form ocupar toda a TabPage
            tabPage2.Controls.Add(crudMaquina);
            crudMaquina.Show();

            SoftwareCrud crudSoftware = new SoftwareCrud();
            crudSoftware.TopLevel = false;
            crudSoftware.FormBorderStyle = FormBorderStyle.None;  // Remove bordas
            crudSoftware.Dock = DockStyle.Fill;  // Faz o form ocupar toda a TabPage
            tabPage3.Controls.Add(crudSoftware);
            crudSoftware.Show();

            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);

            this.Controls.Add(tabControl);

        }

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

    #region MAQUINA
    public class MaquinaCrud : Form
    {
        private System.ComponentModel.IContainer? components = null;

        private CrudMaquina crud;
        
        private Label labelId;
        private Label labelTipo;
        private Label labelVelocidade;
        private Label labelHd;
        private Label labelRede;
        private Label labelRam;
        private Label labelUser;
        private TextBox textBoxId;
        private TextBox textBoxTipo;
        private TextBox textBoxVelocidade;
        private TextBox textBoxHd;
        private TextBox textBoxRede;
        private TextBox textBoxRam;
        private TextBox textBoxUser;
        private Button buttonClear;
        private Button buttonInsert;
        private Button buttonUpdate;
        private Button buttonSearch;
        private Button buttonDelete;
        private ListBox listBoxMaquinas;

        public MaquinaCrud()
        {
            this.Text = "My Form";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#CDCDCD");
            this.ForeColor = ColorTranslator.FromHtml("#1132FF");


            crud = new CrudMaquina();

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Font fontAlternativa = new Font("Arial", 12, FontStyle.Bold);
            int x = 50, y = 45, labelW = 90, labelH = 25, textBoxW = 110, textBoxH = 25;

            #region Labels
            
            this.labelId = new Label {
                Text = "ID",
                AutoSize = true,
                Location = new Point(x, y),
                Name = "labelId",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelId);

            this.labelTipo = new Label {
                Text = "Tipo",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelTipo",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelTipo);

            this.labelVelocidade = new Label {
                Text = "Velocidade",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelVelocidade",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelVelocidade);

            this.labelHd = new Label {
                Text = "Hd",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelHd",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelHd);

            this.labelRede = new Label {
                Text = "Rede",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelRede",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelRede);

            this.labelRam = new Label {
                Text = "Ram",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelRam",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelRam);

            this.labelUser = new Label {
                Text = "User",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelUser",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelUser);

            #endregion

            #region TextBox
            x = 50;
            y = 80;

            // Outra forma de definir o elemento sem ter que inicializar lá em cima
            this.textBoxId = new TextBox {Location = new Point(x, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxTipo = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxVelocidade = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxHd = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxRede = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxRam = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxUser = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};

            this.Controls.Add(textBoxId);
            this.Controls.Add(this.textBoxTipo);
            this.Controls.Add(this.textBoxVelocidade);
            this.Controls.Add(this.textBoxHd);
            this.Controls.Add(this.textBoxRede);
            this.Controls.Add(this.textBoxRam);
            this.Controls.Add(this.textBoxUser);

            #endregion

            #region Buttons

            this.buttonInsert = CriarBotao("Inserir", new Point(180, 150), Color.LightBlue);
            this.buttonUpdate = CriarBotao("Atualizar", new Point(310, 150), Color.LightGreen);
            this.buttonSearch = CriarBotao("Buscar", new Point(460, 150), Color.LightYellow);
            this.buttonDelete = CriarBotao("Deletar", new Point(600, 150), Color.LightSalmon);
            this.buttonClear = CriarBotao("Limpar", new Point(760, 150), Color.LightPink);

            this.Controls.Add(buttonInsert);
            this.Controls.Add(buttonUpdate);
            this.Controls.Add(buttonDelete);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonClear);

            // Craindo enventos dos botões
            buttonInsert.Click += new EventHandler(ButtonInserir_Click!);
            buttonSearch.Click += new EventHandler(ButtonListar_Click!);
            buttonUpdate.Click += new EventHandler(ButtonAtualizar_Click!);
            buttonDelete.Click += new EventHandler(ButtonDeletar_Click!);
            buttonClear.Click += new EventHandler(ButtonClear_Click!);

            #endregion

            #region ListBox
            
            listBoxMaquinas = new ListBox
            {
                Location = new Point((this.Width - 700) / 2, 200),
                Width = 700,
                Height = 200,
                BackColor = Color.White, // Cor de fundo
                ForeColor = Color.Blue, // ForeColor é a cor da fonte
                MultiColumn = true,
                SelectionMode = SelectionMode.MultiExtended
            };

            this.Controls.Add(this.listBoxMaquinas);

            #endregion

            ButtonListar_Click(this, EventArgs.Empty);
        }
        private Button CriarBotao(string texto, Point localizacao, Color cor)
        {
            return new Button
            {
                Text = texto,
                Location = localizacao,
                Width = 100,
                Height = 30,
                BackColor = Color.Black,
                ForeColor = cor,
                Font = new Font("Arial", 12, FontStyle.Underline)
            };
        }

        private void ButtonInserir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                string tipo = textBoxTipo.Text;
                int velocidade = int.Parse(textBoxVelocidade.Text);
                int hd = int.Parse(textBoxHd.Text);
                int rede = int.Parse(textBoxRede.Text);
                int ram = int.Parse(textBoxRam.Text);
                int user = int.Parse(textBoxUser.Text);
                
                crud.Inserir(id, tipo, velocidade, hd, rede, ram, user);
                MessageBox.Show("Máquina inserida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonListar_Click(sender, e);
                LimparCampos();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao inserir Máquina!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            textBoxId.Clear();
            textBoxTipo.Clear();
            textBoxVelocidade.Clear();
            textBoxHd.Clear();
            textBoxRede.Clear();
            textBoxRam.Clear();
            textBoxUser.Clear();
        }

        private void ButtonAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                string tipo = textBoxTipo.Text;
                int velocidade = int.Parse(textBoxVelocidade.Text);
                int hd = int.Parse(textBoxHd.Text);
                int rede = int.Parse(textBoxRede.Text);
                int ram = int.Parse(textBoxRam.Text);
                int user = int.Parse(textBoxUser.Text);
                
                crud.Atualizar(id, tipo, velocidade, hd, rede, ram, user);
                MessageBox.Show("Máquina atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonListar_Click(sender, e);
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar Máquina!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonListar_Click (object sender, EventArgs e)
        {
            try
            {
                listBoxMaquinas.Items.Clear();
                List<string>? maquinas = crud.Listar();
                if (maquinas != null)
                {
                    foreach (var maquina in maquinas)
                    {
                        listBoxMaquinas.Items.Add(maquina);
                    }
                    // MessageBox.Show("Usuários listado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("Nenhuma máquina cadastrado");
                }
                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Erro ao listar Máquinas!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                crud.Deletar(id);
                LimparCampos();
                MessageBox.Show("Máquina excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao excluir Máquina!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e) => LimparCampos();
        
    }
    #endregion

    #region SOFTWARE
    public class SoftwareCrud : Form
    {
        private System.ComponentModel.IContainer? components = null;

        private CrudSoftware crud;
        
        private Label labelId;
        private Label labelProduto;
        private Label labelHd;
        private Label labelRam;
        private Label labelMaquina;
        private TextBox textBoxId;
        private TextBox textBoxProduto;
        private TextBox textBoxHd;
        private TextBox textBoxRam;
        private TextBox textBoxMaquina;
        private Button buttonClear;
        private Button buttonInsert;
        private Button buttonUpdate;
        private Button buttonSearch;
        private Button buttonDelete;
        private ListBox listBoxSoftwares;

        public SoftwareCrud()
        {
            this.Text = "My Form";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#CDCDCD");
            this.ForeColor = ColorTranslator.FromHtml("#1132FF");


            crud = new CrudSoftware();

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Font fontAlternativa = new Font("Arial", 12, FontStyle.Bold);
            int x = 100, y = 45, labelW = 90, labelH = 25, textBoxW = 110, textBoxH = 25;

            #region Labels
            
            this.labelId = new Label {
                Text = "ID",
                AutoSize = true,
                Location = new Point(x, y),
                Name = "labelId",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelId);

            this.labelProduto = new Label {
                Text = "Produto",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelProduto",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelProduto);

            this.labelHd = new Label {
                Text = "Hd",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelHd",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelHd);

            this.labelRam = new Label {
                Text = "Ram",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelRam",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelRam);

            this.labelMaquina = new Label {
                Text = "Maquina",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelMaquina",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
            };
            this.Controls.Add(this.labelMaquina);

            #endregion

            #region TextBox
            x = 100;
            y = 80;

            // Outra forma de definir o elemento sem ter que inicializar lá em cima
            this.textBoxId = new TextBox {Location = new Point(x, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxProduto = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxHd = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxRam = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxMaquina = new TextBox {Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa};

            this.Controls.Add(textBoxId);
            this.Controls.Add(this.textBoxProduto);
            this.Controls.Add(this.textBoxHd);
            this.Controls.Add(this.textBoxRam);
            this.Controls.Add(this.textBoxMaquina);

            #endregion

            #region Buttons

            this.buttonInsert = CriarBotao("Inserir", new Point(180, 150), Color.LightBlue);
            this.buttonUpdate = CriarBotao("Atualizar", new Point(310, 150), Color.LightGreen);
            this.buttonSearch = CriarBotao("Buscar", new Point(460, 150), Color.LightYellow);
            this.buttonDelete = CriarBotao("Deletar", new Point(600, 150), Color.LightSalmon);
            this.buttonClear = CriarBotao("Limpar", new Point(760, 150), Color.LightPink);

            this.Controls.Add(buttonInsert);
            this.Controls.Add(buttonUpdate);
            this.Controls.Add(buttonDelete);
            this.Controls.Add(buttonSearch);
            this.Controls.Add(buttonClear);

            // Craindo enventos dos botões
            buttonInsert.Click += new EventHandler(ButtonInserir_Click!);
            buttonSearch.Click += new EventHandler(ButtonListar_Click!);
            buttonUpdate.Click += new EventHandler(ButtonAtualizar_Click!);
            buttonDelete.Click += new EventHandler(ButtonDeletar_Click!);
            buttonClear.Click += new EventHandler(ButtonClear_Click!);

            #endregion

            #region ListBox
            
            listBoxSoftwares = new ListBox
            {
                Location = new Point((this.Width - 700) / 2, 200),
                Width = 700,
                Height = 200,
                BackColor = Color.White, // Cor de fundo
                ForeColor = Color.Blue, // ForeColor é a cor da fonte
                MultiColumn = true,
                SelectionMode = SelectionMode.MultiExtended
            };

            this.Controls.Add(this.listBoxSoftwares);

            #endregion

            ButtonListar_Click(this, EventArgs.Empty);
        }
        private Button CriarBotao(string texto, Point localizacao, Color cor)
        {
            return new Button
            {
                Text = texto,
                Location = localizacao,
                Width = 100,
                Height = 30,
                BackColor = Color.Black,
                ForeColor = cor,
                Font = new Font("Arial", 12, FontStyle.Underline)
            };
        }

        private void ButtonInserir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                string produto = textBoxProduto.Text;
                int hd = int.Parse(textBoxHd.Text);
                int ram = int.Parse(textBoxRam.Text);
                int maquina = int.Parse(textBoxMaquina.Text);
                
                crud.Inserir(id, produto, hd, ram, maquina);
                MessageBox.Show("Software inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonListar_Click(sender, e);
                LimparCampos();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao inserir Software!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            textBoxId.Clear();
            textBoxProduto.Clear();
            textBoxHd.Clear();
            textBoxRam.Clear();
            textBoxMaquina.Clear();
        }

        private void ButtonAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                string produto = textBoxProduto.Text;
                int hd = int.Parse(textBoxHd.Text);
                int ram = int.Parse(textBoxRam.Text);
                int maquina = int.Parse(textBoxMaquina.Text);
                
                crud.Atualizar(id, produto, hd, ram, maquina);
                MessageBox.Show("Software atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonListar_Click(sender, e);
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar Software!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonListar_Click (object sender, EventArgs e)
        {
            try
            {
                listBoxSoftwares.Items.Clear();
                List<string>? softwares = crud.Listar();
                if (softwares != null)
                {
                    foreach (var software in softwares)
                    {
                        listBoxSoftwares.Items.Add(software);
                    }
                    // MessageBox.Show("Usuários listado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("Nenhum software cadastrado");
                }
                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Erro ao listar Softwares!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                crud.Deletar(id);
                LimparCampos();
                MessageBox.Show("Software excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao excluir Software!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e) => LimparCampos();
        
        
    }
    #endregion

    #region USER
    public class UserCrud : Form
    {
        private System.ComponentModel.IContainer? components = null;

        private CrudUser crud;
        
        private Label labelId;
        private Label labelNome;
        private Label labelSenha;
        private Label labelRamal;
        private Label labelEspecialidade;
        private TextBox textBoxId;
        private TextBox textBoxNome;
        private TextBox textBoxSenha;
        private TextBox textBoxRamal;
        private TextBox textBoxEspecialidade;
        private Button buttonClear;
        private Button buttonInsert;
        private Button buttonUpdate;
        private Button buttonSearch;
        private Button buttonDelete;
        private ListBox listBoxUsuarios;

        public UserCrud()
        {
            this.Text = "My Form";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#CDCDCD");
            this.ForeColor = ColorTranslator.FromHtml("#1132FF");

            this.labelId = new Label();
            this.labelNome = new Label();
            this.labelSenha = new Label();
            this.labelRamal = new Label();
            this.labelEspecialidade = new Label();

            this.buttonClear = new Button();
            this.buttonInsert = new Button();
            this.buttonUpdate = new Button();
            this.buttonSearch = new Button();
            this.buttonDelete = new Button();

            this.listBoxUsuarios = new ListBox();

            crud = new CrudUser();

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Font fontAlternativa = new Font("Arial", 12, FontStyle.Bold);

            #region Labels

            this.labelId.Text = "ID";
            this.labelId.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelId.Location = new Point(100, 45);
            this.labelId.Name = "labelId";
            this.labelId.Size = new Size(90, 20);
            this.labelId.Font = font;
            this.labelId.ForeColor = Color.Blue;
            this.Controls.Add(this.labelId);
            
            this.labelNome.Text = "Nome";
            this.labelNome.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelNome.Location = new Point(250, 45);
            this.labelNome.Name = "labelNome";
            this.labelNome.Size = new Size(90, 20);
            this.labelNome.Font = font;
            this.labelNome.ForeColor = Color.Blue;
            this.Controls.Add(this.labelNome);
            
            this.labelSenha.Text = "Senha";
            this.labelSenha.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelSenha.Location = new Point(400, 45);
            this.labelSenha.Name = "labelSenha";
            this.labelSenha.Size = new Size(90, 20);
            this.labelSenha.Font = font;
            this.labelSenha.ForeColor = Color.Blue;
            this.Controls.Add(this.labelSenha);
            
            this.labelRamal.Text = "Ramal";
            this.labelRamal.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelRamal.Location = new Point(550, 45);
            this.labelRamal.Name = "labelRamal";
            this.labelRamal.Size = new Size(90, 20);
            this.labelRamal.Font = font;
            this.labelRamal.ForeColor = Color.Blue;
            this.Controls.Add(this.labelRamal);
            
            this.labelEspecialidade.Text = "Especialidade";
            this.labelEspecialidade.AutoSize = true;    // O texto se adapta ao tamanho da janela
            this.labelEspecialidade.Location = new Point(700, 45);
            this.labelEspecialidade.Name = "labelEspecialidade";
            this.labelEspecialidade.Size = new Size(90, 20);
            this.labelEspecialidade.Font = font;
            this.labelEspecialidade.ForeColor = Color.Blue;
            this.Controls.Add(this.labelEspecialidade);

            #endregion

            #region TextBox

            // Outra forma de definir o elemento sem ter que inicializar lá em cima
            this.textBoxId = new TextBox {Location = new Point(110, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxNome = new TextBox {Location = new Point(260, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxSenha = new TextBox {Location = new Point(410, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxRamal = new TextBox {Location = new Point(560, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa};
            this.textBoxEspecialidade = new TextBox {Location = new Point(700, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa};

            this.Controls.Add(textBoxId);
            this.Controls.Add(this.textBoxNome);
            this.Controls.Add(this.textBoxSenha);
            this.Controls.Add(this.textBoxRamal);
            this.Controls.Add(this.textBoxEspecialidade);

            #endregion

            #region Buttons

            this.buttonInsert = CriarBotao("Inserir", new Point(180, 150), Color.LightBlue);
            this.buttonUpdate = CriarBotao("Atualizar", new Point(310, 150), Color.LightGreen);
            this.buttonSearch = CriarBotao("Buscar", new Point(460, 150), Color.LightYellow);
            this.buttonDelete = CriarBotao("Deletar", new Point(600, 150), Color.LightSalmon);
            this.buttonClear = CriarBotao("Limpar", new Point(760, 150), Color.LightPink);

            this.Controls.Add(buttonInsert);
            this.Controls.Add(buttonUpdate);
            this.Controls.Add(buttonDelete);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonClear);

            // Craindo enventos dos botões
            buttonInsert.Click += new EventHandler(ButtonInserir_Click!);
            buttonSearch.Click += new EventHandler(ButtonListar_Click!);
            buttonUpdate.Click += new EventHandler(ButtonAtualizar_Click!);
            buttonDelete.Click += new EventHandler(ButtonDeletar_Click!);
            buttonClear.Click += new EventHandler(ButtonClear_Click!);

            #endregion

            #region ListBox
            
            listBoxUsuarios = new ListBox
            {
                Location = new Point(110, 200),
                Width = 700,
                Height = 200,
                BackColor = Color.White, // Cor de fundo
                ForeColor = Color.Blue, // ForeColor é a cor da fonte
                MultiColumn = true,
                SelectionMode = SelectionMode.MultiExtended
            };

            this.Controls.Add(this.listBoxUsuarios);

            #endregion

            ButtonListar_Click(this, EventArgs.Empty);
        }
        private Button CriarBotao(string texto, Point localizacao, Color cor)
        {
            return new Button
            {
                Text = texto,
                Location = localizacao,
                Width = 100,
                Height = 30,
                BackColor = Color.Black,
                ForeColor = cor,
                Font = new Font("Arial", 12, FontStyle.Underline)
            };
        }

        private void ButtonInserir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                string nome = textBoxNome.Text;
                string senha = textBoxSenha.Text;
                int ramal = int.Parse(textBoxRamal.Text);
                string especialidade = textBoxEspecialidade.Text;
                crud.Inserir(id, nome, senha, ramal, especialidade);
                MessageBox.Show("Usuário inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonListar_Click(sender, e);
                LimparCampos();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao inserir Usuário!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            textBoxId.Clear();
            textBoxNome.Clear();
            textBoxSenha.Clear();
            textBoxRamal.Clear();
            textBoxEspecialidade.Clear();
        }

        private void ButtonAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                string Tipo = textBoxNome.Text;
                string senha = textBoxSenha.Text;
                int ramal = int.Parse(textBoxRamal.Text);
                string especialidade = textBoxEspecialidade.Text;

                crud.Atualizar(id, Tipo, senha, ramal, especialidade);
                MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonListar_Click(sender, e);
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar Usuário!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonListar_Click (object sender, EventArgs e)
        {
            try
            {
                listBoxUsuarios.Items.Clear();
                List<string>? usuarios = crud.Listar();
                if (usuarios != null)
                {
                    foreach (var usuario in usuarios)
                    {
                        listBoxUsuarios.Items.Add(usuario);
                    }
                    // MessageBox.Show("Usuários listado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("Nenhum usuário cadastrado");
                }
                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Erro ao listar Usuários!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxId.Text);
                crud.Deletar(id);
                LimparCampos();
                MessageBox.Show("Usuários excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao excluir Usuário!", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e) => LimparCampos();
        
    }
    #endregion
}