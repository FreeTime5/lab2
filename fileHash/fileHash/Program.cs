using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using SHA3.Net;
namespace fileHash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var email = "*******@gmail.com";
            var currentDir = "C:\\******\\*****\\******\\*******\\lab2\\fileHash\\fileHash";
            var dirPath = currentDir + "\\extractedFiles";
            if (!Directory.Exists(dirPath))
            {
                ZipFile.ExtractToDirectory(currentDir + "\\task2.zip", dirPath);
            }

            var files = Directory.GetFiles(dirPath);
            var filesByte = new List<byte[]>();
            foreach (var file in files)
            {
                filesByte.Add(File.ReadAllBytes(file));
            }

            var hashOfFiles = new List<string>();
            using (var sha = Sha3.Sha3256())
            {
                foreach (var by in filesByte)
                {
                    hashOfFiles.Add(Convert.ToHexString(sha.ComputeHash(by)).ToLower());
                }
            }

            hashOfFiles.Sort();
            var builder = new StringBuilder();
            foreach (var str in hashOfFiles)
            {
                builder.Append(str);
            }
            builder.Append(email);

            var bytes = Encoding.UTF8.GetBytes(builder.ToString());
            using (var sha = Sha3.Sha3256())
            {
                Console.WriteLine(Convert.ToHexString(sha.ComputeHash(bytes)).ToLower());
            }

        }
    }
}
