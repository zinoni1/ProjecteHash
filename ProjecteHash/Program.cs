using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjecteHash
{
    class Program
    {
        static void Main(string[] args)
        {
            String textIn = null;
            Console.Write("Entra text: ");
            Boolean joc = true;
            while (joc)
            {
                textIn = null;
                while (string.IsNullOrEmpty(textIn) && textIn != "1" && textIn != "2")
                {
                    //menu per decidir si calcular el hash o comprovar la integritat
                    Console.Clear();
                    Console.WriteLine("**Menú**");
                    Console.WriteLine("1. Calcular hash d'un fitxer");
                    Console.WriteLine("2. Comprovar integritat d'un fitxer");
                    Console.WriteLine("3. Sortir");
                    Console.Write("Opció: ");
                    textIn = Console.ReadLine();
                }
                //si escull la primera opcio
                if (textIn == "1")
                {
                    string rutaFitxer = "";
                    //ha de escriure la ruta
                    while (string.IsNullOrEmpty(rutaFitxer))
                    {
                        Console.Write("Entra la ruta del fitxer: ");
                        rutaFitxer = Console.ReadLine();
                    }
                    //try-catch capturar algun error
                    try
                    {
                         //llegeix el tot el text del fitxer
                        string text = File.ReadAllText(rutaFitxer);
                        //aqui fa la conversió a bytes
                        byte[] bytesIn = Encoding.UTF8.GetBytes(text);
                        SHA512Managed SHA512 = new SHA512Managed();
                        {
                            //calculem el hash dels bytes
                            byte[] hashResult = SHA512.ComputeHash(bytesIn);
                            //fem que el hash es converteixi en un string
                            string hash = BitConverter.ToString(hashResult).Replace("-", string.Empty);
                            //li posem la extensio .SHA
                            string rutaHash = rutaFitxer + ".SHA";
                            //escriu el hash(primer larxiu i despres el text que s'escriura a l'arxiu
                            File.WriteAllText(rutaHash, hash);
                            //escriu la ruta i el hash a la consola
                            Console.WriteLine("Hash del fitxer {0}: {1}", rutaFitxer, hash);
                        }
                        SHA512.Dispose();
                    }
                    //capturar "l'exepcio" i escriure el missatge derror
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                //si escull la segona opcio
                if (textIn == "2")
                {
                    string rutaFitxer = "";
                    //ha de escriure la ruta un altre cop
                    while (string.IsNullOrEmpty(rutaFitxer))
                    {
                        Console.Write("Entra la ruta del fitxer: ");
                        rutaFitxer = Console.ReadLine();
                    }
                    //li posem la extensio .SHA
                    string rutaHash = rutaFitxer + ".SHA";
                    //llegir el tot el fitxer
                    string hashFitxer = File.ReadAllText(rutaHash);
                    //llegir el tot el fitxer
                    string text = File.ReadAllText(rutaFitxer);
                    //aqui fa la conversió a bytes
                    byte[] bytesIn = Encoding.UTF8.GetBytes(text);
                    SHA512Managed SHA512 = new SHA512Managed();
                    {   //calculem el hash dels bytes
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);
                        //fem que el hash es converteixi en un string
                        string hashCalculat = BitConverter.ToString(hashResult).Replace("-", string.Empty);
                        //si son iguals mostra un missatge si no mostra un altre
                        if (hashCalculat == hashFitxer)
                        {
                            Console.WriteLine("El fitxer és intacte.");
                        }
                        else
                        {
                            Console.WriteLine("El fitxer està corrupte.");
                        }
                    }
                    SHA512.Dispose();
                }
                if(textIn == "3")
                {
                    joc = false;
                    Console.WriteLine("Adeu");
                }
                Console.WriteLine("Clica per continuar");
                Console.ReadKey();
            }
        }
    }
}
