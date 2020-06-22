using System;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        public static bool PreveriStevilo(bool nekaj)
        {

            if (nekaj==true)
            {
                return true;
            }
            
            return false;
        }

        public static int Rezultat(int Prvo, string znak, int Drugo)
        {
            if (znak == "+") return Prvo + Drugo;
            if (znak == "-") return Prvo - Drugo;
            if (znak == "/") return Prvo / Drugo;
            if (znak == "*") return Prvo * Drugo;

            return 0;
            
        }

        public static bool PreveriZnak(string Znak) 
        {
            string[] znaki = { "+", "-", "/", "*" };
            for (int i = 0; i < znaki.Length; i++)
            {
                if (znaki[i] == Znak) return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            var server = new NamedPipeServerStream("PodatkiFERI");
            Console.WriteLine("Strežnik je pripravljen-STREŽNIK");
            server.WaitForConnection();
            Console.WriteLine("Strežnik je povezan");
            StreamReader reader = new StreamReader(server);
            StreamWriter writer = new StreamWriter(server);


            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;
            bool flag7 = true;
            string Nadaljevati = "";
            string Vnos="";
            string Vnos2 ;
            int PrvoStevilo=0;
            int DrugoStevilo=0;
            string Znak="";

            while (server.IsConnected)
            {

                if (flag7==false)
                {
                    server.Close();
                }
                while (flag1)
                {
                    
                    while (flag6)
                    {
                        Vnos = reader.ReadLine();

                        if (int.TryParse(Vnos, out PrvoStevilo))
                        {
                            writer.WriteLine($"Stevilka {Vnos} je potrjena.Vnesite znak:");
                            writer.Flush();
                            flag6 = false;

                        }
                        else
                        {
                            writer.WriteLine($"Stevilka {Vnos} Ni potrjena! Prosim ponovno vnesite: ");
                            writer.Flush();
                            flag6 = true;
                            
                        }
                    }


                    while (flag2)
                    {
                        while (flag3)
                        {
                            
                            if (PreveriZnak(Znak))
                            {
                                writer.WriteLine("Podaj znak: ");
                                writer.Flush();
                            }
                            Znak = reader.ReadLine();
                            if (PreveriZnak(Znak))
                            {
                                writer.WriteLine($"Znak {Znak} je potrjen! Vnesite drugo stevilo: ");
                                writer.Flush();
                                flag3 = false;
                            }
                            else
                            {
                                writer.WriteLine($"Znak {Znak} Ni potrjen! Prosim ponoven vnos: ");
                                writer.Flush();
                                flag3 = true;
                            }
                        }



                        while (flag4)
                        {
                            Vnos2 = reader.ReadLine();

                            if (Znak == "/" && Vnos2=="0")
                            {
                                writer.WriteLine($"Ni mogoče deliti z 0! Prosim ponovno vnesite: ");
                                writer.Flush();
                                flag4 = true;

                            }else if (int.TryParse(Vnos2, out DrugoStevilo))
                            {
                                flag4 = false;
                                writer.WriteLine($"Rezultat je {Rezultat(PrvoStevilo,Znak,DrugoStevilo)}. Želite nadaljevati? (DA/NE)");
                                writer.Flush();
                                

                            }
                            else
                            {
                                writer.WriteLine($"Stevilka {Vnos2} Ni potrjena! Prosim ponovno vnesite: ");
                                writer.Flush();
                                flag4 = true;

                            }

                        }
                        flag5 = true;
                        while (flag5)
                        {
                            Nadaljevati = reader.ReadLine();
                            
                            if (Nadaljevati == "DA")
                            {
                                flag5 = false;
                                flag2 = true;
                                flag3 = true;
                                flag4 = true;
                                
                               
                                
                                PrvoStevilo = Rezultat(PrvoStevilo, Znak, DrugoStevilo);

                            }
                            if (Nadaljevati == "NE")
                            {
                                
                                writer.WriteLine("END");
                                writer.Flush();
                                flag1 = false;
                                flag2 = false;
                                flag3 = false;
                                flag4 = false;
                                flag5 = false;
                                flag6 = false;
                                flag7 = false;
                                
                            }
                            if(Nadaljevati!="DA" && Nadaljevati!="NE")
                            {
                                writer.WriteLine("(DA/NE)");
                                writer.Flush();
                                flag5 = true;
                            }


                        }
                       





                    }
                }
                





               



                
                
            
                
            }
        }
    }
}
