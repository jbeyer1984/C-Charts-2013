using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts
{
    public class Helper
    {
        public static void getTrace()
        {
            StackTrace stackTrace = new StackTrace(true);           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();
            foreach (StackFrame stackFrame in stackTrace.GetFrames()) {
                if (null != stackFrame.GetFileName()) {
                    //Console.WriteLine("__FILE__ : {0} , __METHOD__ : {1}", stackFrame.GetFileName(), stackFrame.GetMethod().Name);   // write method name
                }
            }
        }
    }
}
