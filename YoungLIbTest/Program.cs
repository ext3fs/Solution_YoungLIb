using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoungLib.Tools;
using YoungLib.Extensions;

namespace YoungLIbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var log = new LogManager();
            //log.WriteLine("test");

            //string s = null;
            //Console.WriteLine(s.IsNumeric());

            //EmailManager.Send("ext2fs@naver", "subject", "content.......");

            EmailManager email = new EmailManager("smtp.com", 25, "id", "password");
            email.From = "sender@test.com";
            email.To.Add("reciever@ttt.com");
            email.Subject = "subject";
            email.Body = "content";
            email.Send();
        }
    }
}
