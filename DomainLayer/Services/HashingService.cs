using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheMailClient.Domain.Model;

// http://msdn.microsoft.com/en-us/library/xa627k19%28v=vs.110%29.aspx
//http://msdn.microsoft.com/en-us/library/s02tk69a%28v=vs.110%29.aspx
namespace TheMailClient.Domain.Services
{
    public class HashingService
    {
        private static HashingService instance = new HashingService();

        public static HashingService getInstance()
        {
            return instance;
        }

        private HashingService()
        {
        }

        public void GenerateId(User u, Contact c)
        {
            string s = u.Username + new DateTime().Ticks.ToString();

            c.ID = s;
        }

        public string HashPassword(string password)
        {
            return password;
            //Hashing commented for debugging.
            //using (MD5 hasher = MD5.Create())
            //{
            //    password += "bwa"; //salt

            //    // https://stackoverflow.com/questions/472906/converting-a-string-to-byte-array
            //    byte[] bytes = new byte[password.Length * sizeof(char)];
            //    System.Buffer.BlockCopy(password.ToCharArray(), 0, bytes, 0, bytes.Length);

            //    byte[] pw = hasher.ComputeHash(bytes);

            //    // // https://stackoverflow.com/questions/472906/converting-a-string-to-byte-array
            //    char[] chars = new char[pw.Length / sizeof(char)];
            //    System.Buffer.BlockCopy(pw, 0, chars, 0, pw.Length);

            //    return new string(chars);
            //}
        }
    }
}