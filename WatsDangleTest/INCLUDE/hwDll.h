#pragma once 

#define HW_SUCCESS		0x0000 /*����ִ�гɹ�*/
#define HW_ERROR		0x0001 /*����ִ��ʧ��*/
#define HW_NOT_EXIST	0x0010   /*���ܹ�������*/
#define HW_NOT_VALID	0x0100   /*���ܹ���Ч*/

#define HW_DLL_IMPORT  __declspec(dllimport)

#ifdef __cplusplus
extern "C"{
#endif

extern HW_DLL_IMPORT WORD GetModuleCnt(WORD* pwCnt); /*��ȡ�û�������*/
extern HW_DLL_IMPORT WORD GetLongModuleCnt(BYTE* pwCnt);/*��ȡ�����û�����*/
extern HW_DLL_IMPORT WORD GetTimeModuleCnt(BYTE* pwCnt);/*��ȡʱ���û�����*/
extern HW_DLL_IMPORT WORD GetValidModuleCnt(WORD* pwCnt);/*��ȡ��ǰ��Ч�û�����*/
extern HW_DLL_IMPORT WORD GetDllVersion(WORD *pwVer);/*��ȡDLL�汾��*/

#ifdef __cplusplus
}
#endif
