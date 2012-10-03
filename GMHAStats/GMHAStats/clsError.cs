using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GMHAStats
{
    public class clsError
    {
        public static void SendError(Exception e)
        {
            Process.Start("mailto:rick@rtbsoft.com?subject=An error occurred in GMHAStats&body=" + e.Message + "\r\n" + e.StackTrace);
        }
    }
}
