$(document).ready(function () {
    // Function to get all persons using AJAX
    function getPersons() {
        $.ajax({
            url: "http://localhost:50985/MyRestService.svc/json/persons", // Replace with your actual API endpoint
            type: "GET",
            success: function (response) {
                // Handle the response from the server
                console.log(response); // You can perform actions with the response here
                // Clear the table
                $("#personresult").empty();
                // Append the table headers
                $("#personresult").append("<tr><th>Id</th><th>Name</th><th>Email</th><th>Age</th></tr>");

                // Loop through the response and append the data to the table
                for (var i = 0; i < response.length; i++) {
                    var person = response[i];
                    // add id to the table
                    $("#personresult").append("<tr><td>" + person.Id + "</td><td>" + person.Name + "</td><td>" + person.Email + "</td><td>" + person.Age + "</td></tr>");
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
        $.ajax({
            url: "http://localhost:50985/MyRestService.svc/json/persons", // Replace with your actual API endpoint
            type: "POST",
            data: JSON.stringify(person),
            contentType: "application/json",
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

    // Fuction to update a person using AJAX
    function updatePerson(id,person) {
        $.ajax({
            url: "http://localhost:50985/MyRestService.svc/json/persons/" + id, // Replace with your actual API endpoint
            type: "PUT",
            data: JSON.stringify(person),
            contentType: "application/json",
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
        $.ajax({
            url: "http://localhost:50985/MyRestService.svc/json/persons/" + id, // Replace with your actual API endpoint
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
        // Implement your logic here to get a person
        $.ajax({
            url: "http://localhost:50985/MyRestService.svc/json/persons/" + id, // Replace with your actual API endpoint
            type: "GET",
            success: function (response) {
                // insert get person valuse to input fields
                $("#id").val(response.Id);
                $("#name").val(response.Name);
                $("#email").val(response.Email);
                $("#age").val(response.Age);
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
        // Implement your logic here to filter persons
        $.ajax({
            url: "http://localhost:50985/MyRestService.svc/json/filter", // Replace with your actual API endpoint
            type: "POST",
            data: JSON.stringify(person),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                console.log(response); // You can perform actions with the response here
                // Clear the table
                $("#personresult").empty();
                // Append the table headers
                $("#personresult").append("<tr><th>Id</th><th>Name</th><th>Email</th><th>Age</th></tr>");

                // Loop through the response and append the data to the table
                for (var i = 0; i < response.length; i++) {
                    var person = response[i];
                    // add id to the table
                    $("#personresult").append("<tr><td>" + person.Id + "</td><td>" + person.Name + "</td><td>" + person.Email + "</td><td>" + person.Age + "</td></tr>");
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

    // Call the getPersons function initially to retrieve all persons
    getPersons();
});