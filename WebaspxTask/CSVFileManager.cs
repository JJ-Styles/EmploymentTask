using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace WebaspxTask
{
    class CSVFileManager //Controls anything to do with csv Files
    {
        public List<Data> FileContents { get; set; } //Holds the Data of the CSV file in a List of Data Objects

        public CSVFileManager()
        {
            FileContents = new List<Data>(); 
        }

        public void LoadFile(string filename){
            using TextFieldParser csvParser = new TextFieldParser(filename)
            {
                CommentTokens = new string[] { "#" } //Sets up the value to recognize where the start of a comment is. 
            };
            csvParser.SetDelimiters(new string[] { "," }); //Seperates by a comma.
            csvParser.HasFieldsEnclosedInQuotes = true; //Allows the data to be enclosed within quotes.

            // Skip the row with the column names
            csvParser.ReadLine();

            while (!csvParser.EndOfData)
            {
                // Read current line fields and assign the values to the Data class, then pointer moves to the next line.
                string[] fields = csvParser.ReadFields();
                FileContents.Add(new Data
                {
                    Address = fields[0],
                    City = fields[1],
                    State = fields[2],
                    Zip = fields[3],
                    Latitude = Double.Parse(fields[4]),
                    Longitude = Double.Parse(fields[5])
                });
            }
        }

        public List<Data> SearchAdress(string address)
        {
            return FileContents.Where(x => x.Address == address).ToList(); //Uses Linq to identify where the list contains values that are the same as the given address.
        }

        public List<Data> NearByLocations(Data location)
        {
            //Uses Linq to indentify which locations have the same zip code as the given location and that it is not the location
            List<Data> sameZip = FileContents.Where(x => x.Zip == location.Zip && x != location).ToList(); 


            if (sameZip.Count == 10) //Checks that there is not only 10 locations, as this can just be returned if so.
            {
                return sameZip;
            }

            sameZip.OrderBy(x => x.Latitude).ThenBy(x => x.Longitude); // Orders the list by Latitude and Longitude to arrange those geographically closest to the location.
            return sameZip.GetRange(0, 10); //Returns the first 10 as they are now the closest to the location.
        }
    }
}
