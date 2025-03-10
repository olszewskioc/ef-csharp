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
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Entity_Forms
{
    public class Run
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Form1 : Form
    {
        // Declarando os controles
        private TableLayoutPanel tableLayoutPanel;
        private Button buttonAdicionar;
        private Button buttonAtualizar;
        private Button buttonExcluir;
        private ListBox listBox;
        private Label labelTitle;

        // Construtor do formulário
        public Form1()
        {
            // Inicializando os componentes
            InitializeComponent();
            InitializeLayout();
        }

        // Método para inicializar os componentes
        private void InitializeComponent()
        {
            // Definindo as propriedades iniciais do formulário
            this.Icon = new System.Drawing.Icon("icon.ico");
            this.Text = "Exemplo CRUD com TableLayoutPanel";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(300, 400);
            this.Resize += Form1_Resize;
        }

        // Método para inicializar o layout
        private async Task InitializeLayout()
        {
            // Criando o TableLayoutPanel
            tableLayoutPanel = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                // Padding = new Padding(10),
                BackColor = Color.LightGray
            };

            // Definindo o tamanho das colunas e linhas
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 50% para a primeira coluna
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 50% para a segunda coluna

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));  // Título
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Botões
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // ListBox ocupa o restante
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Botões de ação
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 1));  // Botões de ação
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Botões de ação

            // Criando os controles
            labelTitle = new Label
            {
                Text = "CRUD Simples",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.Black,
                BackColor = Color.CornflowerBlue,
            };

            buttonAdicionar = await CreateButton("Adicionar", Color.LightBlue, "https://cdn-icons-png.flaticon.com/512/992/992651.png");
            buttonAtualizar = await CreateButton("Atualizar", Color.LightGreen, "https://cdn-icons-png.flaticon.com/512/1087/1087080.png");
            buttonExcluir = await CreateButton("Excluir", Color.LightCoral, "https://cdn-icons-png.flaticon.com/512/1214/1214428.png");
            listBox = new ListBox
            {
                Name = "listBox",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Adicionando os controles ao TableLayoutPanel
            tableLayoutPanel.Controls.Add(labelTitle, 0, 0);
            tableLayoutPanel.SetColumnSpan(labelTitle, 2);  // Ocupa as duas colunas

            tableLayoutPanel.Controls.Add(buttonAdicionar, 0, 1);
            tableLayoutPanel.SetColumnSpan(buttonAdicionar, 2);
            tableLayoutPanel.Controls.Add(buttonAtualizar, 0, 2);
            tableLayoutPanel.SetColumnSpan(buttonAtualizar, 2);
            tableLayoutPanel.Controls.Add(buttonExcluir, 0, 5);
            tableLayoutPanel.SetColumnSpan(buttonExcluir, 2);

            tableLayoutPanel.Controls.Add(listBox, 0, 2);
            tableLayoutPanel.SetColumnSpan(listBox, 2); // ListBox ocupa as duas colunas

            // Adicionando o TableLayoutPanel ao formulário
            Controls.Add(tableLayoutPanel);
        }

        // Método para criar um botão com estilização padronizada
        private async Task<Button> CreateButton(string text, Color color, string url)   // Task para utilizar await e assim não travar o programa
        {
            Button botao = new Button
            {
                Text = text,
                Font = new Font("Arial", 12),
                BackColor = color,
                Dock = DockStyle.Fill,
                // Padding = new Padding(5),
                Height = 40,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleRight,
            };

            try
            {
                using (var client = new HttpClient())   // Usando HttpClient para fazer requisição
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(url);    // Baixando a imagem

                    using (var ms = new MemoryStream(imageBytes))   // Convertendo a imagem em um MemoryStream
                    {
                        botao.Image = Image.FromStream(ms); // Adicionando a imagem ao botão
                        botao.Image = new Bitmap(botao.Image, new Size(30, 30)); // Redimensionamento direto
                    }
                }
            }
            catch (Exception ex)
            {
                // Exibindo uma mensagem amigável ao usuário e aplicando uma imagem padrão
                MessageBox.Show($"Erro ao carregar imagem do botão {text}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return botao;
        }

        // Evento de redimensionamento do formulário
        private void Form1_Resize(object sender, EventArgs e)
        {
            // Aqui podemos realizar ajustes quando o formulário for redimensionado
            // O TableLayoutPanel e seus controles já são redimensionados automaticamente
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

            tabPage1 = new TabPage { Text = "Usuários" };
            tabPage2 = new TabPage { Text = "Máquinas" };
            tabPage3 = new TabPage { Text = "Softwares" };

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

            foreach (TabPage page in tabControl.TabPages)
            {
                page.BackColor = Color.Transparent;
            }

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
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "bg_machine.jpg");
            // string imagePath = @"C:\Users\thiagocarvalho\Documents\Digix\ef-chsarp\WindowsForms\Entity_Forms\Assets\bg_machine.jpg";

            // Verifica se a imagem existe antes de tentar carregar
            if (File.Exists(imagePath))
            {
                try
                {
                    Image bg = new Bitmap(imagePath);
                    this.BackgroundImage = bg;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show($"Imagem não encontrada no caminho: {imagePath}");
            }
            this.Text = "My Form";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            // this.BackColor = ColorTranslator.FromHtml("#CDCDCD");
            this.ForeColor = ColorTranslator.FromHtml("#1132FF");


            crud = new CrudMaquina();

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Font fontAlternativa = new Font("Arial", 12, FontStyle.Bold);
            int x = 50, y = 45, labelW = 90, labelH = 25, offset = 50, textBoxW = 110, textBoxH = 25;

            #region Labels

            // Criando labels dinamicamente
            labelId = CriarLabel("ID", x, y, font);
            labelTipo = CriarLabel("Tipo", x += labelW + offset, y, font);
            labelVelocidade = CriarLabel("Velocidade", x += labelW + offset, y, font);
            labelHd = CriarLabel("Hd", x += labelW + offset, y, font);
            labelRede = CriarLabel("Rede", x += labelW + offset, y, font);
            labelRam = CriarLabel("Ram", x += labelW + offset, y, font);
            labelUser = CriarLabel("User", x += labelW + offset, y, font);

            // Definir todas as labels como transparentes
            foreach (var label in Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
            }
            #endregion

            #region TextBox
            x = 50;
            y = 80;

            // Outra forma de definir o elemento sem ter que inicializar lá em cima
            this.textBoxId = new TextBox { Location = new Point(x, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxTipo = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxVelocidade = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxHd = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxRede = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxRam = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxUser = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };

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

        private Label CriarLabel(string texto, int x, int y, Font font)
        {
            Label label = new Label
            {
                Text = texto,
                AutoSize = true,
                Location = new Point(x, y),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#1132FF"),
                BackColor = Color.Transparent
            };
            this.Controls.Add(label);
            return label;
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

        private void ButtonListar_Click(object sender, EventArgs e)
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
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "bg_software.jpg");
            // string imagePath = @"C:\Users\thiagocarvalho\Documents\Digix\ef-chsarp\WindowsForms\Entity_Forms\Assets\bg_machine.jpg";

            // Verifica se a imagem existe antes de tentar carregar
            if (File.Exists(imagePath))
            {
                try
                {
                    Image bg = new Bitmap(imagePath);
                    this.BackgroundImage = bg;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show($"Imagem não encontrada no caminho: {imagePath}");
            }
            this.Text = "My Form";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#CDCDCD");
            this.ForeColor = ColorTranslator.FromHtml("#FFFFF");


            crud = new CrudSoftware();

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Font fontAlternativa = new Font("Arial", 12, FontStyle.Bold);
            int x = 100, y = 45, labelW = 90, labelH = 25, textBoxW = 110, textBoxH = 25;


            #region Painel para agrupar os Labels e TextBoxes

            Panel panelContainer = new Panel
            {
                Location = new Point((this.ClientSize.Width - 800) / 2, 30),
                Size = new Size(800, 150), // Tamanho do painel
                BackColor = Color.Transparent // Para não afetar o background do form
            };

            // Centraliza o painel no formulário
            panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            panelContainer.Top = 5;

            this.Controls.Add(panelContainer);

            #endregion

            #region Labels

            this.labelId = new Label
            {
                Text = "ID",
                AutoSize = true,
                Location = new Point(x, y),
                Name = "labelId",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#FFFFF"),
            };
            panelContainer.Controls.Add(this.labelId);

            this.labelProduto = new Label
            {
                Text = "Produto",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelProduto",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#FFFFF"),
            };
            panelContainer.Controls.Add(this.labelProduto);

            this.labelHd = new Label
            {
                Text = "Hd",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelHd",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#FFFFF"),
            };
            panelContainer.Controls.Add(this.labelHd);

            this.labelRam = new Label
            {
                Text = "Ram",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelRam",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#FFFFF"),
            };
            panelContainer.Controls.Add(this.labelRam);

            this.labelMaquina = new Label
            {
                Text = "Maquina",
                AutoSize = true,
                Location = new Point(x += labelW + 50, y),
                Name = "labelMaquina",
                Size = new Size(labelW, labelH),
                Font = font,
                ForeColor = ColorTranslator.FromHtml("#FFFFF"),
            };
            panelContainer.Controls.Add(this.labelMaquina);

            foreach (var label in Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
            }

            #endregion

            #region TextBox
            x = 100;
            y = 80;

            // Outra forma de definir o elemento sem ter que inicializar lá em cima
            this.textBoxId = new TextBox { Location = new Point(x, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxProduto = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxHd = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxRam = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxMaquina = new TextBox { Location = new Point(x += textBoxW + 30, y), Width = textBoxW, Height = textBoxH, Name = "textBoxId", Font = fontAlternativa };

            panelContainer.Controls.Add(textBoxId);
            panelContainer.Controls.Add(this.textBoxProduto);
            panelContainer.Controls.Add(this.textBoxHd);
            panelContainer.Controls.Add(this.textBoxRam);
            panelContainer.Controls.Add(this.textBoxMaquina);

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
                Location = new Point((this.Width - 800) / 2, 200),
                Width = 800,
                Height = 200,
                BackColor = Color.White, // Cor de fundo
                ForeColor = Color.Blue, // ForeColor é a cor da fonte
                MultiColumn = true,
                SelectionMode = SelectionMode.MultiExtended
            };

            // Ajusta a posição inicial
            listBoxSoftwares.Left = (this.ClientSize.Width - listBoxSoftwares.Width) / 2;
            listBoxSoftwares.Top = 200;

            this.Controls.Add(this.listBoxSoftwares);

            // Ajusta a posição ao redimensionar
            this.Resize += (s, e) =>
            {
                listBoxSoftwares.Left = (this.ClientSize.Width - listBoxSoftwares.Width) / 2;
                panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            };

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

        private void ButtonListar_Click(object sender, EventArgs e)
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
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "bg_user.jpg");
            // string imagePath = @"C:\Users\thiagocarvalho\Documents\Digix\ef-chsarp\WindowsForms\Entity_Forms\Assets\bg_machine.jpg";

            // Verifica se a imagem existe antes de tentar carregar
            if (File.Exists(imagePath))
            {
                try
                {
                    Image bg = new Bitmap(imagePath);
                    this.BackgroundImage = bg;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show($"Imagem não encontrada no caminho: {imagePath}");
            }
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

            foreach (var label in Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
            }

            #endregion

            #region TextBox

            // Outra forma de definir o elemento sem ter que inicializar lá em cima
            this.textBoxId = new TextBox { Location = new Point(110, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxNome = new TextBox { Location = new Point(260, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxSenha = new TextBox { Location = new Point(410, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxRamal = new TextBox { Location = new Point(560, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa };
            this.textBoxEspecialidade = new TextBox { Location = new Point(700, 80), Width = 110, Height = 20, Name = "textBoxId", Font = fontAlternativa };

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

        private void ButtonListar_Click(object sender, EventArgs e)
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