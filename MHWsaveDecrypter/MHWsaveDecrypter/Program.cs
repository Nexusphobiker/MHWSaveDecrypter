using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MHWsaveDecrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Drag & Drop the save onto the executable");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("MHW Save Decrypter by Nexusphobiker");
            Console.WriteLine("BACKUP YOUR SAVES");
            Console.WriteLine("Decrypt - enter 1");
            Console.WriteLine("Encrypt - enter 2");
            bool isEncrypt = Console.ReadKey().Key == ConsoleKey.D2;

            if (isEncrypt)
            {
                Console.Title = "Encrypting...";
                Stream iStream = File.Open(args[0], FileMode.Open);
                Stream oStream = File.Create("savEncrypted");
                byte[] buff = new byte[8];
                while (iStream.Position < iStream.Length)
                {
                    if (iStream.Read(buff, 0, buff.Length) != buff.Length)
                        throw (new Exception("Invalid read size"));
                    buff = MHWCrypt.Encrypt(buff);
                    oStream.Write(buff, 0, buff.Length);
                    Console.WriteLine(iStream.Position.ToString() + '/' + iStream.Length.ToString());
                }
                iStream.Flush();
                oStream.Flush();
            }
            else
            {
                Console.Title = "Decrypting...";
                Stream iStream = File.Open(args[0], FileMode.Open);
                Stream oStream = File.Create("savDecrypted");
                byte[] buff = new byte[8];
                while (iStream.Position < iStream.Length)
                {
                    if (iStream.Read(buff, 0, buff.Length) != buff.Length)
                        throw (new Exception("Invalid read size"));
                    buff = MHWCrypt.Decrypt(buff);
                    oStream.Write(buff, 0, buff.Length);
                    Console.WriteLine(iStream.Position.ToString() + '/' + iStream.Length.ToString());
                }
                iStream.Flush();
                oStream.Flush();
            }
            
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
