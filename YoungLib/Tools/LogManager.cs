using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YoungLib.Tools
{
    public class LogManager
    {
        private readonly string _logPath;

        #region Constructors
        public LogManager(string path)
        {
            _logPath = path;
        }

        public LogManager() : this(Path.Combine(Application.Root, "Log"))
        {
        }
        #endregion

        #region Methods
        public void WriteLine(string msg)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string filePath = Path.Combine(_logPath, fileName.Substring(0,4), fileName.Substring(5, 2));

            //create directory
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            try
            {
                using (var writer = new StreamWriter(Path.Combine(filePath, fileName), true))
                {
                    string time = DateTime.Now.ToString("HH:mm:ss");
                    writer.WriteLine(time + "\t" + msg);
                }
            }
            catch (Exception) // writing중에 오류가 나더라도 무시
            {
            }
        }
        #endregion
    }
}
