using System;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace PipesJbt
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new NamedPipeClientStream("PodatkiFERI");
            client.Connect();
            Console.WriteLine("Povezan s strežnikom.-client");
            Console.WriteLine("Pošlji prazno vrstico za konec");
            Console.WriteLine("Vnesite prvo stevilo:");
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);

            string Vnos1= " ";
            string nekaj = "";
            while (nekaj!="END")
            {
                Vnos1 = Console.ReadLine();              
                writer.WriteLine(Vnos1);
                writer.Flush();
                nekaj = reader.ReadLine();
                if (nekaj=="END")
                {
                    client.Close();
                }
                Console.WriteLine(nekaj);
            }


          
        }
    }
}
