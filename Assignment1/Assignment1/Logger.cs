using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Assignment1.ClassFiles
{
    public static class Logger
    {
        private static StreamWriter Writer;

        static Logger()
        {
            string DatetimeFormat = "yyyy-MM-dd HH-mm-ss-fff";
            string Filename = @"C:\Users\Aditi\Desktop\gitproject\A00446685_MCDA5510\Assignment1\logs\"
                        + "log_" + DateTime.Now.ToString(DatetimeFormat) + ".txt";

            Writer = new StreamWriter(Filename, true, Encoding.UTF8);

            if (Writer == null)
            {
                Console.WriteLine("Error initializing Writer.");
            }
            //string logHeader = Filename + " is created.";

            //  if (!File.Exists(Filename))
            //  {
            //      //WriteLine(DateTime.Now.ToString(DatetimeFormat) + " " + logHeader, false);
            //  }
            //  else
            //  {
            //      // if (append == false)
            //      //     WriteLine(DateTime.Now.ToString(DatetimeFormat) + " " + logHeader, false);
            //  }
        }



        public static void Log(string text)
        {
            if (text != "")
            {
                Writer.WriteLine("\n##\n");
                Writer.WriteLine(text);
            }
        }

        public static void FinishWriting()
        {
            Writer.Flush();
        }

    }
}
