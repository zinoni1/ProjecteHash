using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProjecteHash
{
    class Program
    {
        static void Main(string[] args)
        {
            bool joc = true;

            while (joc)
            {
                Console.Clear();
                Console.WriteLine("**Menú**");
                Console.WriteLine("1. Calcular hash d'un fitxer");
                Console.WriteLine("2. Comprovar integritat d'un fitxer");
                Console.WriteLine("3. Sortir");
                Console.Write("Opció: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CalcularHash();
                        break;
                    case "2":
                        ComprovarIntegritat();
                        break;
                    case "3":
                        joc = false;
                        Console.WriteLine("Adeu");
                        break;
                    default:
                        Console.WriteLine("Opció no vàlida. Torna a intentar-ho.");
                        break;
                }

                Console.WriteLine("Clica per continuar");
                Console.ReadKey();
            }
        }

        static void CalcularHash()
        {
            Console.Write("Entra la ruta del fitxer: ");
            string rutaFitxer = Console.ReadLine();

            try
            {
                string text = File.ReadAllText(rutaFitxer);
                byte[] bytesIn = Encoding.UTF8.GetBytes(text);

                using (SHA512Managed SHA512 = new SHA512Managed())
                {
                    byte[] hashResult = SHA512.ComputeHash(bytesIn);
                    string hash = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                    string rutaHash = rutaFitxer + ".SHA";
                    File.WriteAllText(rutaHash, hash);

                    Console.WriteLine($"Hash del fitxer {rutaFitxer}: {hash}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ComprovarIntegritat()
        {
            Console.Write("Entra la ruta del fitxer: ");
            string rutaFitxer = Console.ReadLine();
            string rutaHash = rutaFitxer + ".SHA";

            try
            {
                string hashFitxer = File.ReadAllText(rutaHash);
                string text = File.ReadAllText(rutaFitxer);
                byte[] bytesIn = Encoding.UTF8.GetBytes(text);

                using (SHA512Managed SHA512 = new SHA512Managed())
                {
                    byte[] hashResult = SHA512.ComputeHash(bytesIn);
                    string hashCalculat = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                    if (hashCalculat == hashFitxer)
                    {
                        Console.WriteLine("El fitxer és intacte.");
                    }
                    else
                    {
                        Console.WriteLine("El fitxer està corrupte.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
