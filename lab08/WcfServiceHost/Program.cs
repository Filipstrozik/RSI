using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using WcfService;

namespace WcfServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyData.Info();
            Uri baseAdress = new Uri("http://10.182.36.179:10000/DatabaseService");

            ServiceHost myHost = new ServiceHost(typeof(DatabaseService), baseAdress);
       

            BasicHttpBinding myBinding = new BasicHttpBinding();

            WSHttpBinding binding2 = new WSHttpBinding();
            binding2.Security.Mode = SecurityMode.None;


            ServiceEndpoint endpoint1 = myHost.AddServiceEndpoint(typeof(IDatabaseService), myBinding, "endpoint1");
            ServiceEndpoint endpoint2 = myHost.AddServiceEndpoint(typeof(IDatabaseService), binding2, "endpoint2");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            myHost.Description.Behaviors.Add(smb);

            try
            {
                Console.WriteLine("---> Endpoints:");
                PrintEndpointDetails(endpoint1);
                PrintEndpointDetails(endpoint2);

                myHost.Open();
                Console.WriteLine("Service is running.");
                Console.WriteLine("Press <ENTER> to stop.");
                Console.WriteLine();
                Console.ReadLine();
                myHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Comunication Exception Occured: {0}", ce.Message);
                myHost.Abort();
                Console.ReadLine();
            }
        }

        private static void PrintEndpointDetails(ServiceEndpoint endpoint)
        {
            Console.WriteLine("Service endpoint: {0}", endpoint.Name);
            Console.WriteLine("Binding: {0}", endpoint.Binding.ToString());
            Console.WriteLine("ListenUri: {0}", endpoint.ListenUri.ToString());
        }
    }
}

