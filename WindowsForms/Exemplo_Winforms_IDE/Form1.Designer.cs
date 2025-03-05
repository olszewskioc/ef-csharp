namespace Exemplo_Winforms_IDE;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Label label1;      // Classe específica para texto
    private System.Windows.Forms.Button button1;    // Classe específica para botões
    private System.Windows.Forms.TextBox textBox1;  // Classe específica para caixas de texto
    private System.Windows.Forms.Label label2;      // Classe específica para texto
    private System.Windows.Forms.Button button2;    // Classe específica para botões
    private System.Windows.Forms.TextBox textBox2;  // Classe específica para caixas de texto
    private System.Windows.Forms.Button buttonSomar;  // Classe específica para caixas de texto
    

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()  // Chamado para inicializar o formulário
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Iniciar";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // // Instância a classe Label para mexer com o texto
        // Label label = new Label();  // Instância a classe Label
        // label.Text = "Olá, mundo!"; // Define o texto do label
        // label.AutoSize = true;  // O texto se adapta ao tamanho da janela
        // label.Location = new Point(10, 10); // Posiciona o label na tela
    
        // // Adiciona o label ao formulário
        // this.Controls.Add(label);   // Controls é uma coleção de controles do formulário. Adiciona o label a essa coleção
    
        // Inicializar as variáveis criadas para o formulário
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.buttonSomar = new System.Windows.Forms.Button();
        

        #region Label1

        this.label1.AutoSize = true;    // O texto se adapta ao tamanho da janela
        this.label1.Location = new System.Drawing.Point(30, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(90, 20); // Tamanho do label
        this.label1.Text = "Digite um número: ";    // Define o texto do label
        
        this.Controls.Add(this.label1);

        #endregion

        #region TextBox1

        this.textBox1.AutoSize = true;
        this.textBox1.Location = new System.Drawing.Point(35, 50);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(100, 25); // Tamanho do

        this.Controls.Add(this.textBox1);

        #endregion

        #region Button1

        this.button1.AutoSize = true;
        this.button1.Location = new System.Drawing.Point(35, 85);
        this.button1.Name = "button1";
        this.button1.Text = "Gerar Número";
        this.button1.Size = new System.Drawing.Size(100, 25); // Tamanho do

        this.button1.Click += new System.EventHandler(this.Button1_Click); // Evento para tratar o click do botõ
        
        this.Controls.Add(this.button1);

        #endregion

        #region Label2

        this.label2.AutoSize = true;    // O texto se adapta ao tamanho da janela
        this.label2.Location = new System.Drawing.Point(150, 30);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(90, 20); // Tamanho do label
        this.label2.Text = "Digite um número: ";    // Define o texto do label
        
        this.Controls.Add(this.label2);

        #endregion

        #region TextBox2

        this.textBox2.AutoSize = true;
        this.textBox2.Location = new System.Drawing.Point(155, 50);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(100, 25); // Tamanho do

        this.Controls.Add(this.textBox2);

        #endregion

        #region Button2

        this.button2.AutoSize = true;
        this.button2.Location = new System.Drawing.Point(155, 85);
        this.button2.Name = "button2";
        this.button2.Text = "Gerar Número";
        this.button2.Size = new System.Drawing.Size(100, 25); // Tamanho do

        this.button2.Click += new System.EventHandler(this.Button2_Click); // Evento para tratar o click do botõ
        
        this.Controls.Add(this.button2);

        #endregion

        #region ButtonSomar

        this.buttonSomar.AutoSize = true;
        this.buttonSomar.Location = new System.Drawing.Point(300, 30);
        this.buttonSomar.Name = "buttonSomar";
        this.buttonSomar.Text = "Somar";
        this.buttonSomar.Size = new System.Drawing.Size(100, 70); // Tamanho do

        this.buttonSomar.Click += new System.EventHandler(this.ButtonSomar_Click); // Evento para tratar o click do botõ
        
        this.Controls.Add(this.buttonSomar);

        #endregion
    
    }

    #endregion
}
