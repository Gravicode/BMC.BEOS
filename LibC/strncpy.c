#include "../Kernel/BEOS.h"

void strncpy_(char* _dst, const char* _src, long _n)
{
	long i = 0;
	while (i++ != _n && (*_dst++ = *_src++));
}