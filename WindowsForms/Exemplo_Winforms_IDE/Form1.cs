namespace Exemplo_Winforms_IDE;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        Random rdn = new Random();
        int numero = rdn.Next(0, 100);
        textBox1.Text = numero.ToString();
        // MessageBox.Show("O número digitado foi: " + textBox1.Text);
    }
    private void Button2_Click(object sender, EventArgs e)
    {
        Random rdn = new Random();
        int numero = rdn.Next(0, 100);
        textBox2.Text = numero.ToString();
        // MessageBox.Show("O número digitado foi: " + textBox1.Text);
    }
    private void ButtonSomar_Click(object sender, EventArgs e)
    {
        int num1 = int.Parse(textBox1.Text);
        int num2 = int.Parse(textBox2.Text);

        MessageBox.Show($"{num1} + {num2} = {num1 + num2}", "Soma");
    }
}
