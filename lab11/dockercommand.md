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

mamy dwaoch senderów
jeden reciever
sender wysyła 5 wiadomosci
Imie plus id wiadomosci
czasy wysyłania opóźnanie losuje 1.5 do 2.5 s drugi podobny co wyslanie

kroki projektowe

kroki konfiguracyjne

screenzy z uruchomienia z wysyłanien

krótki opis protokołu rabbit MQ

6.1

6a-Strozik-Filip.zip

//
// q: opisz protokół RabbitMQ
// a: RabbitMQ jest to oprogramowanie open source, które implementuje protokół AMQP (Advanced Message Queuing Protocol).
// RabbitMQ jest serwerem kolejek wiadomości, który pozwala na komunikację między aplikacjami.
// RabbitMQ jest napisany w Erlangu, ale posiada interfejsy do wielu języków programowania.
// RabbitMQ jest wykorzystywany do przesyłania wiadomości między aplikacjami, które mogą być napisane w różnych językach programowania.
// q: opisz samo działanie AMQP
// a: AMQP jest to protokół komunikacyjny, który pozwala na przesyłanie wiadomości między aplikacjami.
// AMQP jest protokołem typu publish-subscribe, co oznacza, że wiadomości są wysyłane do kolejki, a następnie do subskrybentów.

