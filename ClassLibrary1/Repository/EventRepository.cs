using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;

using Microsoft.Extensions.Logging;

namespace ClassLibrary1.Repository
{
    public class EventRepository
    {
        private EventsContext db = new EventsContext(); // use DataBase with this Repository

        /* ---------------- */
        // Create new event
        public int RepositoryCreateEvent(Event newEvent)
        {
            db.Events.Add(newEvent);
            db.SaveChanges();
            return newEvent.Id;
        }

        /* ---------------- */
        // Get users name by event
        public List<string> FetchUserNamesByEvent(int eventID)
        {
            return db.EventUsers.Where(e => e.EventRef == eventID).Select(u => u.UserRefNavigation.Name).ToList();
        }

        /* ---------------- */
        // Register User to Event
        public void RepositoryRegUserToEvent(int eventID, int userID)
        {
            EventUser registration = new EventUser
            {
                EventRef = eventID,
                UserRef = userID,
                Creation = DateTime.Now
            };

            db.EventUsers.Add(registration);
            db.SaveChanges();
        }

        /* ---------------- */
        // GET Event's Information By ID
        public Event? FetchEventByID(int eventID)
        {
            return db.Events.FirstOrDefault(e => e.Id == eventID);
        }


        /* ---------------- */
        // Update Existing Event
        public void RepositoryUpdateEvent(Event EventToUpdate)
        {
            db.Events.Update(EventToUpdate);
            db.SaveChanges();
        }


        /* ---------------- */
        // Delete Existing Event
        public void RepositoryDeleteEvent(int eventID)
        {
            // Delete from EventUser
            var regs = db.EventUsers.Where(e => e.EventRef == eventID).ToList(); // list all
            foreach (var reg in regs)
            {
                db.EventUsers.Remove(reg);
            }
            // Delete from Event
            var ev = db.Events.First(e => e.Id == eventID);
            db.Events.Remove(ev);

            db.SaveChanges();
        }


        /* ---------------- */
        // Repository get Schedule range Events
        public List<Event> RepositoryScheduledEvents(DateTime startDate, DateTime endDate)
        {
            return db.Events.Where(e => e.StartDate >= startDate && e.EndDate <= endDate).ToList();
        }
    }
}
