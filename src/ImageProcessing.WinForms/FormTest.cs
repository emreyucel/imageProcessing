using ImageProcessing.Filtering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ImageProcessing.WinForms
{
    public partial class FormTest : Form
    {
        int kernelSize;
        double coeff = 4.0;
        RadioButton selected;
        Image image;
        Image filteredImage;
        IFilter filter;

        public FormTest()
        {
            InitializeComponent();
            trackBarKernelSize_Scroll(trackBarKernelSize, new EventArgs());
            ToggleButtons(false, btnReset, btnApply, btnSaveImg);
        }

        private void ChangedChecked_RadioButtons(object sender, EventArgs e)
        {
            selected = (RadioButton)sender;
            CreateFilter();
        }

        private void CreateFilter()
        {
            if (selected == rbMedianFilter)
                filter = new MedianFilter(new Size(kernelSize, kernelSize));
            else
            {
                SquareMatrix<int> kernel = new SquareMatrix<int>(kernelSize);
                if (selected == rbBoxFilter)
                {
                    kernel.Fill(1);
                    filter = new BoxFilter(kernel);
                }
                else if(selected == rbGaussianBlur)
                    filter = new GaussianBlur(kernel, kernelSize / coeff);

            }
        }

        public void ApplyFilter()
        {
            if (filter == null) return;
            if (image == null || image.Source == null) return;

            filteredImage = filter.Apply(image);
        }

        private void btnOpenImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result != DialogResult.OK) return;
            var img = System.Drawing.Image.FromFile(ofd.FileName);
            
            image = new Image(ofd.FileName);
            pbImage.Image = image.Source;

            ToggleButtons(true, btnSaveImg, btnApply);
        }

        private void trackBarKernelSize_Scroll(object sender, EventArgs e)
        {
            int value = trackBarKernelSize.Value;
            kernelSize = 2 * value + 1; //2n+1 odd value
            lblKernel.Text = $"{kernelSize}x{kernelSize}";

            CreateFilter();
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            if (image == null || image.Source == null) return;        
            ApplyFilter();
            pbImage.Image = filteredImage != null ? filteredImage.Source : image.Source;

            ToggleButtons(true, btnReset);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (image == null || image.Source == null) return;
            pbImage.Image = image.Source;
            trackBarKernelSize.Value = 1;
            trackBarKernelSize_Scroll(trackBarKernelSize, new EventArgs());
            

            ToggleButtons(false, btnReset);
        }

        private void ToggleButtons(bool enable, params Button[] buttons)
        {
            foreach (var button in buttons)
                button.Enabled = enable;
        }

        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            if (filteredImage == null || filteredImage.Source == null) return;
            Bitmap bitmap = filteredImage.Source;

            string name = txtImageName.Text != "" ? txtImageName.Text + ".png" : "filtered_image.png";
            string path = "../../../../images/";

            if (Directory.Exists(path))
                bitmap.Save(path + name);
            else
            {
                Directory.CreateDirectory(path);
                bitmap.Save(path + name);
            }
        }
    }
}
