using KnapsackProblem;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            NumOfItemsText.TextChanged += TextBox_TextChanged;
            SeedText.TextChanged += TextBox_TextChanged;
            CapacityText.TextChanged += TextBox_TextChanged;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateInput(sender as TextBox);
        }

        private bool ValidateInput(TextBox textBox)
        {
            if (!int.TryParse(textBox.Text, out int value) || value <= 0)
            {
                textBox.BackColor = Color.Red;
                return false;
            }
            else
            {
                textBox.BackColor = Color.White;
                return true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            isValid &= ValidateInput(NumOfItemsText);
            isValid &= ValidateInput(SeedText);
            isValid &= ValidateInput(CapacityText);

            if (!isValid)
            {
                MessageBox.Show("Invalid input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int numOfItems = int.Parse(NumOfItemsText.Text);
            int seed = int.Parse(SeedText.Text);
            int capacity = int.Parse(CapacityText.Text);
            
            Problem problem = new(numOfItems, seed);
            InstanceTextBox.Text = problem.ToString();
            
            Result result = problem.Solve(capacity);
            ResultTextBox.Text = result.ToString();
        }
    }
}
