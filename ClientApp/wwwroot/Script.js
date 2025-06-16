/* ----------------- */
// POST - Create Event
document.getElementById("createEventForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const url = `http://localhost:5162/Events`;

    const body = {
        name: document.getElementById("name").value,
        startDate: document.getElementById("startDate").value,
        endDate: document.getElementById("endDate").value,
        maxRegistrations: parseInt(document.getElementById("maxRegistrations").value),
        location: document.getElementById("location").value
    };

    const jsonString = JSON.stringify(body);
    const options = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: jsonString
    };
    
    fetch(url, options)
        .then(res => {
            if (!res.ok) {
                throw new Error("Error creating event");
            }
            return res.json();
        })
        .then(data => {
            document.getElementById("result").innerText = "Event created with ID: " + data;
        })
        .catch(err => {
            document.getElementById("result").innerText = "Error! " + err.message;
        });
});

/* ----------------- */
// GET - Event's registrations
document.getElementById("getRegistrationsForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const eventId = document.getElementById("regEventId").value;
    const url = `http://localhost:5162/Events/${eventId}/registration`;

    fetch(url)
        .then(res => {
            if (!res.ok) {
                throw new Error("Failed to get registrations");
            }
            return res.text();
        })
        .then(text => {
            if (!text) throw new Error("Empty response");
            const data = JSON.parse(text);

            const list = document.getElementById("registrationList");
            list.innerHTML = "";

            if (data.length === 0) {
                list.innerHTML = "<li>No registrations found.</li>";
                return;
            }

            data.forEach(name => {
                const li = document.createElement("li");
                li.innerText = name;
                list.appendChild(li);
            });
        })
        .catch(err => {
            document.getElementById("registrationList").innerHTML = `<li>Error! ${err.message}</li>`;
        });
});

/* ----------------- */
// POST - Register User to Event
document.getElementById("registerUserForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const eventId = document.getElementById("regEventIdInput").value;
    const url = `http://localhost:5162/Events/${eventId}/registration`;

    const body = {
        id: parseInt(document.getElementById("userIdInput").value),
        name: document.getElementById("userNameInput").value,
        dateOfBirth: document.getElementById("userDobInput").value
    };
    const jsonString = JSON.stringify(body);

    const options = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: jsonString
    };

    fetch(url , options)
        .then(res => {
            if (!res.ok) throw new Error("Registration failed");
            return res.text();
        })
        .then(data => {
            document.getElementById("registrationResult").innerText = "Registration successful!";
        })
        .catch(err => {
            document.getElementById("registrationResult").innerText = "Error!" + err.message;
        });
});

/* ----------------- */
// GET - event by ID
document.getElementById("getEventForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const eventId = document.getElementById("getEventId").value;
    const url = `http://localhost:5162/Events/${eventId}`;

    fetch(url)
        .then(res => {
            if (!res.ok) {
                throw new Error("Event not found");
            }
            return res.json();
        })
        .then(data => {
            document.getElementById("eventDetails").innerText =
                `Name: ${data.name}\nStart: ${data.startDate}\nEnd: ${data.endDate}\nMax: ${data.maxRegistrations}\nLocation: ${data.location}`;
        })
        .catch(err => {
            document.getElementById("eventDetails").innerText = "Error! " + err.message;
        });
});

/* ----------------- */
// PUT - Update existing event
document.getElementById("updateEventForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const eventId = document.getElementById("updateEventId").value;
    const url = `http://localhost:5162/Events/${eventId}`;

    const body = {
        name: document.getElementById("updateName").value,
        startDate: document.getElementById("updateStart").value,
        endDate: document.getElementById("updateEnd").value,
        maxRegistrations: parseInt(document.getElementById("updateMax").value),
        location: document.getElementById("updateLocation").value
    };
    const jsonString = JSON.stringify(body);

    const options = {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: jsonString
    };

    fetch(url, options)
        .then(res => {
            if (!res.ok) {
                throw new Error("Event not found");
            }
            return res.text();
        })
        .then(data => {
            document.getElementById("updateResult").innerText = `#${eventId} updated succesfully!`;
        })
        .catch(err => {
            document.getElementById("updateResult").innerText = "Error! " + err.message;
        });
});

/* ----------------- */
// DELETE - Delete existing event
document.getElementById("deleteEventForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const eventId = document.getElementById("deleteEventId").value;
    const url = `http://localhost:5162/Events/${eventId}`;

    fetch(url, {
        method: "DELETE"
    })
    fetch(url)
        .then(res => {
            if (!res.ok) {
                throw new Error("Event not found");
            }
            return res.json();
        })
        .then(data => {
            document.getElementById("deleteResult").innerText = `#${eventId} deleted succesfully!`;
        })
        .catch(err => {
            document.getElementById("deleteResult").innerText = "Error! " + err.message;
        });
});

/* ----------------- */
// GET - Scheduled events
document.getElementById("scheduledEventsForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const startDate = document.getElementById("scheduleStartDate").value;
    const endDate = document.getElementById("scheduleEndDate").value;

    const url = `http://localhost:5162/schedule?startDate=${startDate}&endDate=${endDate}`;

    fetch(url)
        .then(res => {
            if (!res.ok) {
                throw new Error("Failed to fetch events");
            }
            return res.json();
        })
        .then(events => {
            const list = document.getElementById("scheduledEventsList");
            list.innerHTML = "";

            if (events.length === 0) {
                list.innerHTML = "<li>No events found in this range.</li>";
                return;
            }

            events.forEach(ev => {
                const item = document.createElement("li");
                item.textContent = `${ev.name} (${ev.startDate} to ${ev.endDate}) at ${ev.location}`;
                list.appendChild(item);
            });
        })
        .catch(err => {
            document.getElementById("scheduledEventsList").innerHTML = "<li>Error: " + err.message + "</li>";
        });
});

/* ----------------- */
// GET - Link to Google Maps
document.getElementById("mapLinkForm").addEventListener("submit", function (e) {
    e.preventDefault();

    const eventId = document.getElementById("mapEventId").value;
    const url = `http://localhost:5162/Events/${eventId}/map`;

    fetch(url)
        .then(res => {
            if (!res.ok) throw new Error("Event not found");
            return res.json();
        })
        .then(data => {
            const mapUrl = data.link;
            document.getElementById("mapResult").innerHTML =
                `<a href="${mapUrl}" target="_blank">Click here to view in Google Maps</a>`;

            window.open(mapUrl, "_blank");
        })
        .catch(err => {
            document.getElementById("mapResult").innerText = "Error! " + err.message;
        });
});


