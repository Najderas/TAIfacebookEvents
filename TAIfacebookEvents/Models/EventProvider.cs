using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIfacebookEvents.Models
{
    class EventProvider
    {
        public static List<Event> Events { get; set; }

        public static IEnumerable<Event> getEvents()
        {
            if (Events == null) Events = new List<Event>();
            if (!Events.Any()) createdata();
            return Events.Where(x => true);

        }

        public static IEnumerable<Event> getEvent(int i)
        {
            if (Events == null) Events = new List<Event>();
            if (!Events.Any()) createdata();
            return Events.Where(e => e.ID == i);
        }

        public static void createdata()
        {
            Event e;
            e = new Event(new DateTime(2016, 2, 16), "wybuch bomby", "niszczymy WTC", "Allah");
            e.AddComment("abdul", "ja sie wysadzam");
            e.AddComment("ahmed", "allah akbar");
            Events.Add(e);

            e = new Event(new DateTime(2016, 3, 16), "wybuch bomby2", "niszczymy WTC2", "Allah");
            e.AddComment("abdul", "ja juz sie wysadzalem");
            e.AddComment("ahmed", "to ja teraz");
            Events.Add(e);

            e = new Event(new DateTime(2016, 4, 16), "wybuch bomby3", "niszczymy WTC3", "Allah");
            e.AddComment("abdul", "ja juz sie wysadzalem");
            e.AddComment("ahmed", "ja tysz");
            e.AddComment("mohammed", "tyraz jo");
            Events.Add(e);
        }
    }
}
