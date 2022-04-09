namespace Day13_01_GrayScale_Image_Processing_Update_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void ���Ͽ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equal_image();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseImage();
        }

        private void ��Ծ�Ӱ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addImage();
        }

        private void �����ϰ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearImage();
        }

        private void ����ϰ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faintImage();
        }

        private void ��鿵��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bwImage();
        }
        private void ��鿵����հ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bwavgImage();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HLImage();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GammaImage();
        }

        private void �����Ͷ���¡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Posterizing();        
        }

        private void ȸ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate1_image();
        }

        private void ȸ���߾ӿ�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate2_image();
        }

        private void Ȯ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomOut();
        }
        private void Ȯ��ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            zoomOut2();
        }

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomIn();
        }
        private void �̵�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveImage();
        }

        private void �¿�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirrorImage1();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirrorImage2();
        }

        private void ��Ʈ��ĪToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histo_stretch();
        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            end_in();
        }

        private void ��ȰȭToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histo_equalize();
        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            embossing();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blurring();
        }
        private void ����þ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gausian();
        }
        private void ȸ������ũ1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpening1();
        }

        private void ȸ������ũ2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpening2();
        }

        private void ���������͸�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpening3();
        }
        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boundryLine1();
        }
        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boundryLine2();
        }
        private void ���翬����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            similar();
        }
        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minus();
        }
        private void �ι�������ũToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robertsmask();
        }
        private void ����������ũToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prewittmask();
        }

        private void �Һ�����ũToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sobelmask();
        }
        private void ���ö�þ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laplacian();
        }
        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoG();
        }
        private void dogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoG();
        }
        
        // ���� ����
        static byte[,] inImage = null, outImage = null;
        static int inH, inW, outH, outW;
        static string fileName;
        static Bitmap paper; // �׸��� �׸� ����

        ///////// ���� �Լ� �κ� //////////
        void openImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            fileName = ofd.FileName;

            BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
            // �̹����� ���� ����
            long fsize = new FileInfo(fileName).Length;
            inH = inW = (int)Math.Sqrt(fsize);
            // �޸� �Ҵ�
            inImage = new byte[inH, inW];
            // ���� --> �޸�
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    inImage[i, k] = br.ReadByte();
                }
            }
            br.Close();
            displayImage_input();
        }
        void displayImage_input()
        {
            //����, ����, �� ũ�� ����
            paper = new Bitmap(inH, inW); // ����
            pb_inImage.Size = new Size(inH, inW); // ����
            this.Size = new Size(inH + 20, inW + 80); // ��

            Color pen; // ��
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    byte ink = inImage[i, k]; // ��ũ(����)
                    pen = Color.FromArgb(ink, ink, ink); // �濡 ��ũ ������
                    paper.SetPixel(k, i, pen); // ���̿� ���� ���
                }
            }
            pb_inImage.Image = paper; // ���ڿ� ���� �ɱ�
            toolStripStatusLabel1.Text = Path.GetFileName(fileName);
            toolStripStatusLabel2.Text = inH.ToString() + 'x' + inW.ToString();
            toolStripStatusLabel3.Text = "";
        }
        void displayImage()
        {
            //pb_outImage ��ġ ����
            pb_outImage.Location = new Point(inH + 10, pb_inImage.Location.Y);
            //����, ����, �� ũ�� ����
            paper = new Bitmap(outH, outW); // ����
            pb_outImage.Size = new Size(outH, outW); // ����
            if (outW > inW)
                this.Size = new Size(outH + inH + 10, outW + 80); // ��
            else
                this.Size = new Size(outH + inH + 10, inW + 80); // ��
            Color pen; // ��
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    byte ink = outImage[i, k]; // ��ũ(����)
                    pen = Color.FromArgb(ink, ink, ink); // �濡 ��ũ ������
                    paper.SetPixel(k, i, pen); // ���̿� ���� ���
                }
            }
            pb_outImage.Image = paper; // ���ڿ� ���� �ɱ�
            toolStripStatusLabel3.Text = outH.ToString() + 'x' + outW.ToString();
        }
        ///////// ���� ó�� �˰��� //////////
        void equal_image()
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = inImage[i, k];
                }
            }
            ////////////////////
            displayImage();
        }
        void embossing() // ������ �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { {-1.0, 0.0, 0.0},
                               {0.0, 0.0, 0.0},
                               {0.0, 0.0, 1.0} }; // ������ ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void blurring() // ���� �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { { 1/9.0, 1/9.0, 1/9.0},
                               { 1/9.0, 1/9.0, 1/9.0},
                               { 1/9.0, 1/9.0, 1/9.0} }; // ���� ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void Gausian() // ����þ� ���� �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { {1/16.0, 1/8.0, 1/16.0},
                               {1/8.0, 1/4.0, 1/8.0},
                               {1/16.0, 1/8.0, 1/16.0} }; // ����þ� ���� ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void sharpening1()  // ������ �˰���(����ũ1)
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { { -1.0, -1.0, -1.0},
                               { -1.0, 9.0, -1.0},
                               { -1.0, -1.0, -1.0} }; // ������ ����ũ1
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void sharpening2()   // ������ �˰���(����ũ 2)
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { { 0.0, -1.0, 0.0},
                               { -1.0, 5.0, -1.0},
                               { 0.0, -1.0, 0.0} }; // ������ ����ũ2
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void sharpening3()  // ������ �˰���(������ ����)
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { {-1.0/9.0, -1.0/9.0, -1.0/9.0},
                               {-1.0/9.0, 8.0/9.0, -1.0/9.0},
                               {-1.0/9.0, -1.0/9.0, -1.0/9.0} }; // ������(������ ����) ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            //for (int i = 0; i < inH + 2; i++)
            //    for (int k = 0; k < inW + 2; k++)
            //        tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += 20 * mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void boundryLine1() // ���� ���� �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { { 0.0, 0.0, 0.0},
                               {-1.0, 1.0, 0.0},
                               { 0.0, 0.0, 0.0} }; // ������ ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void boundryLine2() // ���� ���� �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { { 0.0,-1.0, 0.0},
                               { 0.0, 1.0, 0.0},
                               { 0.0, 0.0, 0.0} }; // ������ ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void Robertsmask() // �ι��� ����ũ �˰���(���� �κе��� ������.)
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE1 = 3;
            const int MSIZE2 = 3;
            double[,] mask1 = { { -1.0, 0.0, 0.0},
                               {  0.0, 1.0, 0.0},
                               {  0.0, 0.0, 0.0} }; // �� ���� ����ũ
            double[,] mask2 = { { 0.0, 0.0, -1.0},
                               {  0.0, 1.0, 0.0},
                               {  0.0, 0.0, 0.0} }; // �� ���� ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S=0.0, S1=0.0, S2=0.0;
                    for (int m = 0; m < MSIZE1; m++)
                        for (int n = 0; n < MSIZE1; n++)
                            S1 += mask1[m, n] * tmpInput[m + i, n + k];
                    for (int m = 0; m < MSIZE2; m++)
                        for (int n = 0; n < MSIZE2; n++)
                            S2 += mask2[m, n] * tmpInput[m + i, n + k];
                    S = S1 + S2;
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void Prewittmask() // ������ ����ũ �˰���(�ι��� ����ũ���� ��輱�� ������ ���ϴ�.)
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE1 = 3;
            const int MSIZE2 = 3;
            double[,] mask1 = { { -1.0, -1.0, -1.0},
                               {  0.0, 0.0, 0.0},
                               {  1.0, 1.0, 1.0} }; // �� ���� ����ũ
            double[,] mask2 = { { 1.0, 0.0, -1.0},
                               {  1.0, 0.0, -1.0},
                               {  1.0, 0.0, -1.0} }; // �� ���� ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0, S1 = 0.0, S2 = 0.0;
                    for (int m = 0; m < MSIZE1; m++)
                        for (int n = 0; n < MSIZE1; n++)
                            S1 += mask1[m, n] * tmpInput[m + i, n + k];
                    for (int m = 0; m < MSIZE2; m++)
                        for (int n = 0; n < MSIZE2; n++)
                            S2 += mask2[m, n] * tmpInput[m + i, n + k];
                    S = S1 + S2;
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void Sobelmask() // �Һ� ����ũ �˰���(�̼��� ��輱���� ����)
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE1 = 3;
            const int MSIZE2 = 3;
            double[,] mask1 = { { -1.0, -2.0, -1.0},
                               {  0.0, 0.0, 0.0},
                               {  1.0, 2.0, 1.0} }; // �� ���� ����ũ
            double[,] mask2 = { { 1.0, 0.0, -1.0},
                               {  2.0, 0.0, -2.0},
                               {  1.0, 0.0, -1.0} }; // �� ���� ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0, S1 = 0.0, S2 = 0.0;
                    for (int m = 0; m < MSIZE1; m++)
                        for (int n = 0; n < MSIZE1; n++)
                            S1 += mask1[m, n] * tmpInput[m + i, n + k];
                    for (int m = 0; m < MSIZE2; m++)
                        for (int n = 0; n < MSIZE2; n++)
                            S2 += mask2[m, n] * tmpInput[m + i, n + k];
                    S = S1 + S2;
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void Laplacian() // ���ö�þ� �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            double[,] mask = { { -1.0, -1.0, -1.0},
                               { -1.0, 8.0, -1.0},
                               { -1.0, -1.0, -1.0} }; // ���ö�þ� ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void LoG() // ������ �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 5;
            double[,] mask = { {0.0, 0.0, -1.0, 0.0, 0.0},
                               {0.0, -1.0, -2.0, -1.0, 0.0},
                               {-1.0, -2.0, 16.0, -2.0, -1.0},
                               {0.0, -1.0, -2.0, -1.0, 0.0},
                               {0.0, 0.0, -1.0, 0.0, 0.0}}; // LoG ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 4, inW + 4]; // 5x5 ����ũ�� +4
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void DoG() // ������ �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 9;
            double[,] mask = new double [9, 9] { {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0},
                                                 {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
                                                 {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
                                                 {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
                                                 {-1.0, -3.0, -1.0, 9.0, 19.0, 9.0, -1.0, -3.0, -1.0},
                                                 {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
                                                 {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
                                                 {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
                                                 {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0} }; // DoG ����ũ
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 8, inW + 8]; // 5x5 ����ũ�� +4
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void similar() // ���� ������ �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            const int MSIZE = 3;
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    double max = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            if(Math.Abs(tmpInput[i+1, k+1] - tmpInput[i+m, k+n]) >= max)
                            max += Math.Abs(tmpInput[i + 1, k + 1] - tmpInput[i + m, k + n]);
                    tmpOutput[i, k] = max;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void minus() // �� ������ �˰���
        {
            if (inImage == null)
                return;
            // �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double max = 0;
            double[] mask = new double[4];
            // �ӽ� ����� �޸� Ȯ��
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // �Է� --> �ӽ� �Է�
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // ȸ�� ����
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // �� ���� ���� ����ũ ����
                    max = 0.0;
                    mask[0] = Math.Abs(tmpInput[i, k] - tmpInput[i+2, k+2]);
                    mask[1] = Math.Abs(tmpInput[i, k+1] - tmpInput[i+2, k+1]);
                    mask[2] = Math.Abs(tmpInput[i, k+2] - tmpInput[i+2, k]);
                    mask[3] = Math.Abs(tmpInput[i+1, k+2] - tmpInput[i+1, k]);
                    for (int m = 0; m<4; m++)
                    {
                        if (mask[m] >= max) max = mask[m];
                    }
                    tmpOutput[i, k] = max;
                }
            }
            //����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // �ӽ� ��� --> ���
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    if (tmpOutput[i, k] < 0)
                        outImage[i, k] = 0;
                    else if (tmpOutput[i, k] > 255)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = (byte)(tmpOutput[i, k]);

                }
            ////////////////////
            displayImage();
        }
        void rotate1_image() // ȸ�� ������ ppt 20p ����
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            // xd = cos * xs - sin * ys
            // yd = sin * xs + cos * ys
            double angle = getValue();
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    int xd = (int)(Math.Cos(angle) * i - Math.Sin(angle) * k);
                    int yd = (int)(Math.Sin(angle) * i + Math.Cos(angle) * k);
                    if ((0 <= xd && xd < outH) && (0 <= yd && yd < outW))
                        outImage[xd, yd] = inImage[i, k];
                }
            }
            ////////////////////
            displayImage();
        }
        void rotate2_image() // ȸ��(�߾�, ������) ppt 24p ����
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            // xd = ( cos * (xs-cx) - sin * (ys-cy) ) + cx
            // yd = ( -sin * (xs-cx) + cos * (ys-cy) ) + cy
            double angle = getValue();
            double radian = angle * Math.PI / 180.0; // ��ǻ�ʹ� ���� ���� ���� �޴´�.
            int cx = outH / 2; // �߾Ӱ�
            int cy = outW / 2;
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    int xd = (int)(Math.Cos(radian) * (i - cx) + Math.Sin(radian) * (k - cy)); // ������ �߾����� �̵�
                    int yd = (int)(-Math.Sin(radian) * (i - cx) + Math.Cos(radian) * (k - cy));
                    xd += cx; // ������ �ٽ� ���� ��ġ�� �̵�
                    yd += cy;
                    if ((0 <= xd && xd < outH) && (0 <= yd && yd < outW))
                        outImage[i, k] = inImage[xd, yd];
                }
            }
            ////////////////////
            displayImage();
        }
        void reverseImage()
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = (byte)(255 - inImage[i, k]);
                }
            }
            ////////////////////
            displayImage();
        }
        double getValue()
        {
            double value;
            InputForm1 sub = new InputForm1(); // ���� �� �غ�
            if (sub.ShowDialog() == DialogResult.Cancel)
                value = 0;
            else
                value = (double)(sub.updown_value1.Value);
            return value;
        }
        void addImage() // ���/��Ӱ�
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double value = getValue();
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    int px = (int)(inImage[i, k] + value);
                    if (px > 255)
                        px = 255;
                    else if (px < 0)
                        px = 0;

                    outImage[i, k] = (byte)px;
                }
            }
            ////////////////////
            displayImage();
        }
        void histo_stretch() //��Ʈ��Ī
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            // out = (in - low) / (high - low) * 255  --> Psudo Code
            byte low = inImage[0, 0], high = inImage[0, 0]; // �ּҰ�, �ִ�
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    if (inImage[i, k] < low)
                        low = inImage[i, k];
                    if (inImage[i, k] > high)
                        high = inImage[i, k];
                }
            }
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    double outValue = (inImage[i, k] - (double)low) / (high - (double)low) * 255;
                    if (outValue < 0.0)
                        outValue = 0.0;
                    else if (outValue > 255.0)
                        outValue = 255.0;

                    outImage[i, k] = (byte)outValue;
                }
            }
            ////////////////////
            displayImage();
        }
        void end_in()
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            // out = (in - low) / (high - low) * 255  --> Psudo Code
            byte low = inImage[0, 0], high = inImage[0, 0];
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    if (inImage[i, k] < low)
                        low = inImage[i, k];
                    if (inImage[i, k] > high)
                        high = inImage[i, k];
                }
            }
            low += 50; high -= 50;  // ����1 �ش�
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    double outValue = (inImage[i, k] - (double)low) / (high - (double)low) * 255;
                    if (outValue < 0.0)
                        outValue = 0.0;
                    else if (outValue > 255.0)
                        outValue = 255.0;

                    outImage[i, k] = (byte)outValue;
                }
            }
            ////////////////////
            displayImage();
        }
        void histo_equalize()
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            // 1�ܰ� : ������׷� ����
            int[] hist = new int[256];
            for (int i = 0; i < 256; i++)
                hist[i] = 0;
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    hist[inImage[i, k]]++; // ������ ȭ�Ұ��� ���Ë����� + 1
                }
            }
            // 2�ܰ� : ���� ������׷� ����
            int[] sumHist = new int[256];
            int sValue = 0;
            for (int i = 0; i < 256; i++)
            {
                sValue += hist[i];
                sumHist[i] = sValue;
            }
            // 3�ܰ� : ����ȭ�� ���� ������׷� ����
            // sum = (sumHist / (��x��)) * 255.0
            double[] normalHist = new double[256];
            for (int i = 0; i < 256; i++)
            {
                normalHist[i] = sumHist[i] / (double)(inH * inW) * 255.0;
            }
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = (byte)normalHist[inImage[i, k]];
                }
            }
            ////////////////////
            displayImage();
        }
        void clearImage() // �����ϰ�
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double value = getValue();
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    int px = (int)(inImage[i, k] * value);
                    if (px > 255)
                        px = 255;
                    else if (px < 0)
                        px = 0;

                    outImage[i, k] = (byte)px;
                }
            }
            ////////////////////
            displayImage();
        }
        void faintImage()  // ����ϰ�
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double value = getValue();
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    int px = (int)(inImage[i, k] / value);
                    if (px > 255)
                        px = 255;
                    else if (px < 0)
                        px = 0;

                    outImage[i, k] = (byte)px;
                }
            }
            ////////////////////
            displayImage();
        }
        void bwImage()  // ��鿵��
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = inImage[i, k];
                    if (inImage[i, k] > 127)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = 0;
                }
            }
            ////////////////////
            displayImage();
        }
        void bwavgImage()  // ��鿵��(��հ�)
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            int sum = 0, avg = 0;
            for(int i=0; i<inH; i++)
            {
                for(int k=0; k<inW; k++)
                {
                    sum += inImage[i, k];
                    avg = sum / (inH * inW);
                }
            }
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = inImage[i, k];
                    if(outImage[i, k] >= avg) outImage[i, k] = 255;
                    else outImage[i, k] = 0;
                }
            }
            ////////////////////
            displayImage();
        }
        void HLImage() // ��������
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ���� 
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double min = getValue();
            double max = getValue();
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    if (inImage[i, k] <= min || inImage[i, k] > max)
                        outImage[i, k] = 255;
                    else
                        outImage[i, k] = inImage[i, k];
                }
            }
            ////////////////////
            displayImage();
        }
        void GammaImage()  // ���� ����
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ���� 
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double gamma, temp;
            gamma = getValue();
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    temp = Math.Pow(inImage[i, k], (1 / gamma));
                    if (temp > 255)
                        outImage[i, k] = 255;
                    else if (temp < 0)
                        outImage[i, k] = 0;
                    else
                        outImage[i, k] = (byte)temp;
                }
            }
            ////////////////////
            displayImage();
        }
        void Posterizing()  // �����Ͷ���¡
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            int value = 32;
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    if (inImage[i, k] < value)
                        outImage[i, k] = 32;
                    else if (inImage[i, k] < value * 2)
                        outImage[i, k] = 64;
                    else if (inImage[i, k] < value * 3)
                        outImage[i, k] = 96;
                    else if (inImage[i, k] < value * 4)
                        outImage[i, k] = 128;
                    else if (inImage[i, k] < value * 5)
                        outImage[i, k] = 160;
                    else if (inImage[i, k] < value * 6)
                        outImage[i, k] = 192;
                    else if (inImage[i, k] < value * 7)
                        outImage[i, k] = 224;
                    else if (inImage[i, k] < value * 8)
                        outImage[i, k] = 255;
                }
            }
            ////////////////////
            displayImage();
        }
        void zoomOut() // Ȯ�� �˰���
        {
            double scale = getValue();
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = (int)(inH * scale); outW = (int)(inW * scale);
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    outImage[(int)(i*scale), (int)(k*scale)] = inImage[i, k];
                }
            }
            ////////////////////
            displayImage();
        }
        void zoomOut2() // Ȯ��(�̿� ȭ�� ������) �˰���
        {
            double scale = getValue();
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = (int)(inH * scale); outW = (int)(inW * scale);
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = inImage[(int)(i / scale), (int)(k / scale)];
                }
            }
            ////////////////////
            displayImage();
        }
        void zoomIn() // ��� �˰���
        {
            double scale = getValue();
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = (int)(inH / scale); outW = (int)(inW / scale);
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = inImage[(int)(i * scale), (int)(k * scale)];
                }
            }
            ////////////////////
            displayImage();
        }
        void moveImage() // �̵� �˰���
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            double UD = getValue();
            double LR = getValue();
            for (int i = (int)UD; i < inH; i++)
            {
                for (int k = (int)LR; k < inW; k++)
                {
                    outImage[i, k] = inImage[(int)(i - LR), (int)(k - UD)];
                }
            }
            ////////////////////
            displayImage();
        }
        void mirrorImage1() // �¿� ��Ī �˰���
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = inImage[i, inW - k - 1];
                }
            }
            ////////////////////
            displayImage();
        }
        void mirrorImage2()
        {
            if (inImage == null)
                return;
            // ��� ������ ũ�� ����
            outH = inH; outW = inW;
            // ��� ���� �޸� �Ҵ�
            outImage = new byte[outH, outW];
            // ���� ó�� �˰���
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = inImage[inH - i - 1, k];
                }
            }
            ////////////////////
            displayImage();
        }
    }
}