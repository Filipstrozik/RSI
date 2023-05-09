using ClientApp;
using System.Net;
using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        //uri    http://10.182.36.179:50985/MyRestService.svc/persons
        //uri    http://10.182.36.179:50985/MyRestService.svc/json/persons

        //xml data add new      <Person xmlns="http://schemas.datacontract.org/2004/07/MyWebService"><Name>Piotr G</Name><Age>21</Age><Email>piotrg@g.com</Email></Person>
        //json data add new     { "Name": "Filip Strozik", "Age": 21, "Email": "filips@example.com" }

        //json update           { "Id": 1, "Name": "John Doe", "Age": 35, "Email": "johndoe@example.com" }
        //xml update            <Person xmlns="http://schemas.datacontract.org/2004/07/MyWebService"> <Id>2</Id> <Name>John Doe</Name> <Age>35</Age> <Email>johndoe@example.com</Email> </Person>

        //json delete          { "Id": 1 }
        //xml delete            <Person xmlns="http://schemas.datacontract.org/2004/07/MyWebService"><Id>2</Id></Person>

        MyData.Info();
        do
        {
            try
            {
                Console.WriteLine("Enter the method (GET, POST, PUT, or DELETE):");
                string method = Console.ReadLine();

                Console.WriteLine("Enter URI:");
                string uri = Console.ReadLine();
                HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = method.ToUpper();

                string dataFormat = "XML";
                if(method != "GET" && method != "DELETE")
                {
                    Console.WriteLine("Enter data format (JSON or XML):");
                    dataFormat = Console.ReadLine().ToUpper();

                    if (dataFormat == "JSON")
                        req.ContentType = "application/json";
                    else if (dataFormat == "XML")
                        req.ContentType = "text/xml";
                    else
                        throw new ArgumentException("Invalid data format.");
                }

                switch (method.ToUpper())
                {
                    case "GET":
                        break;

                    case "POST":
                        Console.WriteLine("Enter data :");
                        string postData = Console.ReadLine();
                        byte[] postBuffer = Encoding.UTF8.GetBytes(postData);
                        req.ContentLength = postBuffer.Length;
                        Stream postStream = req.GetRequestStream();;
                        postStream.Write(postBuffer, 0, postBuffer.Length);
                        postStream.Close();
                        break;

                    case "PUT":
                        Console.WriteLine("Enter data :");
                        string putData = Console.ReadLine();
                        byte[] putBuffer = Encoding.UTF8.GetBytes(putData);
                        req.ContentLength = putBuffer.Length;
                        Stream putStream = req.GetRequestStream();
                        putStream.Write(putBuffer, 0, putBuffer.Length);
                        putStream.Close();
                        break;

                    default:
                        break;
                }
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                // response message recoding: 
                Encoding enc = Encoding.UTF8;
                StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
                string responseString = responseStream.ReadToEnd();
                responseStream.Close();
                resp.Close();
                Console.WriteLine(responseString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            Console.WriteLine("\n Do you want to continue?");
        } while (Console.ReadLine().ToUpper() == "Y");
    }
}
