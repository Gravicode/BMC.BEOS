using BEOS.Drawing.UI;
using System.Collections.Generic;

namespace BEOS.Glide
{
    
        public static class CollectionExtension
        {
            public static bool Contains(this List<object> arr, object obj)
            {
                for(var i = 0; i < arr.Count; i++)
                {
                    if (arr[i] == obj)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    
}
