
/* -------------- */
// Create new event function
document.getElementById("createEventForm").addEventListener("submit", function (f) {
    f.preventDefault();

    const EventBody = {
        name: document.getElementById("name").value,
        startDate: document.getElementById("startDate").value,
        endDate: document.getElementById("endDate").value,
        maxRegistrations: parseInt(document.getElementById("MaxRegistrations").value),
        location: document.getElementById("location").value
    };
    const jsonString = JSON.stringify(EventBody);
    const options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: jsonString
    };

    fetch("http://localhost:5162/Events", options)
        .then(res => {
            if (!res.ok) throw new Error("Error creating event");
            return res.json();
        })
        .then(updatedData => {
            document.getElementById('result').innerText = "Event created with ID: " + updatedData;
        })
        .catch(err => {
            document.getElementById('result').innerText = "ERROR! " + err.message;
        });
});
