#pragma once 

#define HW_SUCCESS		0x0000 /*函数执行成功*/
#define HW_ERROR		0x0001 /*函数执行失败*/
#define HW_NOT_EXIST	0x0010   /*加密狗不存在*/
#define HW_NOT_VALID	0x0100   /*加密狗无效*/

#define HW_DLL_IMPORT  __declspec(dllimport)

#ifdef __cplusplus
extern "C"{
#endif

extern HW_DLL_IMPORT WORD GetModuleCnt(WORD* pwCnt); /*获取用户总数量*/
extern HW_DLL_IMPORT WORD GetLongModuleCnt(BYTE* pwCnt);/*获取永久用户数量*/
extern HW_DLL_IMPORT WORD GetTimeModuleCnt(BYTE* pwCnt);/*获取时间用户数量*/
extern HW_DLL_IMPORT WORD GetValidModuleCnt(WORD* pwCnt);/*获取当前有效用户数量*/
extern HW_DLL_IMPORT WORD GetDllVersion(WORD *pwVer);/*获取DLL版本号*/

#ifdef __cplusplus
}
#endif
