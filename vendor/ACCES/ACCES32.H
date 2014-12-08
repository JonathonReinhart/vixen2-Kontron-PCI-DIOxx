// ACCES32.h : Header file for ACCES32.dll
//
//For Microsoft Visual C++, use VCACCES.lib
//For Borland C++ Builder, use CBACCES.lib

#ifdef __cplusplus
extern "C" {
#endif

__declspec(dllimport) unsigned short InPort(unsigned long Port);
__declspec(dllimport) unsigned short InPortB(unsigned long Port);
__declspec(dllimport) unsigned long InPortL(unsigned long Port);
__declspec(dllimport) unsigned long InPortDWord(unsigned long Port);
__declspec(dllimport) unsigned short OutPort(unsigned long Port, unsigned short Value);
__declspec(dllimport) unsigned short OutPortB(unsigned long Port, unsigned char Value);
__declspec(dllimport) unsigned short OutPortL(unsigned long Port, unsigned long Value);
__declspec(dllimport) unsigned short OutPortDWord(unsigned long Port, unsigned long Value);

#ifdef __cplusplus
}
#endif