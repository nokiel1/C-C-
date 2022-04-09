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

// 함수 선언부
void openImage(); void displayImage(); void saveImage(); void print_menu();
void equal(); void reverse(); void addImage(); void clear(); void faint();
void BW(); void BWavg(); void Gamma(); void HL();
void zoomIn(); void zoomOut(); void zoomOut2(); void zoomOutBack(); void rotateC(); void rotateRC();
void move(); void mirrorLR(); void mirrorUD(); void histo_stretch(); void end_in();
void PosterizingImage(); void histo_equalize(); void embossing(); void blurring();
void Gaussian(); void sharpening1(); void sharpening2(); void sharpening3();
void boundryLine1(); void boundryLine2(); void similarImage(); void minusImage();
void RobertsImage(); void PrewittImage(); void SobelImage(); void Laplasian(); void LoG(); void DoG();


// 전역 변수부
vector<vector<UC>> inImage, outImage;
int inH, inW, outH, outW;
string folderName = "C:\\images\\RAW\\";
string fileName, saveName;
HWND hwnd;
HDC hdc;
// 메인 함수
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
			printf("Open파일명 --> "); scanf("%s", fname);
			fileName = folderName + fname + ".raw";
			openImage();  break;
		case '1':
			printf("Save파일명 --> "); scanf("%s", fname);
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
		default:printf("키 잘못 누름"); break;
		}
	}
}
void print_menu() {
	puts("\t ## GrayScale 영상처리 (RC1) ##");
	puts("0. 파일 열기 1. 파일 저장 9. 프로그램 종료");
	puts("화소점 처리 --> A. 동일 B. 반전 C. 밝게/어둡게 D. 뚜렷하게 E. 희미하게\n\t\tF. 흑백 G. 흑백(평균값) H. 범위강조 I. 감마보정 J. 포스터라이징");
	puts("기하학 처리 --> L. 90도 회전(시계방향) M. 90도 회전(반시계) N. 확대 O. 확대(이웃 화소 보간) P. 축소\n\t\tQ. 이동 R. 미러링(좌우) S. 미러링(상하)");
	puts("히스토그램  --> T. 스트레칭 U. 엔드-인 V. 평활화");
	puts("화소영역 처리 --> W. 엠보싱 X. 블러링 Y. 가우시안 Z. 샤프닝(회선마스크1) !. 샤프닝(회선마스크2)\n\t\t  @. 샤프닝(고주파 필터) #. 수직 검출 $. 수평 검출 %. 유사 연산자 ^. 차 연산자\n\t\t  &. 로버츠 마스크  *. 프리윗 마스크 -. 소벨 마스크 ~. 라플라시안 `. LoG ?. DoG");
}

void printFileName() {
	struct _finddata_t fd;
	intptr_t handle;
	string path = folderName + "*.*";
	if ((handle = _findfirst(path.c_str(), &fd)) == -1L) {
		printf("파일경로 문서가 비어있습니다.");
	}

	printf("%s 안의 파일 목록\n", folderName.c_str());
	do {
		printf("%s\t", fd.name);
	} while (_findnext(handle, &fd) == 0);
};


// 공통 함수부
void openImage() {
	char fname[100];
	printFileName();
	printf("\nOpen 파일명(확장자 제외) --> ");
	scanf("%s", fname);
	fileName = folderName + fname + ".raw";

	//inH, inW 계산
	FILE* rfp = fopen(fileName.c_str(), "rb");
	if (rfp == 0) {
		system("cls");
		printf("파일명이 잘못되었습니다.");
		return;
	}
	fseek(rfp, 0L, SEEK_END); // 파일의 끝으로
	LONG fsize = ftell(rfp); // 현재위치(파일크기, inH * inW)
	inH = inW = sqrt(fsize);
	fclose(rfp);

	// 파일로부터 inImage 값 입력
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
		inImage.push_back(tmpAry); // 한 행씩 채움
	}
	fclose(rfp);
	displayImage();
}
void saveImage() {
	// 저장용 파일이름(위치포함) 만들기
	char fname[100];
	printf("Save 파일명(확장자 제외) --> ");
	scanf("%s", fname);
	saveName = folderName + fname + ".raw";

	//outImage 배열을 파일로 저장
	FILE* wfp = fopen(fileName.c_str(), "wb+");
	UC px;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			px = outImage[i][k];
			fputc(px, wfp);
		}
	}
	fclose(wfp);
	printf("%s 이 저장됨.", fname);
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

// 영상처리 함수부
void equal() { // 동일
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
void reverse() { // 반전
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
void addImage() { // 밝게/어둡게
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int num; // 명암 수치
	printf("수치 입력 -->");
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
void clear() { // 선명하게
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int num; // 밝기 수치
	printf("수치 입력 -->");
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
void faint() { // 뚜렷하게
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int num; // 밝기 수치
	printf("수치 입력 -->");
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
void BW() { // 흑백
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
void BWavg() { //흑백(평균)
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
	printf("감마 값 -->"); scanf("%lf", &num);
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
void HL() { // 범위 강조
	int start, fin;
	printf("시작 값 -->"); scanf("%d", &start);
	printf("종료 값 -->"); scanf("%d", &fin);
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
void PosterizingImage() { // 포스터라이징
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
void zoomIn() { // 축소
	int num;
	printf("축소 값 --> "); scanf("%d", &num);
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
void zoomOut() { // 확대
	int num;
	printf("확대 값 --> "); scanf("%d", &num);
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
void zoomOut2() { // 확대
	int num;
	printf("확대 값 --> "); scanf("%d", &num);
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
void rotateC() { // 시계방향 회전
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
void rotateRC() { // 반시계방향 회전
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
void move() { // 이동 
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	int UD = 0; int LR = 0;
	printf("상하 이동 : "); scanf("%d", &UD);
	printf("좌우 이동 : "); scanf("%d", &LR);
	for (int i = UD; i < inH; i++) {
		for (int k = LR; k < inW; k++) {
			outImage[i][k] = inImage[i - LR][k - UD];
		}
	}
	displayImage();
}
void mirrorLR() { // 좌우 대칭
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
void mirrorUD() { // 상하 대칭
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
void histo_stretch() { // 스트레칭
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH; outW = inW;
	// 빈 출력영상 배열 준비
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
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
void end_in() { // 엔드-인
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
void histo_equalize() { // 평활화
	outH = inH; outW = inW;
	vector<vector<UC>> tmpImage(outH, vector<UC>(outW, 0));
	outImage = tmpImage;
	// 1단계 : 히스토그램 생성
	int hist[256];
	for (int i = 0; i < 256; i++)
		hist[i] = 0;
	for (int i = 0; i < inH; i++) {
		for (int k = 0; k < inW; k++) {
			hist[inImage[i][k]]++;
		}
	}
	// 2단계 : 누적 히스토그램 생성
	int sumHist[256];
	int sValue = 0;
	for (int i = 0; i < 256; i++) {
		sValue += hist[i];
		sumHist[i] = sValue;
	}
	// 3단계 : 정규화된 누적 히스토그램 생성
	// n = sum / (행x열) * 255.0;
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
void embossing() { // 엠보싱 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1.0, 0.0, 0.0},
								  {0.0, 0.0, 0.0 },
								  {0.0, 0.0, 1.0 } };  // 엠보싱 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
void blurring() { // 블러링 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {1/9.0, 1/9.0, 1/9.0},
								  {1/9.0, 1/9.0, 1/9.0 },
								  {1/9.0, 1/9.0, 1/9.0 } };  // 블러링 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// 임시 출력 --> 출력
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
void Gaussian() { // 가우시안 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {1/16.0, 1/8.0, 1/16.0},
								  {1/8.0, 1/4.0, 1/8.0 },
								  {1/16.0, 1/8.0, 1/16.0 } };  // 가우시안 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// 임시 출력 --> 출력
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
void sharpening1() { // 샤프닝(회선 마스크1) 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1.0, -1.0, -1.0},
								  {-1.0, 9.0, -1.0 },
								  {-1.0, -1.0, -1.0 } };  // 회선 마스크1
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// 임시 출력 --> 출력
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
void sharpening2() { // 샤프닝(회선 마스크2) 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {0.0, -1.0, 0.0},
								  {-1.0, 5.0, -1.0 },
								  {0.0, -1.0, 0.0 } };  // 회선 마스크2
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127*/;
	// 임시 출력 --> 출력
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
void sharpening3() { // 샤프닝(고주파 필터) 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1/9.0, -1/9.0, -1/9.0},
								  {-1/9.0, 8/9.0, -1/9.0 },
								  {-1/9.0, -1/9.0, -1/9.0 } };  // 고주파 필터 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += 20 * mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// 임시 출력 --> 출력
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
void boundryLine1() { // 수직 에지 검출 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {0.0, 0.0, 0.0},
								  {-1.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // 수직 에지 검출 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
void boundryLine2() { // 수평 에지 검출 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {0.0, -1.0, 0.0},
								  {0.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // 수평 에지 검출 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
	// 실수의 절대 값 연산 함수
	if (x >= 0) return x;
	else        return -x;
}
void similarImage() // 유사 연산자 알고리즘
{
	// 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
	outH = inH; outW = inW;
	// 출력 영상 메모리 할당
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	// 영상 처리 알고리즘
	const int MSIZE = 3;
	// 임시 입출력 메모리 확보
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	//double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 임시 입력을 초기화(0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];
	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double max = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					if (doubleABS(tmpInput[i + 1][k + 1] - tmpInput[i + m][k + n]) >= max)
						max += doubleABS(tmpInput[i + 1][k + 1] - tmpInput[i + m][k + n]);
			tmpOutput[i][k] = max;
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
void minusImage() // 차 연산자 알고리즘
{
	// 중요! 출력 영상의 크기 결정 --> 알고리즘에 따라서..
	outH = inH; outW = inW;
	// 출력 영상 메모리 할당
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	// 영상 처리 알고리즘
	double max = 0;
	double mask[4] = { 0, };
	// 임시 입출력 메모리 확보
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	//double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 임시 입력을 초기화(0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];
	// 회선 연산
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
	//마스크의 합계가 0이면, 127 정도를 더해주기.
	//for (int i = 0; i < outH; i++)
	//    for (int k = 0; k < outW; k++)
	//        tmpOutput[i, k] += 127;
	// 임시 출력 --> 출력
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
void RobertsImage() { // 로버츠 마스크 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask1[MSIZE][MSIZE] = { {-1.0, 0.0, 0.0},
								  {0.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // 로버츠 마스크(행 검출)
	double mask2[MSIZE][MSIZE] = { {0.0, 0.0, -1.0},
								  {0.0, 1.0, 0.0 },
								  {0.0, 0.0, 0.0 } };  // 로버츠 마스크(열 검출)
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
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
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
void PrewittImage() { // 프리윗 마스크 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask1[MSIZE][MSIZE] = { {-1.0, -1.0, -1.0},
								  {0.0, 0.0, 0.0 },
								  {1.0, 1.0, 1.0 } };  // 프리윗 마스크(행 검출)
	double mask2[MSIZE][MSIZE] = { {1.0, 0.0, -1.0},
								  {1.0, 0.0, -1.0 },
								  {1.0, 0.0, -1.0 } };  // 프리윗 마스크(열 검출)
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
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
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
void SobelImage() { // 소벨 마스크 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask1[MSIZE][MSIZE] = { {-1.0, -2.0, -1.0},
								  {0.0, 0.0, 0.0 },
								  {1.0, 2.0, 1.0 } };  // 프리윗 마스크(행 검출)
	double mask2[MSIZE][MSIZE] = { {1.0, 0.0, -1.0},
								  {2.0, 0.0, -2.0 },
								  {1.0, 0.0, -1.0 } };  // 프리윗 마스크(열 검출)
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
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
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
void Laplasian() { // 라플라시안 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 3;
	double mask[MSIZE][MSIZE] = { {-1.0, -1.0, -1.0},
								  {-1.0, 8.0, -1.0 },
								  {-1.0, -1.0, -1.0 } };  // 라플라시안 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 2, vector <double>(inW + 2, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 2; i++)
		for (int k = 0; k < inW + 2; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	/*for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;*/
	// 임시 출력 --> 출력
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
void LoG() { // 로그 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 5;
	double mask[MSIZE][MSIZE] = { {0.0, 0.0, -1.0, 0.0, 0.0},
								  {0.0, -1.0, -2.0, -1.0, 0.0},
								  {-1.0, -2.0, 16.0, -2.0, 0.0}, 
	                              {0.0, -1.0, -2.0, -1.0, 0.0},
	                              {0.0, 0.0, -1.0, 0.0, 0.0} };  // 로그 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 4, vector <double>(inW + 4, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 4; i++)
		for (int k = 0; k < inW + 4; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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
void DoG() { // 도그 알고리즘
	// 중요! 출력 영상의 크기 구하기 ---> 알고리즘에 의존
	outH = inH;  outW = inW;
	// 빈 출력영상 배열 준비
	vector <vector <byte>> tmpImage(outH, vector <byte>(outW, 0));
	outImage = tmpImage;
	//*** 진짜 영상처리 알고리즘 ***//
	const int MSIZE = 9;
	double mask[MSIZE][MSIZE] = { {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0},
								  {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
								  {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
								  {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
								  {-1.0, -3.0, -1.0, 9.0, 19.0, 9.0, -1.0, -3.0, -1.0},
	                              {-1.0, -3.0, -1.0, 9.0, 9.0, 9.0, -1.0, -3.0, -1.0},
	                              {0.0, -3.0, -2.0, -1.0, -1.0, -1.0, -2.0, -3.0, 0.0},
	                              {0.0, -2.0, -3.0, -3.0, -3.0, -3.0, -3.0, -2.0, 0.0},
	                              {0.0, 0.0, 0.0, -1.0, -1.0, -1.0, 0.0, 0.0, 0.0} };  // 도그 마스크
	// 임시 입출력 메모리 확조
	vector <vector <double>> tmpInput(inH + 8, vector <double>(inW + 8, 0));
	// double[, ] tmpInput = new double[inH + 2, inW + 2];
	vector <vector <double>> tmpOutput(outH, vector <double>(outW, 0));
	//double[, ] tmpOutput = new double[outH, outW];
	// 
	// 임시 입력을 초기화 (0, 127, 평균값)
	for (int i = 0; i < inH + 8; i++)
		for (int k = 0; k < inW + 8; k++)
			tmpInput[i][k] = 127.0;
	// 입력 --> 임시 입력
	for (int i = 0; i < inH; i++)
		for (int k = 0; k < inW; k++)
			tmpInput[i + 1][k + 1] = inImage[i][k];

	// 회선 연산
	for (int i = 0; i < inH; i++)
	{
		for (int k = 0; k < inW; k++)
		{
			// 한 점에 대한 마스크 연산
			double S = 0.0;
			for (int m = 0; m < MSIZE; m++)
				for (int n = 0; n < MSIZE; n++)
					S += mask[m][n] * tmpInput[m + i][n + k];

			tmpOutput[i][k] = S;
		}
	}
	// 마스크의 합계가 0이면, 127 정도를 더해주기.
	for (int i = 0; i < outH; i++)
		for (int k = 0; k < outW; k++)
			tmpOutput[i][k] += 127;
	// 임시 출력 --> 출력
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