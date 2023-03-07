import java.net.InetAddress;
import java.net.UnknownHostException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class MyData {

    public static void main(String[] args) {

    }
    public static void info(){

        System.out.println("Filip Strózik, 260377");
        System.out.println("Piotr Grygoruk, 260299");

        SimpleDateFormat tf = new SimpleDateFormat("dd MMM, HH:mm:ss");
        System.out.println(tf.format(new Date()));

        System.out.println(System.getProperty("java.version"));
        System.out.println(System.getProperty("user.name"));
        System.out.println(System.getProperty("os.name"));
        try {
            System.out.println(InetAddress.getLocalHost().getHostAddress());
        } catch (UnknownHostException e) {
            throw new RuntimeException(e);
        }

    }
}
