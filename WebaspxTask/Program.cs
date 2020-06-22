using System;
using System.Collections.Generic;
using System.Linq;

namespace WebaspxTask
{
    class Program
    {
        static void Main(string[] args) {
            FileManager fileManager = new FileManager();
            fileManager.LoadFile("C:/Users/User/source/repos/WebaspxTask/WebaspxTask/Data.csv");

            int x = 0;
            List<Data> addresses = fileManager.SearchAdress(GetAddress());
            foreach (Data i in addresses)
            {
                Console.WriteLine(x + ": " + i.Address + " " + i.City + " " + i.State + " " + i.Zip + " " + i.Latitude + " " + i.Longitude);
                x++;
            }

            foreach (Data i in fileManager.NearByLocations(addresses.ElementAt(GetClosestLocations())))
            {
                Console.WriteLine(i.Address + " " + i.City + " " + i.State + " " + i.Zip + " " + i.Latitude + " " + i.Longitude);
            }

            Console.ReadLine();
        }

        static string GetAddress()
        {
            Console.WriteLine("Please enter the address you would like to search");
            return Console.ReadLine();
        }

        static int GetClosestLocations()
        {
            while (true)
            {
                Console.WriteLine("Please Select an Address by entering the row number.");
                string temp = Console.ReadLine();
                try
                {
                    return int.Parse(temp);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a Integer");
                }
            }
        }
    }
}
