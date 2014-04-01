// WatsDangleTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "hwDll.h"

#include <iostream>
using namespace std;

void Info(WORD wCode)
{
	switch(wCode)
	{
	case HW_SUCCESS:
		cout<<"����ִ�гɹ�"<<endl;
		break;
	case HW_ERROR:
		cout<<"����ִ��ʧ��"<<endl;
		break;
	case HW_NOT_EXIST:
		cout<<"���ܹ�������"<<endl;
		break;
	case HW_NOT_VALID:
		cout<<"���ܹ���Ч"<<endl;
		break;
	default:
		cout<<"δ֪����"<<endl;
		break;
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	WORD wRet = 0;

	//��ȡ���ܹ��ڲ��û�����
	WORD wUserCnt = 0;
	wRet = GetModuleCnt(&wUserCnt);
	cout<<"�û�������"<<wUserCnt<<endl;
	Info(wRet);

	//��ȡ�����û�����
	BYTE blUserCnt = 0;
	wRet = GetLongModuleCnt(&blUserCnt);
	cout<<"�����û�����:"<<(unsigned short)blUserCnt<<endl;
	Info(wRet);

	//��ȡʱ���û�����
	BYTE btUserCnt = 0;
	wRet = GetTimeModuleCnt(&btUserCnt);
	cout<<"ʱ���û�������"<<(unsigned short)btUserCnt<<endl;
	Info(wRet);

	//��ȡ��ǰ��Ч���û�����(�����û�+����ʱ���û�)
	WORD wValidUserCnt = 0;
	wRet = GetValidModuleCnt(&wValidUserCnt);
	cout<<"��ǰ��Ч���û�������"<<wValidUserCnt<<endl;
	Info(wRet);

	//��ȡDLL�汾��
	WORD wVersion = 0;
	wRet = GetDllVersion(&wVersion);
	cout<<"DLL �汾�ţ�"<<(unsigned short)((wVersion>>8)&0x00ff)<<"."<<(unsigned short)(wVersion&0x00ff)<<endl;
	Info(wRet);

	cin.get();
	return 0;
}

