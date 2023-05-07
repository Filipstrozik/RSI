using System.Net;
using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        do
        {
            try
            {
                Console.WriteLine("Enter the format (xml or json):");
                string format = Console.ReadLine();
                Console.WriteLine("Enter the method (GET or POST ...):");
                string method = Console.ReadLine();
                Console.WriteLine("Enter URI:");
                string uri = Console.ReadLine();
                HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = method.ToUpper();
                if (format == "xml")
                    req.ContentType = "text/xml";
                else if (format == "json") 
                    req.ContentType = "application/json";
                else
                {
                    Console.WriteLine("Entered bad data!");
                    return;
                }
                switch (method.ToUpper())
                {
                    case "GET":
                        break;
                    case "POST":
                        Console.WriteLine("Paste XML or JSON content  (in one line!)");

                        string dane = Console.ReadLine();
                        // request message recoding:
                        byte[] bufor = Encoding.UTF8.GetBytes(dane);
                        req.ContentLength = bufor.Length;
                        Stream postData = req.GetRequestStream();
                        postData.Write(bufor, 0, bufor.Length);
                        postData.Close();
                        break;
                    //here possible other cases 
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
            Console.WriteLine();
            Console.WriteLine("Do you want to continue?");
        } while (Console.ReadLine().ToUpper() == "Y");
    }
}