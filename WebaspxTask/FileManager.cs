using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace WebaspxTask
{
    class FileManager
    {
        public List<Data> FileContents { get; set; }

        public FileManager()
        {
            FileContents = new List<Data>();
        }

        public void LoadFile(string filename){
            using TextFieldParser csvParser = new TextFieldParser(filename)
            {
                CommentTokens = new string[] { "#" }
            };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            // Skip the row with the column names
            csvParser.ReadLine();

            while (!csvParser.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
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
            return FileContents.Where(x => x.Address == address).ToList();
        }

        public List<Data> NearByLocations(Data location)
        {
            List<Data> sameZip = FileContents.Where(x => x.Zip == location.Zip && x != location).ToList();

            if (sameZip.Count == 10)
            {
                return sameZip;
            }

            sameZip.OrderBy(x => x.Latitude).ThenBy(x => x.Longitude);
            return sameZip.GetRange(0, 10);
        }
    }
}
