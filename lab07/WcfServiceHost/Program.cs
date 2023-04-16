using CallbackService;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfService;

namespace WcfServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Uri baseAdress = new Uri("http://localhost:10000/MyCalculator");

            ServiceHost myHost = new ServiceHost(typeof(MyCalculator), baseAdress);
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            myHost.Description.Behaviors.Add(smb);

            BasicHttpBinding myBinding = new BasicHttpBinding();
            WSHttpBinding binding2 = new WSHttpBinding();
            binding2.Security.Mode = SecurityMode.None;

            ServiceEndpoint endpoint1 = myHost.AddServiceEndpoint(typeof(ICalculator), myBinding, "endpoint1");
            ServiceEndpoint endpoint2 = myHost.AddServiceEndpoint(typeof(ICalculator), binding2, "endpoint2");

            Uri baseAddress3 = new Uri("http://localhost:10000/MySuperCalculator"); 
            ServiceHost myHost3 = new ServiceHost(typeof(MySuperCalc), baseAddress3);
            WSDualHttpBinding myBinding3 = new WSDualHttpBinding();
            ServiceEndpoint endpoint3 = myHost3.AddServiceEndpoint(typeof(ISuperCalc), myBinding3, "endpoint3");
            myHost3.Description.Behaviors.Add(smb);

            try
            {
                Console.WriteLine("---> Endpointy:");
                PrintEndpointDetails(endpoint1);
                PrintEndpointDetails(endpoint2);
                PrintEndpointDetails(endpoint3);

                myHost.Open();
                myHost3.Open();
                Console.WriteLine("Serwis jest uruchomiony.");
                Console.WriteLine("Nacisnij <ENTER> aby zakonczyc.");
                Console.WriteLine();
                Console.ReadLine();
                myHost.Close();
                myHost3.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Wystapil wyjatek: {0}", ce.Message);
                myHost.Abort();
                myHost3.Abort();
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

