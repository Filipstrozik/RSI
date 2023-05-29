1. change hostname to localhost or machine ip.
2. run docker command (login: guest, password: guest)
   docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
3. login to rabbitmq management console
   http://localhost:15672/ or http://machine_ip:15672/
4. open two terminals at RSI/lab11
5. run:
   cd Send/
   dotnet run
6. run:
   cd Receive/
   dotnet run
7. send.cs should send Hello World! and receive.cs should receive it.
