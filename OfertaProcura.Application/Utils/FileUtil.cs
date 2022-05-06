using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace OfertaProcura.Utils
{
    public class FileUtil
    {
        public static string SaveFile(string base64, string basePath, Guid idUsuario)
        {
            string caminho = string.Concat(basePath, idUsuario);

            var pathFile = string.Empty;

            if (!Directory.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }

            string outputFileName = $@"{DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "")}_{DateTime.Now.Millisecond}{GetFileExtension(base64)}";

            pathFile = Path.Combine(caminho, outputFileName);
            File.WriteAllBytes(pathFile, Convert.FromBase64String(base64));

            return pathFile;
        }

        public static string SaveFileImgProfile(string base64, string basePath, Guid idUsuario)
        {
            basePath += "Perfil\\";

            string caminho = string.Concat(basePath, idUsuario);

            var pathFile = string.Empty;

            if (!Directory.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }

            string outputFileName = $@"{DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "")}_{DateTime.Now.Millisecond}{GetFileExtension(base64)}";

            pathFile = Path.Combine(caminho, outputFileName);
            File.WriteAllBytes(pathFile, Convert.FromBase64String(base64));

            return pathFile;
        }

        public static byte[] GetHashMD5(string pathArquivo) // obtem o MD5 para comparar se a foto foi alterada com a que ja foi salva
        {
            MD5 md5 = MD5.Create();

            using FileStream stream = File.OpenRead(pathArquivo);
            return md5.ComputeHash(stream);
        }

        public static string FindFile(string pathFile)
        {
            string result = string.Empty;

            if (File.Exists(pathFile) == true)
            {
                byte[] FileBytes = File.ReadAllBytes(pathFile);
                result = Convert.ToBase64String(FileBytes, 0, FileBytes.Length);
            }

            return result;
        }

        public static void DeleteFile(string pathFile)
        {
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }
        }

        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return string.Empty;
            }
        }
    }
}
