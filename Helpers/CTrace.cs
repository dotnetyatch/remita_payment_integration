using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RemitaMiddleWare
{
    public class CTrace
    {
        protected string time;
        protected string format;
        protected string module = "Remita";

        public CTrace()
        {
            format = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + module + "----";
            string yr = DateTime.Now.Year.ToString();
            string mnth = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            time = yr + mnth + day;
        }
        public void logger(string errormsg)
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"C:\APILogs\Errors" + "_" + time + ".txt", true);
                writer.WriteLine(format + errormsg);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                string message = ex.ToString() + format + errormsg;
                File.AppendAllText("c:\\APILogs\\err.txt", message);
            }


        }
        public void argsLogger(string args)
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"C:\APILogs\Args" + "_" + time + ".txt", true);
                writer.WriteLine(format + args);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                string message = ex.ToString() + format;
                File.AppendAllText("c:\\APILogs\\err2.txt", message);
            }
        }

    }
}
