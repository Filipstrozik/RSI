using System.Collections.Generic;
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
            new Person() { Id = 3, Name = "Sammy Doe", Age = 13, Email = "Samdoe@wp.pl"},
            new Person() { Id = 4, Name = "Sally Doe", Age = 11, Email = "Saldoe@onet.pl"},
        };

        private int getNextId()
        {
            if (_persons.Count == 0)
            {
                return 1;
            }
            else
            {
                return _persons.Max(p => p.Id) + 1;
            }
        }

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


            int newId = getNextId();

            int idx = _persons.FindIndex(b => b.Id == newId);
            if (idx == -1)
            {
                person.Id = newId;
                _persons.Add(person);
                WebOperationContext context = WebOperationContext.Current;
                context.OutgoingResponse.StatusCode = HttpStatusCode.Created;
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
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = HttpStatusCode.Accepted;
            return "Removed item with ID=" + Id;
        }

        public List<Person> getAllJson()
        {
            Debug.Print("List<Person> getAllJson()");
            return _persons;
        }

        public List<Person> filterPersonsJson(Person person)
        {
            Debug.Print("List<Person> filterPersonsJson(Person person)");
            HashSet<Person> filtered = new HashSet<Person>();
            // if any of stringifed properties in _persons includes a stringified property in person add to filtered
            foreach (Person p in _persons)
            {
                // if person name inclued in p name add it to filtered
                // create a boolean flag to check 
                if (p.Name.Contains(person.Name) && !person.Name.Equals(""))
                {
                    filtered.Add(p);
                }
                if (p.Age == person.Age)
                {
                    filtered.Add(p);
                }
                if (p.Email.Contains(person.Email) && !person.Email.Equals(""))
                {
                    filtered.Add(p);
                }
            }
            return filtered.ToList();
        }

        public List<Person> filterPersonsXml(Person person)
        {
            Debug.Print("List<Person> filterPersonsXml(Person person)");
            HashSet<Person> filtered = new HashSet<Person>();
            // if any of stringifed properties in _persons includes a stringified property in person add to filtered
            foreach (Person p in _persons)
            {
                // if person name inclued in p name add it to filtered
                // create a boolean flag to check 
                if (p.Name.Contains(person.Name) && !person.Name.Equals(""))
                {
                    filtered.Add(p);
                }
                if (p.Age == person.Age)
                {
                    filtered.Add(p);
                }
                if (p.Email.Contains(person.Email) && !person.Email.Equals(""))
                {
                    filtered.Add(p);
                }
            }
            return filtered.ToList();
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

            int newId = getNextId();
            int idx = _persons.FindIndex(b => b.Id == newId);
            if (idx == -1)
            {
                person.Id = newId;
                _persons.Add(person);
                WebOperationContext context = WebOperationContext.Current;
                context.OutgoingResponse.StatusCode = HttpStatusCode.Created;
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
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = HttpStatusCode.Accepted;
            return "Removed item with ID=" + Id;
        }

        public string updateXml(string Id, Person item)
        {
            Debug.Print("string updateXml(string Id, Person item)");
            int id = int.Parse(Id);
            var personToUpdate = _persons.FirstOrDefault(p => p.Id == id);


            // if the person is not found throw an exception
            if (personToUpdate == null)
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);

            personToUpdate.Name = item.Name;
            personToUpdate.Age = item.Age;
            personToUpdate.Email = item.Email;

            return "Person updated successfully.";
        }

        public string updateJson(string Id, Person item)
        {
            Debug.Print("string updateJson(string Id, Person item)");
            int id = int.Parse(Id);
            // find the index of the person with the id
            var personToUpdate = _persons.FirstOrDefault(p => p.Id == id);


            // if the person is not found throw an exception
            if (personToUpdate == null)
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);

            personToUpdate.Name = item.Name;
            personToUpdate.Age = item.Age;
            personToUpdate.Email = item.Email;

            return "Person updated successfully.";

        }

        public int getSize()
        {
            Debug.Print("int getSize()");
            return _persons.Count;
        }
    }
}
