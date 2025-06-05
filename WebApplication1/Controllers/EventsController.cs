using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTO;
using ClassLibrary1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;
        public EventsController(EventService eventService) // Controller constructor
        {
            this._eventService = eventService;
        }

        /* ---------------- */
        // POST - Create new Event
        [HttpPost]
        [Route("")]
        public ActionResult<int> ControllerCreateEvent([FromBody] EventDTO dto)
        {
            try
            {
                Event newEvent = new Event // use Object of Event type
                {
                    Name = dto.Name,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    MaxRegistrations = dto.MaxRegistrations,
                    Location = dto.Location
                };

                return Ok(_eventService.ServiceCreateEvent(newEvent)); // keep to the Service
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* ---------------- */
        // GET - event's registered users
        [HttpGet("{id}/registration")]
        public ActionResult GetUserNamesByEvent(int id)
        {
            try
            {
                List<string> names = _eventService.retrieveUserNamesByEvent(id);
                return Ok(names); // List of names
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* ---------------- */
        // POST - register user to event
        [HttpPost("{id}/registration")]
        public ActionResult ControllerRegUserToEvent(int id, [FromBody] UserDTO dto)
        {
            try
            {
                int UserID = dto.Id;
                _eventService.ServiceRegUserToEvent(id, UserID);
                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /* ---------------- */
        // GET Event's Information By ID
        [HttpGet("{id}")]
        public ActionResult<EventDTO> GetEventbyID(int id)
        {
            try
            {
                return Ok(_eventService.retrieveEventbyID(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /* ---------------- */
        // PUT Update an existing event
        [HttpPut("{id}")]
        public ActionResult ControllerUpdateEvent(int id, [FromBody] EventDTO dto)
        {
            try
            {
                _eventService.ServiceUpdateEvent(id, dto);
                return Ok($"#{id} Event Updated!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /* ---------------- */
        // Delete Existing Event
        [HttpDelete("{id}")]
        public ActionResult ControllerDeleteEvent(int id)
        {
            try
            {
                _eventService.ServiceDeleteEvent(id);
                return Ok($"#{id} Event deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /* ---------------- */
        // GET Schedule range Events
        [HttpGet("/schedule")]
        public ActionResult<List<Event>> ControllerScheduledEvents([FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
        {
            try
            {
                return Ok(_eventService.ServiceScheduledEvents(StartDate, EndDate));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /* ---------------- */
        // GET Event Weather
        // [HttpGet("{id}/weather")]
        // I didn't understand how to make it
    } 
}
