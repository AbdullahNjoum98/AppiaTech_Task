using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAPI
{
    public static class HelperMethods
    {
        public static string getException(Exception exception) {
            string formattedException=exception.Message;
            if (exception.InnerException != null)
            {
                formattedException+= getException(exception.InnerException);
            }
            return formattedException;
        }
    }
}
