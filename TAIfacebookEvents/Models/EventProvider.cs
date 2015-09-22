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
            e = new Event(new DateTime(2016, 2, 16), "Protest Stowarzyszenia Wolnych Borówek", "Spotkajmy sie w warszawskim metrze. Niech każdy podróżny wie, że jesteśmy za legalizacją chodowli Borówek Hamerykańskich", "GreenPeach@greenTree.com");
            e.AddComment("seba@osiedle.pl", "wezmę plecak kanapek");
            e.AddComment("abdul@uchodzcy.eu", "to ja też wezmę plecak");
            Events.Add(e);

            e = new Event(new DateTime(2016, 2, 16), "Wybuch bomby", "Terroryzujemy Warszawskie metro. Potrzebujemy chetnych zamachowców samobójców.", "Allah@jihad.org");
            e.AddComment("abdul@uchodzcy.eu", "ja sie wysadzam");
            e.AddComment("ahmed@isis.isis", "To ja tez. Allah akbar!");
            Events.Add(e);
            
            e = new Event(new DateTime(2016, 4, 16), "Święto Gumowej Kaczuszki", "Wszyscy celebrujmy święte prawo do rozmowy z Gumową Kaczuszką.", "cmd@bash.io");
            e.AddComment("admin@admin.pl", "juhu!!");
            Events.Add(e);
        }
    }
}
