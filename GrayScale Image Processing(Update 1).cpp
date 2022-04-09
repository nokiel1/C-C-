#define _CRT_SECURE_NO_WARNINGS
#define UC unsigned char
#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <vector>
#include <string>
#include <math.h>
#include <conio.h>
#include <io.h>
using namespace std;

// �Լ� �����
void openImage(); void displayImage(); void saveImage(); void print_menu();
void equal(); void reverse(); void addImage(); void clear(); void faint();
void BW(); void BWavg(); void Gamma(); void HL();
void zoomIn(); void zoomOut(); void zoomOut2(); void zoomOutBack(); void rotateC(); void rotateRC();
void move(); void mirrorLR(); void mirrorUD(); void histo_stretch(); void end_in();
void PosterizingImage(); void histo_equalize(); void embossing(); void blurring();
void Gaussian(); void sharpening1(); void sharpening2(); void sharpening3();
void boundryLine1(); void boundryLine2(); void similarImage(); void minusImage();
void RobertsImage(); void PrewittImage(); void SobelImage(); void Laplasian(); void LoG(); void DoG();


// ���� ������
vector<vector<UC>> inImage, outImage;
int inH, inW, outH, outW;
string folderName = "C:\\images\\RAW\\";
string fileName, saveName;
HWND hwnd;
HDC hdc;
// ���� �Լ�
int main() {
	hwnd = GetForegroundWindow();
	hdc = GetWindowDC(hwnd);
	char choice = '0';
	char fname[100];
	while (choice != '9') {
		print_menu();
		choice = _getche();
		system("cls");
		switch (choice) {
		case '0':
			printf("Open���ϸ� --> "); scanf("%s", fname);
			fileName = folderName + fname + ".raw";
			openImage();  break;
		case '1':
			printf("Save���ϸ� --> "); scanf("%s", fname);
			saveName = folderName + fname + ".raw";
			saveImage();  break;
		case 'A':
		case 'a': equal(); break;
		case 'B':
		case 'b': reverse(); break;
		case 'C':
		case 'c': addImage(); break;
		case 'D':
		case 'd': clear(); break;
		case 'E':
		case 'e': faint(); break;
		case 'F':
		case 'f': BW(); break;
		case 'G':
		case 'g': BWavg(); break;
		case 'H':
		case 'h': HL(); break;
		case 'I':
		case 'i': Gamma(); break;
		case 'J':
		case 'j': PosterizingImage(); break;
		case 'L':
		case 'l': rotateC(); break;
		case 'M':
		case 'm': rotateRC(); break;
		case 'N':
		case 'n': zoomOut(); break;
		case 'O':
		case 'o': zoomOut2(); break;
		case 'P':
		case 'p': zoomIn(); break;
		case 'Q':
		case 'q': move(); break;
		case 'R':
		case 'r': mirrorLR(); break;
		case 'S':
		case 's': mirrorUD(); break;
		case 'T':
		case 't': histo_stretch(); break;
		case 'U':
		case 'u': end_in(); break;
		case 'V':
		case 'v': histo_equalize(); break;
		case 'W':
		case 'w': embossing(); break;
		case 'X':
		case 'x': blurring(); break;
		case 'Y':
		case 'y': Gaussian(); break;
		case 'Z':
		case 'z': sharpening1(); break;
		case '!': sharpening2(); break;
		case '@': sharpening3(); break;
		case '#': boundryLine1(); break;
		case '$': boundryLine2(); break;
		case '%': similarImage(); break;
		case '^': minusImage(); break;
		case '&': RobertsImage(); break;
		case '*': PrewittImage(); break;
		case '-': SobelImage(); break;
		case '~': Laplasian(); break;
		case '`': LoG(); break;
		case '?': DoG(); break;
		case '9': break;
		default:printf("Ű �߸� ����"); break;
		}
	}
}
void print_menu() {
	puts("\t ## GrayScale ����ó�� (RC1) ##");
	puts("0. ���� ���� 1. ���� ���� 9. ���α׷� ����");
	puts("ȭ���� ó�� --> A. ���� B. ���� C. ���/��Ӱ� D. �ѷ��ϰ� E. ����ϰ�\n\t\tF. ��� G. ���(��հ�) H. �������� I. �������� J. �����Ͷ���¡");
	puts("������ ó�� --> L. 90�� ȸ��(�ð����) M. 90�� ȸ��(�ݽð�) N. Ȯ�� O. Ȯ��(�̿� ȭ�� ����) P. ���\n\t\tQ. �̵� R. �̷���(�¿�) S. �̷���(����)");
	puts("������׷�  --> T. ��Ʈ��Ī U. ����-�� V. ��Ȱȭ");
	puts("ȭ�ҿ��� ó�� --> W. ������ X. ���� Y. ����þ� Z. ������(ȸ������ũ1) !. ������(ȸ������ũ2)\n\t\t  @. ������(������ ����) #. ���� ���� $. ���� ���� %. ���� ������ ^. �� ������\n\t\t  &. �ι��� ����ũ  *. ������ ����ũ -. �Һ� ����ũ ~. ���ö�þ� `. LoG ?. DoG");
}

void printFileName() {
	struct _finddata_t fd;
	intptr_t handle;
	string path = folderName + "*.*";
	if ((handle = _findfirst(path.c_str(), &fd)) == -1L) {
		printf("���ϰ�� ������ ����ֽ��ϴ�.");
	}

	printf("%s ���� ���� ���\n", folderName.c_str());
	do {
		printf("%s\t", fd.name);
	} while (_findnext(handle, &fd) == 0);
};


// ���� �Լ���
void openImage() {
	char fname[100];
	printFileName();
	printf("\nOpen ���ϸ�(Ȯ���� ����) --> ");
	scanf("%s", fname);
	fileName = folderName + fname + ".raw";

	//inH, inW ���
	FILE* rfp = fopen(fileName.c_str(), "rb");
	if (rfp == 0) {
		system("cls");
		printf("���ϸ��� �߸��Ǿ����ϴ�.");
		return;
	}
	fseek(rfp, 0L, SEEK_END); // ������ ������
	LONG fsize = ftell(rfp); // ������ġ(����ũ��, inH * inW)
	inH = inW = sqrt(fsize);
	fclose(rfp);

	// ���Ϸκ��� inImage �� �Է�
	inImage.clear();
	//outImage.clear();  
	rfp = fopen(fileName.c_str(), "rb");
	vector<UC>tmpAry;
	UC px;
	for (int i = 0; i < inH; i++) {
		tmpAry.clear();
		for (int k = 0; k < inW; k++) {
			px = fgetc(rfp);
			tmpAry.push_back(px);
		}
		inImage.push_back(tmpAry); // �� �྿ ä��
	}
	fclose(rfp);
	displayImage();
}
void saveImage() {
	// ����� �����̸�(��ġ����) �����
	char fname[100];
	printf("Save ���ϸ�(Ȯ���� ����) --> ");
	scanf("%s", fname);
	saveName = folderName + fname + ".raw";

	//outImage �迭�� ���Ϸ� ����
	FILE* wfp = fopen(fileName.c_str(), "wb+");
	UC px;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			px = outImage[i][k];
			fputc(px, wfp);
		}
	}
	fclose(wfp);
	printf("%s �� �����.", fname);
}
void displayImage() {
	UC px;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			px = inImage[i][k];
			SetPixel(hdc, k + 50, i + 300, RGB(px, px, px));
		}
	}
	for (int i = 0; i < outH; i++) {
		for (int k = 0; k < outW; k++) {
			px = outImage[i][k];
			SetPixel(hdc, inW + k + 100, i + 300, RGB(px, px, px));
		}
	}
}

// ����ó�� �Լ���
void equal() { // ����
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) 
		{
			outImage[i][k] = inImage[i][k];
		}
	}
	displayImage();
}
void reverse() { // ����
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = 255 - inImage[i][k];
		}
	}
	displayImage();
}
void addImage() { // ���/��Ӱ�
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int num; // ��� ��ġ
	printf("��ġ �Է� -->");
	scanf("%d", &num);
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			int px = inImage[i][k] + num;
			if (px >= 255) px = 255;
			else if (px < 0) px = 0;
			outImage[i][k] = px;
		}
	}
	displayImage();
}
void clear() { // �����ϰ�
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int num; // ��� ��ġ
	printf("��ġ �Է� -->");
	scanf("%d", &num);
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			int px = inImage[i][k] * num;
			if (px >= 255) px = 255;
			outImage[i][k] = px;
		}
	}
	displayImage();
}
void faint() { // �ѷ��ϰ�
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int num; // ��� ��ġ
	printf("��ġ �Է� -->");
	scanf("%d", &num);
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			int px = inImage[i][k] / num;
			if (px < 0)  px = 0;
			outImage[i][k] = px;
		}
	}
	displayImage();
}
void BW() { // ���
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = inImage[i][k];
			if (outImage[i][k] >= 128) {
				outImage[i][k] = 255;
			}
			else
				outImage[i][k] = 0;
		}
	}
	displayImage();
}
void BWavg() { //���(���)
	int hap = 0, avg = 0;
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			hap += inImage[i][k];
			avg = hap / (inW * inH);
		}
	}
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = inImage[i][k];
			if (outImage[i][k] > avg) {
				outImage[i][k] = 255;
			}
			else
				outImage[i][k] = 0;
		}
	}
	displayImage();
}
void Gamma() {
	double num;
	double temp;
	printf("���� �� -->"); scanf("%lf", &num);
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < outH; i++) {
		for (int k = 0; k < outW; k++) {
			temp = pow(inImage[i][k], 1 / num);
			if (temp < 0) {
				outImage[i][k] = 0;
			}
			else if (temp > 255) {
				outImage[i][k] = 255;
			}
			else outImage[i][k] = (UC)temp;
		}
	}
	displayImage();
}
void HL() { // ���� ����
	int start, fin;
	printf("���� �� -->"); scanf("%d", &start);
	printf("���� �� -->"); scanf("%d", &fin);
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			if ((inImage[i][k] > start) && (inImage[i][k] <= fin)) {
				outImage[i][k] = 255;
			}
			else
				outImage[i][k] = inImage[i][k];
		}
	}
	displayImage();
}
void PosterizingImage() { // �����Ͷ���¡
	int value = 32;
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			if (inImage[i][k] < value)
				outImage[i][k] = 32;
			else if (inImage[i][k] < value * 2)
				outImage[i][k] = 64;
			else if (inImage[i][k] < value * 3)
				outImage[i][k] = 96;
			else if (inImage[i][k] < value * 4)
				outImage[i][k] = 128;
			else if (inImage[i][k] < value * 5)
				outImage[i][k] = 160;
			else if (inImage[i][k] < value * 6)
				outImage[i][k] = 192;
			else if (inImage[i][k] < value * 7)
				outImage[i][k] = 224;
			else if (inImage[i][k] < value * 8)
				outImage[i][k] = 255;
		}
	}
	displayImage();
}
void zoomIn() { // ���
	int num;
	printf("��� �� --> "); scanf("%d", &num);
	outH = inH / num; outW = inW / num;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i / num][k / num] = inImage[i][k];
		}
	}
	displayImage();
}
void zoomOut() { // Ȯ��
	int num;
	printf("Ȯ�� �� --> "); scanf("%d", &num);
	outH = inH * num; outW = inW * num;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i*num][k*num] = inImage[i][k];
		}
	}
	displayImage();
}
void zoomOut2() { // Ȯ��
	int num;
	printf("Ȯ�� �� --> "); scanf("%d", &num);
	outH = inH * num; outW = inW * num;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < outH; i++) {
		for (int k = 0; k < outW; k++) {
			outImage[i][k] = inImage[i / num][k / num];
		}
	}
	displayImage();
}
void rotateC() { // �ð���� ȸ��
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = inImage[inW - k - 1][i];
		}
	}
	displayImage();
}
void rotateRC() { // �ݽð���� ȸ��
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = inImage[k][inH - i - 1];
		}
	}
	displayImage();
}
void move() { // �̵� 
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int UD = 0; int LR = 0;
	printf("���� �̵� : "); scanf("%d", &UD);
	printf("�¿� �̵� : "); scanf("%d", &LR);
	for (int i = UD; i < inH; i++) {
		for (int k = LR; k < inW; k++) {
			outImage[i][k] = inImage[i - LR][k - UD];
		}
	}
	displayImage();
}
void mirrorLR() { // �¿� ��Ī
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = inImage[i][inW - k - 1];
		}
	}
	displayImage();
}
void mirrorUD() { // ���� ��Ī
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = inImage[inH - i - 1][k];
		}
	}
	displayImage();
}
void histo_stretch() { // ��Ʈ��Ī
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH; outW = inW;
	// �� ��¿��� �迭 �غ�
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	// out = (in - low) / (high - low) * 255  --> Psudo Code
	byte low = inImage[0][0], high = inImage[0][0];
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			if (inImage[i][k] < low)
				low = inImage[i][k];
			if (inImage[i][k] > high)
				high = inImage[i][k];
		}
	}
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			double outValue = ((inImage[i][k] - (double)low) / (high - (double)low)) * 255.0;
			if (outValue > 255.0)
				outValue = 255.0;
			else if (outValue < 0.0)
				outValue = 0.0;

			outImage[i][k] = (byte)outValue;
		}
	}
	displayImage();
}
void end_in() { // ����-��
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	byte low = inImage[0][0], high = inImage[0][0];
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			if (inImage[i][k] < low)
				low = inImage[i][k];
			if (inImage[i][k] > high)
				high = inImage[i][k];
		}
	}
	low += 50; high -= 50;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			double outValue = ((inImage[i][k] - low) / (high - low)) * 255.0;
			if (outValue > 255.0)
				outValue = 255.0;
			else if (outValue < 0.0)
				outValue = 0.0;

			outImage[i][k] = (byte)outValue;
		}
	}
	displayImage();
}
void histo_equalize() { // ��Ȱȭ
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	// 1�ܰ� : ������׷� ����
	int hist[256];
	for (int i = 0; i < 256; i++)
		hist[i] = 0;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			hist[inImage[i][k]]++;
		}
	}
	// 2�ܰ� : ���� ������׷� ����
	int sumHist[256];
	int sValue = 0;
	for (int i = 0; i < 256; i++) {
		sValue += hist[i];
		sumHist[i] = sValue;
	}
	// 3�ܰ� : ����ȭ�� ���� ������׷� ����
	// n = sum / (��x��) * 255.0;
	double normalHist[256];
	for (int i = 0; i < 256; i++)
		normalHist[i] = sumHist[i] / (double)(inH * inW) * 255.0;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			outImage[i][k] = (byte)normalHist[inImage[i][k]];
		}
	}
	displayImage();
}
void embossing() { // ������ �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1.0, 0.0, 0.0},
								  {0.0, 0.0, 0.0 },
								  {0.0, 0.0, 1.0 } };  // ������ ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void blurring() { // ���� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {1/9.0, 1/9.0, 1/9.0},
								  {1/9.0, 1/9.0, 1/9.0 },
								  {1/9.0, 1/9.0, 1/9.0 } };  // ���� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void Gaussian() { // ����þ� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {1/16.0, 1/8.0, 1/16.0},
								  {1/8.0, 1/4.0, 1/8.0 },
								  {1/16.0, 1/8.0, 1/16.0 } };  // ����þ� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void sharpening1() { // ������(ȸ�� ����ũ1) �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1.0, -1.0, -1.0},
								  {-1.0, 9.0, -1.0 },
								  {-1.0, -1.0, -1.0 } };  // ȸ�� ����ũ1
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void sharpening2() { // ������(ȸ�� ����ũ2) �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {0.0, -1.0, 0.0},
								  {-1.0, 5.0, -1.0 },
								  {0.0, -1.0, 0.0 } };  // ȸ�� ����ũ2
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127*/;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void sharpening3() { // ������(������ ����) �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1/9.0, -1/9.0, -1/9.0},
								  {-1/9.0, 8/9.0, -1/9.0 },
								  {-1/9.0, -1/9.0, -1/9.0 } };  // ������ ���� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += 20 * mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void boundryLine1() { // ���� ���� ���� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {0.0, 0.0, 0.0},
								  {-1.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // ���� ���� ���� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void boundryLine2() { // ���� ���� ���� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {0.0, -1.0, 0.0},
								  {0.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // ���� ���� ���� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
double doubleABS(double x) {
	// �Ǽ��� ���� �� ���� �Լ�
	if (x >= 0) return x;
	else        return -x;
}
void similarImage() // ���� ������ �˰���
{
	// �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
	outH = inH; outW = inW;
	// ��� ���� �޸� �Ҵ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	// ���� ó�� �˰���
	const int MSIZE = 3;
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	//double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];
	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double max = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					if (doubleABS(tmpInput[i + 1][k + 1] - tmpInput[i + m][k + n]) >= max)
						max += doubleABS(tmpInput[i + 1][k + 1] - tmpInput[i + m][k + n]);
			tmpOutput[i][k] = max;
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
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = tmpOutput[i][k];

		}
	////////////////////
	displayImage();
}
void minusImage() // �� ������ �˰���
{
	// �߿�! ��� ������ ũ�� ���� --> �˰��� ����..
	outH = inH; outW = inW;
	// ��� ���� �޸� �Ҵ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	// ���� ó�� �˰���
	double max = 0;
	double mask[4] = { 0, };
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	//double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// �ӽ� �Է��� �ʱ�ȭ(0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];
	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			max = 0.0;
			mask[0] = doubleABS(tmpInput[i][k] - tmpInput[i + 2][k + 2]);
			mask[1] = doubleABS(tmpInput[i][k+1] - tmpInput[i + 2][k]);
			mask[2] = doubleABS(tmpInput[i][k+2] - tmpInput[i + 2][k]);
			mask[3] = doubleABS(tmpInput[i+1][k+2] - tmpInput[i + 1][k]);
			for (int m = 0; m < 4; m++)
				if (mask[m] >= max) max = mask[m];
			tmpOutput[i][k] = max;
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
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = tmpOutput[i][k];
		}
	////////////////////
	displayImage();
}
void RobertsImage() { // �ι��� ����ũ �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask1[MSIZE][MSIZE] = { {-1.0, 0.0, 0.0},
								  {0.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // �ι��� ����ũ(�� ����)
	double mask2[MSIZE][MSIZE] = { {0.0, 0.0, -1.0},
								  {0.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // �ι��� ����ũ(�� ����)
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0, S1 = 0.0, S2 = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S1 += mask1[m][n] * tmpInput[m + i][n + k];
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S2 += mask2[m][n] * tmpInput[m + i][n + k];
			S = S1 + S2;
			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void PrewittImage() { // ������ ����ũ �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask1[MSIZE][MSIZE] = { {-1.0, -1.0, -1.0},
								  {0.0, 0.0, 0.0 },
								  {1.0, 1.0, 1.0 } };  // ������ ����ũ(�� ����)
	double mask2[MSIZE][MSIZE] = { {1.0, 0.0, -1.0},
								  {1.0, 0.0, -1.0 },
								  {1.0, 0.0, -1.0 } };  // ������ ����ũ(�� ����)
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0, S1 = 0.0, S2 = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S1 += mask1[m][n] * tmpInput[m + i][n + k];
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S2 += mask2[m][n] * tmpInput[m + i][n + k];
			S = S1 + S2;
			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void SobelImage() { // �Һ� ����ũ �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask1[MSIZE][MSIZE] = { {-1.0, -2.0, -1.0},
								  {0.0, 0.0, 0.0 },
								  {1.0, 2.0, 1.0 } };  // ������ ����ũ(�� ����)
	double mask2[MSIZE][MSIZE] = { {1.0, 0.0, -1.0},
								  {2.0, 0.0, -2.0 },
								  {1.0, 0.0, -1.0 } };  // ������ ����ũ(�� ����)
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0, S1 = 0.0, S2 = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S1 += mask1[m][n] * tmpInput[m + i][n + k];
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S2 += mask2[m][n] * tmpInput[m + i][n + k];
			S = S1 + S2;
			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void Laplasian() { // ���ö�þ� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1.0, -1.0, -1.0},
								  {-1.0, 8.0, -1.0 },
								  {-1.0, -1.0, -1.0 } };  // ���ö�þ� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void LoG() { // �α� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 5;
	double mask[MSIZE][MSIZE] = { {0.0, 0.0, -1.0, 0.0, 0.0},
								  {0.0, -1.0, -2.0, -1.0, 0.0},
								  {-1.0, -2.0, 16.0, -2.0, 0.0}, 
	                              {0.0, -1.0, -2.0, -1.0, 0.0},
	                              {0.0, 0.0, -1.0, 0.0, 0.0} };  // �α� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 4, vector <double>(inW + 4, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 4; i++)
		for (int k = 0; k < inW + 4; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}
void DoG() { // ���� �˰���
	// �߿�! ��� ������ ũ�� ���ϱ� ---> �˰��� ����
	outH = inH;  outW = inW;
	// �� ��¿��� �迭 �غ�
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** ��¥ ����ó�� �˰��� ***//
	const int MSIZE = 9;
	double mask[MSIZE][MSIZE] = { {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0},
								  {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
								  {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
								  {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
								  {-1.0, -3.0, -1.0, 9.0, 19.0, 9.0, -1.0, -3.0, -1.0},
	                              {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
	                              {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
	                              {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
	                              {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0} };  // ���� ����ũ
	// �ӽ� ����� �޸� Ȯ��
	vector <vector <double>> tmpInput(inH + 8, vector <double>(inW + 8, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// �ӽ� �Է��� �ʱ�ȭ (0, 127, ��հ�)
	for (int i = 0; i < inH + 8; i++)
		for (int k = 0; k < inW + 8; k++)
			tmpInput[i][k] = 127.0;
	// �Է� --> �ӽ� �Է�
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// ȸ�� ����
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// �� ���� ���� ����ũ ����
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// ����ũ�� �հ谡 0�̸�, 127 ������ �����ֱ�.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// �ӽ� ��� --> ���
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
		{
			if (tmpOutput[i][k] < 0)
				outImage[i][k] = 0;
			else if (tmpOutput[i][k] > 255)
				outImage[i][k] = 255;
			else
				outImage[i][k] = (byte)(tmpOutput[i][k]);
		}
	//*****************************//
	displayImage();
}