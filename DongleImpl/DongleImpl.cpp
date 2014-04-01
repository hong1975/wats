// DongleImpl.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "jni.h"
#include "hwDll.h"
#include "Dongle.h"

JNIEXPORT jint JNICALL Java_Dongle_getAvailableUserCount(JNIEnv *, jobject)
{
	WORD wRet = 0;
	WORD wValidUserCnt = 0;
	wRet = GetValidModuleCnt(&wValidUserCnt);

	return wValidUserCnt;
}


