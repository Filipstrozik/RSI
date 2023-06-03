public class Message {
    private String time;
    private String name;
    private int counter;

    public Message(String var1, String var2, int var3) {
        this.time = var1;
        this.name = var2;
        this.counter = var3;
    }

    public String getTime() {
        return this.time;
    }

    public String getName() {
        return this.name;
    }

    public int getCounter() {
        return this.counter;
    }
}
