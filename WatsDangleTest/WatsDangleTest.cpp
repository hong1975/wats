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
		cout<<"函数执行成功"<<endl;
		break;
	case HW_ERROR:
		cout<<"函数执行失败"<<endl;
		break;
	case HW_NOT_EXIST:
		cout<<"加密狗不存在"<<endl;
		break;
	case HW_NOT_VALID:
		cout<<"加密狗无效"<<endl;
		break;
	default:
		cout<<"未知错误"<<endl;
		break;
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	WORD wRet = 0;

	//获取加密狗内部用户总数
	WORD wUserCnt = 0;
	wRet = GetModuleCnt(&wUserCnt);
	cout<<"用户总数："<<wUserCnt<<endl;
	Info(wRet);

	//获取永久用户总数
	BYTE blUserCnt = 0;
	wRet = GetLongModuleCnt(&blUserCnt);
	cout<<"永久用户总数:"<<(unsigned short)blUserCnt<<endl;
	Info(wRet);

	//获取时间用户总数
	BYTE btUserCnt = 0;
	wRet = GetTimeModuleCnt(&btUserCnt);
	cout<<"时间用户总数："<<(unsigned short)btUserCnt<<endl;
	Info(wRet);

	//获取当前有效的用户总数(永久用户+部分时间用户)
	WORD wValidUserCnt = 0;
	wRet = GetValidModuleCnt(&wValidUserCnt);
	cout<<"当前有效的用户总数："<<wValidUserCnt<<endl;
	Info(wRet);

	//获取DLL版本号
	WORD wVersion = 0;
	wRet = GetDllVersion(&wVersion);
	cout<<"DLL 版本号："<<(unsigned short)((wVersion>>8)&0x00ff)<<"."<<(unsigned short)(wVersion&0x00ff)<<endl;
	Info(wRet);

	cin.get();
	return 0;
}

