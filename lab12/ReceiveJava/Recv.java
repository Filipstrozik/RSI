import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.DeliverCallback;
import java.nio.charset.StandardCharsets;
import com.google.gson.Gson;


// javac -cp amqp-client-5.16.0.jar Recv.java
// java -cp .;amqp-client-5.16.0.jar;slf4j-api-1.7.36.jar;slf4j-simple-1.7.36.jar Recv

public class Recv {

    private final static String QUEUE_NAME = "hello";
    private final static int NO_SENDERS = 2;
    private static int endMarkerCount = 0;

    public static void main(String[] argv) throws Exception {
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        // factory.setPort(5672);
        // factory.setUsername("guest");
        // factory.setPassword("guest");
        Connection connection = factory.newConnection();
        Channel channel = connection.createChannel();

        channel.queueDeclare(QUEUE_NAME, false, false, false, null);
        System.out.println(" [*] Waiting for messages. To exit press CTRL+C");

        DeliverCallback deliverCallback = (consumerTag, delivery) -> {
            String message = new String(delivery.getBody(), StandardCharsets.UTF_8);
            System.out.println(" [x] Received '" + message + "'");

            if (message.equals("EndMarker")) {
                endMarkerCount++;
                if (endMarkerCount >= NO_SENDERS) {
                    System.out.println("Received " + NO_SENDERS + " end markers. Exiting...");
                    channel.basicCancel(consumerTag);
                    try {
                        channel.close();
                        connection.close();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
            else {
                Gson gson = new Gson();
                Message messageDeserialized = gson.fromJson(message, Message.class);

                System.out.println("Name: " + messageDeserialized.getName());
                System.out.println("Time: " + messageDeserialized.getTime());
                System.out.println("Counter: " + messageDeserialized.getCounter());
            }
        };
        channel.basicConsume(QUEUE_NAME, true, deliverCallback, consumerTag -> { });
    }
}

