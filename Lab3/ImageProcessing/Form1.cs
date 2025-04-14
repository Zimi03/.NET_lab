namespace ImageProcessing
{
    public partial class ImageProcessing : Form
    {
        private Bitmap? img;
        public ImageProcessing()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            try
            {
                img = new Bitmap(openFileDialog1.FileName);
                OriginalPicture.Image = img;
                textBox1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania obrazu: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("Provide Image First");
                return;
            }

            var sourceClones = new Bitmap[] {
                (Bitmap)img.Clone(),
                (Bitmap)img.Clone(),
                (Bitmap)img.Clone(),
                (Bitmap) img.Clone()
            };

            Action[] filters = new Action[]
            {
                () => {GreyScale.Image = Filters.ToGrayScale(sourceClones[0]); },
                () => {Threshold.Image = Filters.ToThreshold(sourceClones[1]); },
                () => {Negative.Image = Filters.ToNegative(sourceClones[2]); },
                () => {Mirroring.Image = Filters.ToMirror(sourceClones[3]); }
            };


            Parallel.For(0, filters.Length, i =>
            {
                filters[i]();
            });
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;

        }
    }
}
