import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class Message {
    public LocalDateTime time;
    public String name;
    public int counter;

    public Message(String time, String name, int counter) {
        DateTimeFormatter formatter = DateTimeFormatter.ISO_OFFSET_DATE_TIME;
        this.time = LocalDateTime.parse(time, formatter);
        this.name = name;
        this.counter = counter;
    }

    public LocalDateTime getTime() {
        return this.time;
    }

    public String getName() {
        return this.name;
    }

    public int getCounter() {
        return this.counter;
    }
}


