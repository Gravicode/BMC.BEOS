#include "../Kernel/BEOS.h"

int
abs_(int i)
{
	return i < 0 ? -i : i;
}