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
        public void ServiceUpdateEvent(int eventID, EventDTO dto)
        {
            Event EventToUpdate = _eventRepository.FetchEventByID(eventID); // will storage neccesary event
            EventToUpdate.Name = dto.Name;
            EventToUpdate.StartDate = dto.StartDate;
            EventToUpdate.EndDate = dto.EndDate;
            EventToUpdate.MaxRegistrations = dto.MaxRegistrations;
            EventToUpdate.Location = dto.Location;
            _eventRepository.RepositoryUpdateEvent(EventToUpdate);
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
    }
}
