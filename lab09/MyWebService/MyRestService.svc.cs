﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyWebService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MyRestService : IRestService
    {
        private static List<Person> _persons = new List<Person>()
        {
            new Person() { Id = 1, Name = "John Doe", Age = 44, Email = "Joedoe@gmail.com"},
            new Person() { Id = 2, Name = "Jane Doe", Age = 39, Email = "Janedoe@hotmail.com"},
            new Person() { Id = 3, Name = "Sammy Doe", Age = 13, Email = "Samdoe@wp.pl"}
        };

        public List<Person> getAllXml()
        {
            Debug.Print("List<Person> getAllXml()");
            return _persons;
        }
        public Person getByIdXml(string Id)
        {
            Debug.Print("Person getByIdXml(string Id)");
            int id = int.Parse(Id);
            int idx = _persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }
            return _persons.ElementAt(idx);
        }
        public string addXml(Person person)
        {
            Debug.Print("string addXml(Person person)");


            int newId = _persons.Count + 1;

            int idx = _persons.FindIndex(b => b.Id == newId);
            if (idx == -1)
            {
                person.Id = newId;
                _persons.Add(person);
                return "Added item with ID=" + person.Id;
            }
            else
                throw new WebFaultException<string>("409: Conflict",
                HttpStatusCode.Conflict);

        }
        public string deleteXml(string Id)
        {
            Debug.Print("string deleteXml(string Id)");
            int id = int.Parse(Id);
            int idx = _persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }
            _persons.RemoveAt(idx);
            return "Removed item with ID=" + Id;
        }

        public List<Person> getAllJson()
        {
            Debug.Print("List<Person> getAllJson()");
            return _persons;
        }

        public Person getByIdJson(string Id)
        {
            Debug.Print("Person getByIdJson(string Id)");
            int id = int.Parse(Id);
            int idx = _persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }
            return _persons.ElementAt(idx);
        }

        public string addJson(Person person)
        {
            Debug.Print("string addJson(Person person)");
            if (person == null)
                throw new WebFaultException<string>("400:BadRequest",
                HttpStatusCode.BadRequest);
            // generate new Id based on the last Id in the list

            int newId = _persons.Count + 1;
            int idx = _persons.FindIndex(b => b.Id == newId);
            if (idx == -1)
            {
                person.Id = newId;
                _persons.Add(person);
                return "Added item with ID=" + person.Id;
            }
            else
                throw new WebFaultException<string>("409: Conflict",
                HttpStatusCode.Conflict);
        }

        public string deleteJson(string Id)
        {
            Debug.Print("string deleteJson(string Id)");
            int id = int.Parse(Id);
            int idx = _persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }
            _persons.RemoveAt(idx);
            return "Removed item with ID=" + Id;
        }

        public string updateXml(string Id, Person item)
        {
            Debug.Print("string updateXml(string Id, Person item)");

            int id = int.Parse(Id);
            int idx = _persons.FindIndex(p => p.Id == id);

            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }
            else
            {
                _persons[idx] = item;

            }

            return "Person updated successfully.";
        }

        public string updateJson(string Id, Person item)
        {
            Debug.Print("string updateJson(string Id, Person item)");
            int id = int.Parse(Id);
            int idx = _persons.FindIndex(p => p.Id == id);

            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }
            else
            {
                _persons[idx] = item;
            }
            return "Person updated successfully.";

        }
        public int getSize()
        {
            Debug.Print("int getSize()");
            return _persons.Count;
        }

        public string getAuthorsJson()
        {
            Debug.Print("string getAuthorsJson()");
            return "Authors: Filip Strózik, Piotry Grygoruk";
        }

        public string getAuthorsXml()
        {
            Debug.Print("string getAuthorsXml())");
            return "Authors: Filip Strózik, Piotry Grygoruk";
        }
    }
}
