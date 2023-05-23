using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyWebService
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/persons")]
        List<Person> getAllXml();

        [OperationContract]
        [WebGet(UriTemplate = "/json/persons", ResponseFormat = WebMessageFormat.Json)]
        List<Person> getAllJson();

        [OperationContract]
        [WebGet(UriTemplate = "/persons/{id}",
        ResponseFormat = WebMessageFormat.Xml)]
        Person getByIdXml(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "/json/persons/{id}",
        ResponseFormat = WebMessageFormat.Json)]
        Person getByIdJson(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/persons",
        Method = "POST",
        RequestFormat = WebMessageFormat.Xml)]
        string addXml(Person item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/persons",
        Method = "POST",
        RequestFormat = WebMessageFormat.Json)]
        string addJson(Person item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/persons/{id}", Method = "DELETE")]
        string deleteXml(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/persons/{id}", Method = "DELETE")]
        string deleteJson(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/persons/{id}",
        Method = "PUT",
        RequestFormat = WebMessageFormat.Xml)]
        string updateXml(string Id, Person item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/persons/{id}",
        Method = "PUT",
        RequestFormat = WebMessageFormat.Json)]
        string updateJson(string Id, Person item);

        [OperationContract]
        [WebGet(UriTemplate = "/persons/size")]
        int getSize();

        // greate a operation to get a string at /authors will return in json fromat as a string
        [OperationContract]
        [WebGet(UriTemplate = "/json/authors", ResponseFormat = WebMessageFormat.Json)]
        string getAuthorsJson();

        // greate a operation to get a string at /authors will return in xml fromat as a string
        [OperationContract]
        [WebGet(UriTemplate = "/authors", ResponseFormat = WebMessageFormat.Xml)]
        string getAuthorsXml();

    }

    [DataContract]
    public class Person
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public int Age { get; set; }

        [DataMember(Order = 4)]
        public string Email { get; set; }

    }
}
