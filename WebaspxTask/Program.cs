using System;
using System.Collections.Generic;
using System.Linq;

namespace WebaspxTask
{
    class Program
    {
        static void Main(string[] args) {
            CSVFileManager fileManager = new CSVFileManager();
            fileManager.LoadFile("C:/Users/User/source/repos/WebaspxTask/WebaspxTask/Data.csv"); //send the file location to the class to load it.

            int x = 0; //Used for row numbering so the user can easily identify the row they require.
            List<Data> addresses = fileManager.SearchAdress(GetAddress()); //Keeps the addresses in a temp list as they are required later.
            foreach (Data i in addresses) //Displays each Row of Data.
            {
                Console.WriteLine(x + ": " + i.Address + " " + i.City + " " + i.State + " " + i.Zip + " " + i.Latitude + " " + i.Longitude);
                x++;
            }

            //Calls the method to collect the near by locations by getting the Data location from the list by the index of where it is in the List
            foreach (Data i in fileManager.NearByLocations(addresses.ElementAt(GetClosestLocations()))) //Displays each Row of Data
            {
                Console.WriteLine(i.Address + " " + i.City + " " + i.State + " " + i.Zip + " " + i.Latitude + " " + i.Longitude);
            }

            Console.ReadLine(); //Allow for the user to read the Data
        }

        //Gets the address the user wishes to search
        static string GetAddress()
        {
            Console.WriteLine("Please enter the address you would like to search");
            return Console.ReadLine();
        }

        //Gets the Location the user wants to find the closest locations of by getting the row number for the location.
        static int GetClosestLocations()
        {
            while (true)
            {
                Console.WriteLine("Please Select an Address by entering the row number.");
                string temp = Console.ReadLine();

                //Try catch used to make sure value is a Integer.
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
