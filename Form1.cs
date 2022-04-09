namespace Day13_01_GrayScale_Image_Processing_Update_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void 동일영상ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equal_image();
        }

        private void 반전영상ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseImage();
        }

        private void 밝게어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addImage();
        }

        private void 선명하게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearImage();
        }

        private void 희미하게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faintImage();
        }

        private void 흑백영상ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bwImage();
        }
        private void 흑백영상평균값ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bwavgImage();
        }

        private void 범위강조ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HLImage();
        }

        private void 감마보정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GammaImage();
        }

        private void 포스터라이징ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Posterizing();        
        }

        private void 회전정방향ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate1_image();
        }

        private void 회전중앙역방향ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate2_image();
        }

        private void 확대ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomOut();
        }
        private void 확대ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            zoomOut2();
        }

        private void 축소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomIn();
        }
        private void 이동ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveImage();
        }

        private void 좌우ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirrorImage1();
        }

        private void 상하ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirrorImage2();
        }

        private void 스트레칭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histo_stretch();
        }

        private void 엔드인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            end_in();
        }

        private void 평활화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histo_equalize();
        }

        private void 엠보싱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            embossing();
        }

        private void 블러링ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blurring();
        }
        private void 가우시안ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gausian();
        }
        private void 회선마스크1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpening1();
        }

        private void 회선마스크2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpening2();
        }

        private void 고주파필터링ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpening3();
        }
        private void 수직검출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boundryLine1();
        }
        private void 수평검ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boundryLine2();
        }
        private void 유사연산자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            similar();
        }
        private void 차연산자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minus();
        }
        private void 로버츠마스크ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robertsmask();
        }
        private void 프리윗마스크ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prewittmask();
        }

        private void 소벨마스크ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sobelmask();
        }
        private void 라플라시안ToolStripMenuItem_Click(object sender, EventArgs e)
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
        
        // 전역 변수
        static byte[,] inImage = null, outImage = null;
        static int inH, inW, outH, outW;
        static string fileName;
        static Bitmap paper; // 그림을 그릴 종이

        ///////// 공통 함수 부분 //////////
        void openImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            fileName = ofd.FileName;

            BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
            // 이미지의 폭과 높이
            long fsize = new FileInfo(fileName).Length;
            inH = inW = (int)Math.Sqrt(fsize);
            // 메모리 할당
            inImage = new byte[inH, inW];
            // 파일 --> 메모리
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
            //종이, 액자, 벽 크기 지정
            paper = new Bitmap(inH, inW); // 종이
            pb_inImage.Size = new Size(inH, inW); // 액자
            this.Size = new Size(inH + 20, inW + 80); // 벽

            Color pen; // 펜
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    byte ink = inImage[i, k]; // 잉크(색상값)
                    pen = Color.FromArgb(ink, ink, ink); // 펜에 잉크 묻히기
                    paper.SetPixel(k, i, pen); // 종이에 한점 찍기
                }
            }
            pb_inImage.Image = paper; // 액자에 종이 걸기
            toolStripStatusLabel1.Text = Path.GetFileName(fileName);
            toolStripStatusLabel2.Text = inH.ToString() + 'x' + inW.ToString();
            toolStripStatusLabel3.Text = "";
        }
        void displayImage()
        {
            //pb_outImage 위치 선정
            pb_outImage.Location = new Point(inH + 10, pb_inImage.Location.Y);
            //종이, 액자, 벽 크기 지정
            paper = new Bitmap(outH, outW); // 종이
            pb_outImage.Size = new Size(outH, outW); // 액자
            if (outW > inW)
                this.Size = new Size(outH + inH + 10, outW + 80); // 벽
            else
                this.Size = new Size(outH + inH + 10, inW + 80); // 벽
            Color pen; // 펜
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    byte ink = outImage[i, k]; // 잉크(색상값)
                    pen = Color.FromArgb(ink, ink, ink); // 펜에 잉크 묻히기
                    paper.SetPixel(k, i, pen); // 종이에 한점 찍기
                }
            }
            pb_outImage.Image = paper; // 액자에 종이 걸기
            toolStripStatusLabel3.Text = outH.ToString() + 'x' + outW.ToString();
        }
        ///////// 영상 처리 알고리즘 //////////
        void equal_image()
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void embossing() // 엠보싱 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { {-1.0, 0.0, 0.0},
                               {0.0, 0.0, 0.0},
                               {0.0, 0.0, 1.0} }; // 엠보싱 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void blurring() // 블러링 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { { 1/9.0, 1/9.0, 1/9.0},
                               { 1/9.0, 1/9.0, 1/9.0},
                               { 1/9.0, 1/9.0, 1/9.0} }; // 블러링 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void Gausian() // 가우시안 필터 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { {1/16.0, 1/8.0, 1/16.0},
                               {1/8.0, 1/4.0, 1/8.0},
                               {1/16.0, 1/8.0, 1/16.0} }; // 가우시안 필터 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void sharpening1()  // 샤프닝 알고리즘(마스크1)
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { { -1.0, -1.0, -1.0},
                               { -1.0, 9.0, -1.0},
                               { -1.0, -1.0, -1.0} }; // 샤프닝 마스크1
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void sharpening2()   // 샤프닝 알고리즘(마스크 2)
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { { 0.0, -1.0, 0.0},
                               { -1.0, 5.0, -1.0},
                               { 0.0, -1.0, 0.0} }; // 샤프닝 마스크2
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void sharpening3()  // 샤프닝 알고리즘(고주파 필터)
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { {-1.0/9.0, -1.0/9.0, -1.0/9.0},
                               {-1.0/9.0, 8.0/9.0, -1.0/9.0},
                               {-1.0/9.0, -1.0/9.0, -1.0/9.0} }; // 샤프닝(고주파 필터) 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            //for (int i = 0; i < inH + 2; i++)
            //    for (int k = 0; k < inW + 2; k++)
            //        tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += 20 * mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void boundryLine1() // 수직 검출 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { { 0.0, 0.0, 0.0},
                               {-1.0, 1.0, 0.0},
                               { 0.0, 0.0, 0.0} }; // 엠보싱 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void boundryLine2() // 수평 검출 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { { 0.0,-1.0, 0.0},
                               { 0.0, 1.0, 0.0},
                               { 0.0, 0.0, 0.0} }; // 엠보싱 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void Robertsmask() // 로버츠 마스크 알고리즘(작은 부분들은 뭉게짐.)
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE1 = 3;
            const int MSIZE2 = 3;
            double[,] mask1 = { { -1.0, 0.0, 0.0},
                               {  0.0, 1.0, 0.0},
                               {  0.0, 0.0, 0.0} }; // 행 검출 마스크
            double[,] mask2 = { { 0.0, 0.0, -1.0},
                               {  0.0, 1.0, 0.0},
                               {  0.0, 0.0, 0.0} }; // 열 검출 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
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
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void Prewittmask() // 프리윗 마스크 알고리즘(로버츠 마스크보다 경계선의 강도가 강하다.)
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE1 = 3;
            const int MSIZE2 = 3;
            double[,] mask1 = { { -1.0, -1.0, -1.0},
                               {  0.0, 0.0, 0.0},
                               {  1.0, 1.0, 1.0} }; // 행 검출 마스크
            double[,] mask2 = { { 1.0, 0.0, -1.0},
                               {  1.0, 0.0, -1.0},
                               {  1.0, 0.0, -1.0} }; // 열 검출 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
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
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void Sobelmask() // 소벨 마스크 알고리즘(미세한 경계선까지 검출)
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE1 = 3;
            const int MSIZE2 = 3;
            double[,] mask1 = { { -1.0, -2.0, -1.0},
                               {  0.0, 0.0, 0.0},
                               {  1.0, 2.0, 1.0} }; // 행 검출 마스크
            double[,] mask2 = { { 1.0, 0.0, -1.0},
                               {  2.0, 0.0, -2.0},
                               {  1.0, 0.0, -1.0} }; // 열 검출 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
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
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                    tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void Laplacian() // 라플라시안 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            double[,] mask = { { -1.0, -1.0, -1.0},
                               { -1.0, 8.0, -1.0},
                               { -1.0, -1.0, -1.0} }; // 라플라시안 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void LoG() // 엠보싱 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 5;
            double[,] mask = { {0.0, 0.0, -1.0, 0.0, 0.0},
                               {0.0, -1.0, -2.0, -1.0, 0.0},
                               {-1.0, -2.0, 16.0, -2.0, -1.0},
                               {0.0, -1.0, -2.0, -1.0, 0.0},
                               {0.0, 0.0, -1.0, 0.0, 0.0}}; // LoG 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 4, inW + 4]; // 5x5 마스크라서 +4
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void DoG() // 엠보싱 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 9;
            double[,] mask = new double [9, 9] { {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0},
                                                 {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
                                                 {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
                                                 {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
                                                 {-1.0, -3.0, -1.0, 9.0, 19.0, 9.0, -1.0, -3.0, -1.0},
                                                 {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
                                                 {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
                                                 {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
                                                 {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0} }; // DoG 마스크
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 8, inW + 8]; // 5x5 마스크라서 +4
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double S = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            S += mask[m, n] * tmpInput[m + i, n + k];
                    tmpOutput[i, k] = S;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void similar() // 유사 연산자 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            const int MSIZE = 3;
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
                    double max = 0.0;
                    for (int m = 0; m < MSIZE; m++)
                        for (int n = 0; n < MSIZE; n++)
                            if(Math.Abs(tmpInput[i+1, k+1] - tmpInput[i+m, k+n]) >= max)
                            max += Math.Abs(tmpInput[i + 1, k + 1] - tmpInput[i + m, k + n]);
                    tmpOutput[i, k] = max;
                }
            }
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void minus() // 차 연산자 알고리즘
        {
            if (inImage == null)
                return;
            // 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            double max = 0;
            double[] mask = new double[4];
            // 임시 입출력 메모리 확보
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];
            // 임시 입력을 초기화(0, 127, 평균값)
            for (int i = 0; i < inH + 2; i++)
                for (int k = 0; k < inW + 2; k++)
                    tmpInput[i, k] = 127.0;
            // 입력 --> 임시 입력
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    tmpInput[i + 1, k + 1] = inImage[i, k];
            // 회선 연산
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    // 한 점에 대한 마스크 연산
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
            //마스크의 합계가 0이면, 127 정도를 더해주기.
            //for (int i = 0; i < outH; i++)
            //    for (int k = 0; k < outW; k++)
            //        tmpOutput[i, k] += 127;
            // 임시 출력 --> 출력
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
        void rotate1_image() // 회전 정방향 ppt 20p 참조
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void rotate2_image() // 회전(중앙, 역방향) ppt 24p 참조
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            // xd = ( cos * (xs-cx) - sin * (ys-cy) ) + cx
            // yd = ( -sin * (xs-cx) + cos * (ys-cy) ) + cy
            double angle = getValue();
            double radian = angle * Math.PI / 180.0; // 컴퓨터는 거의 라디안 값을 받는다.
            int cx = outH / 2; // 중앙값
            int cy = outW / 2;
            for (int i = 0; i < outH; i++)
            {
                for (int k = 0; k < outW; k++)
                {
                    int xd = (int)(Math.Cos(radian) * (i - cx) + Math.Sin(radian) * (k - cy)); // 사진을 중앙으로 이동
                    int yd = (int)(-Math.Sin(radian) * (i - cx) + Math.Cos(radian) * (k - cy));
                    xd += cx; // 사진을 다시 원래 위치로 이동
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
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
            InputForm1 sub = new InputForm1(); // 서브 폼 준비
            if (sub.ShowDialog() == DialogResult.Cancel)
                value = 0;
            else
                value = (double)(sub.updown_value1.Value);
            return value;
        }
        void addImage() // 밝게/어둡게
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void histo_stretch() //스트레칭
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            // out = (in - low) / (high - low) * 255  --> Psudo Code
            byte low = inImage[0, 0], high = inImage[0, 0]; // 최소값, 최댓값
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
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
            low += 50; high -= 50;  // 퀴즈1 해답
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
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
            // 1단계 : 히스토그램 생성
            int[] hist = new int[256];
            for (int i = 0; i < 256; i++)
                hist[i] = 0;
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    hist[inImage[i, k]]++; // 동일한 화소값이 나올떄마다 + 1
                }
            }
            // 2단계 : 누적 히스토그램 생성
            int[] sumHist = new int[256];
            int sValue = 0;
            for (int i = 0; i < 256; i++)
            {
                sValue += hist[i];
                sumHist[i] = sValue;
            }
            // 3단계 : 정규화된 누적 히스토그램 생성
            // sum = (sumHist / (행x열)) * 255.0
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
        void clearImage() // 선명하게
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void faintImage()  // 희미하게
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void bwImage()  // 흑백영상
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void bwavgImage()  // 흑백영상(평균값)
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void HLImage() // 범위강조
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정 
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void GammaImage()  // 감마 보정
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정 
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void Posterizing()  // 포스터라이징
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void zoomOut() // 확대 알고리즘
        {
            double scale = getValue();
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = (int)(inH * scale); outW = (int)(inW * scale);
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void zoomOut2() // 확대(이웃 화소 보간법) 알고리즘
        {
            double scale = getValue();
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = (int)(inH * scale); outW = (int)(inW * scale);
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void zoomIn() // 축소 알고리즘
        {
            double scale = getValue();
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = (int)(inH / scale); outW = (int)(inW / scale);
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void moveImage() // 이동 알고리즘
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
        void mirrorImage1() // 좌우 대칭 알고리즘
        {
            if (inImage == null)
                return;
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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
            // 출력 영상의 크기 결정
            outH = inH; outW = inW;
            // 출력 영상 메모리 할당
            outImage = new byte[outH, outW];
            // 영상 처리 알고리즘
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