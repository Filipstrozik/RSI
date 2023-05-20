var isXML = true;

$(document).ready(function () {
    // Function to get all persons using AJAX
    function getPersons() {
        var endpoint = isXML ? "http://localhost:50985/MyRestService.svc/persons" : "http://localhost:50985/MyRestService.svc/json/persons";
        console.log(endpoint)
        $.ajax({
            url: endpoint,
            type: "GET",
            dataType: isXML ? "xml" : "json",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here
                // Clear the table
                $("#personresult").empty();
                // Append the table headers
                $("#personresult").append("<tr><th>Id</th><th>Name</th><th>Email</th><th>Age</th></tr>");

                if (isXML) {
                    // Parse XML response
                    var persons = $(response).find("Person");
                    persons.each(function () {
                        var person = $(this);
                        var id = person.find("Id").text();
                        var name = person.find("Name").text();
                        var email = person.find("Email").text();
                        var age = person.find("Age").text();

                        $("#personresult").append("<tr><td>" + id + "</td><td>" + name + "</td><td>" + email + "</td><td>" + age + "</td></tr>");
                    });
                } else {
                    // Loop through the JSON response and append the data to the table
                    for (var i = 0; i < response.length; i++) {
                        var person = response[i];
                        // add id to the table
                        $("#personresult").append("<tr><td>" + person.Id + "</td><td>" + person.Name + "</td><td>" + person.Email + "</td><td>" + person.Age + "</td></tr>");
                    }
                }
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.log(error);
            }
        });
    }

    // Add an event listener to the "Get All" button
    $("#getall").click(function () {
        getPersons();
    });


    // Function to add a person using AJAX
    function addPerson(person) {
        var endpoint = isXML ? "http://localhost:50985/MyRestService.svc/persons" : "http://localhost:50985/MyRestService.svc/json/persons";

        $.ajax({
            url: endpoint,
            type: "POST",
            data: isXML ? personToXml(person) : JSON.stringify(person),
            contentType: isXML ? "text/xml" : "application/json",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.log(personToXml(person));
                console.log(error);
            }
        });
    }

    // Fuction to update a person using AJAX
    function updatePerson(id, person) {
        var endpoint = isXML ? "http://localhost:50985/MyRestService.svc/persons/" + id : "http://localhost:50985/MyRestService.svc/json/persons/" + id;

        $.ajax({
            url: endpoint,
            type: "PUT",
            data: isXML ? personToXml(person) : JSON.stringify(person),
            contentType: isXML ? "application/xml" : "application/json",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.log(error);
            }
        });
    }

    // Add an event listener to the "Add" button
    $("#add").click(function () {
        // when adding get the values from the input fields
        var name = $("#name").val();
        var email = $("#email").val();
        var age = $("#age").val();


        //create a new person json object like this : 
        var newPerson = {
            "Name": name,
            "Email": email,
            "Age": age
        };

        //console log the new person object
        console.log(newPerson);

        //call the addPerson function and pass the new person object
        addPerson(newPerson);

        // clear the input fields
        $("#name").val("");
        $("#email").val("");
        $("#age").val("");

        getPersons();
    });

    // Add an event listener to the "Update" button
    $("#update").click(function () {
        // Implement your logic here to update a person\
        var id = $("#id").val();
        var name = $("#name").val();
        var email = $("#email").val();
        var age = $("#age").val();

        var updatedPerson = {
            "Id": id,
            "Name": name,
            "Email": email,
            "Age": age
        };

        updatePerson(id,updatedPerson);

        getPersons();
    });

    function deletePerson(id) {
        var endpoint = isXML ? "http://localhost:50985/MyRestService.svc/persons/" + id : "http://localhost:50985/MyRestService.svc/json/persons/" + id;

        $.ajax({
            url: endpoint,
            type: "DELETE",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.log(error);
            }
        });
    }

    // Add an event listener to the "Delete" button
    $("#delete").click(function () {
        // Implement your logic here to delete a person
        var id = $("#id").val();

        deletePerson(id);
        getPersons();
    });

    function getPerson(id) {
        var endpoint = isXML ? "http://localhost:50985/MyRestService.svc/persons/" + id : "http://localhost:50985/MyRestService.svc/json/persons/" + id;

        $.ajax({
            url: endpoint,
            type: "GET",
            dataType: isXML ? "xml" : "json",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here

                if (isXML) {
                    var person = $(response).find("Person");
                    var id = person.find("Id").text();
                    var name = person.find("Name").text();
                    var email = person.find("Email").text();
                    var age = person.find("Age").text();

                    // insert get person values to input fields
                    $("#id").val(id);
                    $("#name").val(name);
                    $("#email").val(email);
                    $("#age").val(age);
                } else {
                    var person = response;
                    // insert get person values to input fields
                    $("#id").val(person.Id);
                    $("#name").val(person.Name);
                    $("#email").val(person.Email);
                    $("#age").val(person.Age);
                }
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.log(error);
            }
        });
    }
    // Add an event listener to the "Search" button
    $("#search").click(function () {
        // get id from input
        var id = $("#id").val();

        getPerson(id);
    });

    $("#clear").click(function () {
        // clear input fields
        $("#id").val("");
        $("#name").val("");
        $("#email").val("");
        $("#age").val("");
    });


    function filterPersons(person) {
        var endpoint = isXML ? "http://localhost:50985/MyRestService.svc/filter" : "http://localhost:50985/MyRestService.svc/json/filter";

        $.ajax({
            url: endpoint,
            type: "POST",
            data: isXML ? personToXml(person) : JSON.stringify(person),
            contentType: isXML ? "application/xml" : "application/json",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here
                // Clear the table
                $("#personresult").empty();
                // Append the table headers
                $("#personresult").append("<tr><th>Id</th><th>Name</th><th>Email</th><th>Age</th></tr>");

                if (isXML) {
                    var persons = $(response).find("Person");
                    persons.each(function () {
                        var person = $(this);
                        var id = person.find("Id").text();
                        var name = person.find("Name").text();
                        var email = person.find("Email").text();
                        var age = person.find("Age").text();

                        $("#personresult").append("<tr><td>" + id + "</td><td>" + name + "</td><td>" + email + "</td><td>" + age + "</td></tr>");
                    });
                } else {
                    for (var i = 0; i < response.length; i++) {
                        var person = response[i];
                        // add id to the table
                        $("#personresult").append("<tr><td>" + person.Id + "</td><td>" + person.Name + "</td><td>" + person.Email + "</td><td>" + person.Age + "</td></tr>");
                    }
                }
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.log(error);
            }
        });
    }

    // add event listener to the "filter"   button
    $("#filter").click(function () {
        // get values from input fields
        var name = $("#name").val();
        var email = $("#email").val();
        var age = $("#age").val();

        // if age input is empty do not include Age in the json object
        if (age == "") {
            var newPerson = {
                "Name": name,
                "Email": email
            };
        }
        // else include Age in the json object
        else {
            var newPerson = {
                "Name": name,
                "Email": email,
                "Age": age
            };
        }
        
        //console log the new person object
        console.log(newPerson);

        // call filterPersons function and pass the new person object
        filterPersons(newPerson);
    });

    function personToXml(person) {
        var xml = "<Person xmlns=\"http://schemas.datacontract.org/2004/07/MyWebService\">";
        // xml += "<Id>" + person.Id + "</Id>";
        xml += "<Name>" + person.Name + "</Name>";
        xml += "<Email>" + person.Email + "</Email>";
        xml += "<Age>" + person.Age + "</Age>";
        xml += "</Person>";
        return xml;
    }


    $("#jsonbutton").click(function () {
        // when adding get the values from the input fields
        isXML = false;
        console.log(isXML);
        document.getElementById("dataformat").innerHTML = "JSON";
    });
    
    $("#xmlbutton").click(function () {
        // when adding get the values from the input fields
        isXML = true;
        console.log(isXML);
        document.getElementById("dataformat").innerHTML = "XML";
    });

    // Call the getPersons function initially to retrieve all persons
    getPersons();
});

