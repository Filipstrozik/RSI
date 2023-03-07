import java.util.*;

public class Main {
    public static void main(String[] args) {
        MyData.info();

        Vector<Object> vector = new Vector<>(new ArrayList<Object>(List.of(true, 1, "RSI")));

        System.out.println(vector);
        Scanner scanner = new Scanner(System.in);
        String line = scanner.nextLine();

        Vector<Object> scannerVector = new Vector<>(List.of(line.split(", ")));

        System.out.println(scannerVector);
        System.out.println(vector.equals(scannerVector));
    }
}