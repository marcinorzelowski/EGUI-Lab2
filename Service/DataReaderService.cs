using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Calendar.Models;
using System.Globalization;

namespace Calendar.Service
{
    public class DataReaderService
    {
        private List<EventDto> events = new List<EventDto>();

        public DataReaderService()
        {
            events = getAllEvents();
        }

       public List<Event> getEventsForCalendar()
        {
            System.Globalization.CultureInfo provider = CultureInfo.InvariantCulture;
            List<Event> EventList = new List<Event>();
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineElements = line.Split(",");
                        Event e = new Event();

                        e.start = lineElements[0];
                        e.title = lineElements[2];
                        EventList.Add(e);
                    }

                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return EventList;
        }

        public List<EventDto> getAllEvents()
        {
            System.Globalization.CultureInfo provider = CultureInfo.InvariantCulture;
            List<EventDto> EventList = new List<EventDto>();
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineElements = line.Split(",");
                        EventDto e = new EventDto();

                        e.start = lineElements[0];
                        e.hour = lineElements[1];
                        e.title = lineElements[2];
                        EventList.Add(e);
                    }

                }

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return EventList;
        }

        public void delEvent(string name)
        {
            events.RemoveAll(e => e.title.Trim().Equals(name.Trim()));
            saveToFile();
        }

        public EventDto findWithName(string name)
        {
            return events.FindAll(e => e.title.Equals(name))[0];// returns first 
        }


        public List<EventDto> getEventsWithDate(string date)
        {
            saveToFile();
            return events.FindAll(e => e.start.Equals(date));
            
        }




        public void saveToFile()
        {
            FileInfo fi = new FileInfo(@"data.txt");
            using (TextWriter txtWriter = new StreamWriter(fi.Open(FileMode.Truncate)))
            {
                foreach(EventDto ev in events)
                {
                    txtWriter.Write(ev.start + "," + ev.hour + "," + ev.title + Environment.NewLine);
                }
            }
        }
       
         
    }
}
