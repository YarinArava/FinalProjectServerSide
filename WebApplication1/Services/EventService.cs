using ClassLibrary1.Models;
using ClassLibrary1.Repository;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
namespace WebApplication1.Services
{
    public class EventService
    {
        private readonly EventRepository _eventRepository;
        public EventService(EventRepository eventRepository) // Service constructor
        {
            this._eventRepository = eventRepository;
        }


        /* ---------------- */
        // Service - Create Event
        public int ServiceCreateEvent(Event newEvent)
        {
           return _eventRepository.RepositoryCreateEvent(newEvent);
        }


        /* ---------------- */
        // Service - get Users names by EventID
        public List<string> retrieveUserNamesByEvent(int eventID)
        {
            return _eventRepository.FetchUserNamesByEvent(eventID);
        }


        /* ---------------- */
        // Service - Register User to Event
        public void ServiceRegUserToEvent(int eventID, int UserID)
        {
            _eventRepository.RepositoryRegUserToEvent(eventID, UserID);
        }


        /* ---------------- */
        // GET Event's Information By ID
        public Event retrieveEventbyID(int eventID)
        {
           return _eventRepository.FetchEventByID(eventID);
        }


        /* ---------------- */
        // Update Existing Event
        public Event? ServiceUpdateEvent(int eventID, EventDTO dto)
        {
            Event? eventToUpdate = _eventRepository.FetchEventByID(eventID);

            if (eventToUpdate == null)
                return null;

            eventToUpdate.Name = dto.Name;
            eventToUpdate.StartDate = dto.StartDate;
            eventToUpdate.EndDate = dto.EndDate;
            eventToUpdate.MaxRegistrations = dto.MaxRegistrations;
            eventToUpdate.Location = dto.Location;

            _eventRepository.RepositoryUpdateEvent(eventToUpdate);

            return eventToUpdate;
        }


        /* ---------------- */
        // Delete Existing Event
        public void ServiceDeleteEvent(int eventID)
        {
            _eventRepository.RepositoryDeleteEvent(eventID);
        }


        /* ---------------- */
        // Service get Schedule range Events
        public List<Event> ServiceScheduledEvents(DateTime StartDate,DateTime EndDate)
        {
            return _eventRepository.RepositoryScheduledEvents(StartDate, EndDate);
        }


        /* ---------------- */
        // GET Event Weather
        // I didn't understand how to make it


        /* ---------------- */
        // GET Event's location on Google maps
        public GoogleMapsDTO ServiceGetGoogleMapsLink(int eventID)
        {
            var ev = _eventRepository.FetchEventByID(eventID);
            if (ev == null) return null;

            return new GoogleMapsDTO
            {
                Link = $"https://www.google.com/maps/search/?api=1&query={ev.Location}"
            };
        }
    }
}
