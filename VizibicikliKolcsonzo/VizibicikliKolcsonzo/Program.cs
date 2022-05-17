using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VizibicikliKolcsonzo
{
    class Kolcsonzes
    {
        public string nev { get; set; }
        public char azon { get; set; }
        public TimeSpan elvitt { get; set; }
        public TimeSpan hozott { get; set; }


        public Kolcsonzes(string[] bemenet)
        {
            nev = bemenet[0];
            azon = char.Parse(bemenet[1]);
            TimeSpan vitt_ido = new TimeSpan();
            vitt_ido = TimeSpan.FromHours(int.Parse(bemenet[2])) + TimeSpan.FromMinutes(int.Parse(bemenet[3]));
            TimeSpan hozott_ido = new TimeSpan();
            hozott_ido = TimeSpan.FromHours(int.Parse(bemenet[4])) + TimeSpan.FromMinutes(int.Parse(bemenet[5]));
            elvitt = vitt_ido;
            hozott = hozott_ido;
        }


        public static List<Kolcsonzes> Beolvas(string fajlnev)
        {
            List<Kolcsonzes> kolcsonzesek = new List<Kolcsonzes>();

            StreamReader sr = new StreamReader(fajlnev);
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string[] adatok = sor.Split(';');
                Kolcsonzes kolcson = new Kolcsonzes(adatok);

                kolcsonzesek.Add(kolcson);
            }
            sr.Close();
            return kolcsonzesek;
        }




    }



    internal class Program
    {
        static void Main(string[] args)
        {

            //4. feladat
            List<Kolcsonzes> ViziB = Kolcsonzes.Beolvas("kolcsonzesek.txt");


            //5. feladat
            Console.WriteLine("5. feladat: napi kölcsönzések száma: {0}", ViziB.Count);


            //6. feladat
            Console.Write("6. feladat: Kérek egy nevet: ");
            string bekeres = Console.ReadLine();
            Console.WriteLine("\t{0} kölcsönzései:", bekeres);
            bool talal = false;
            for (int i = 0; i < ViziB.Count; i++)
            {
                if(ViziB[i].nev == bekeres)
                {
                    talal = true;
                    Console.WriteLine("\t{0}:{1}-{2}:{3}",
                        ViziB[i].elvitt.Hours, ViziB[i].elvitt.Minutes, ViziB[i].hozott.Hours, ViziB[i].hozott.Minutes);
                }
            }
            if(!talal)
                Console.WriteLine("Nem volt ilyen nevű kölcsönző!");


            //7. feladat
            Console.Write("7. feladat: Adjon mege egy időpontot óra:perc alakban: ");
            bekeres = Console.ReadLine();
            Console.WriteLine("\tA vízen lévő járművek:");
            string[] konvert = bekeres.Split(':');
            TimeSpan bekert_ido = TimeSpan.FromHours(int.Parse(konvert[0])) + TimeSpan.FromMinutes(int.Parse(konvert[1]));
            for (int i = 0; i < ViziB.Count; i++)
            {
                if(ViziB[i].elvitt < bekert_ido && ViziB[i].hozott > bekert_ido)
                {
                    Console.WriteLine("\t{0}:{1}-{2}:{3} : {4}",
                        ViziB[i].elvitt.Hours, ViziB[i].elvitt.Minutes, ViziB[i].hozott.Hours, ViziB[i].hozott.Minutes, ViziB[i].nev);
                }
            }


            //8. feladat
            double napi_bev = 0;
            double osszes_perc = 0;
            for (int i = 0; i < ViziB.Count; i++)
            {
                osszes_perc += ViziB[i].hozott.TotalMinutes - ViziB[i].elvitt.TotalMinutes;
            }
            napi_bev = (osszes_perc / 30) * 2400;
            Console.WriteLine("8. feladat: A napi bevétel: {0} Ft", napi_bev);


            //9. feladat
            StreamWriter sw = new StreamWriter("F.txt",false,Encoding.UTF8);
            for (int i = 0; i < ViziB.Count; i++)
            {
                if (ViziB[i].azon == 'F')
                {
                    sw.WriteLine("\t{0}:{1}-{2}:{3} : {4}",
                            ViziB[i].elvitt.Hours, ViziB[i].elvitt.Minutes, ViziB[i].hozott.Hours, ViziB[i].hozott.Minutes, ViziB[i].nev);
                }
            }
            sw.Close();


            //10. feladat
            List<char> azonositok = new List<char>();
            for (int i = 0; i < ViziB.Count; i++)
            {
                if (!azonositok.Contains(ViziB[i].azon))
                    azonositok.Add(ViziB[i].azon);

            }
            azonositok.Sort();

            int[] kolcsonzok_szama = new int[azonositok.Count];
            for (int i = 0; i < ViziB.Count; i++)
            {
                for (int y = 0; y < azonositok.Count; y++)
                {
                    if (azonositok[y] == ViziB[i].azon)
                        kolcsonzok_szama[y]++;
                }
            }

            for (int i = 0; i < azonositok.Count; i++)
            {
                Console.WriteLine("{0} - {1}",
                    azonositok[i], kolcsonzok_szama[i]);
            }


            Console.ReadKey();
        }
    }
}
