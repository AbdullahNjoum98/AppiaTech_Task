using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class HelperMethods
    {
        public static bool CustomContains<T>(List<T> list, T item)
        {
            if (list == null || list.Count == 0 || item == null) return false;
            foreach (var listItem in list)
            {
                if ((int)listItem.GetType().GetProperty("Id").GetValue(listItem) ==
                    (int)item.GetType().GetProperty("Id").GetValue(item))
                    return true;
            }
            return false;
        }
    }
}
