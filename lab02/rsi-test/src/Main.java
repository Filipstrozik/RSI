import java.text.DecimalFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        ArrayList<Integer> list = new ArrayList();
        list.add(2);
        list.add(4);
        list.add(3);
        list.add(1);
        System.out.println("Before sorting list:");
        System.out.println(list);
        Arrays.sort(list.toArray(), Collections.reverseOrder());
        System.out.println("after sorting list:");
        System.out.println(list);

        Integer[] myNum = {-10, 50, -30, 40};

        Arrays.sort(myNum, Collections.reverseOrder());
        for (Integer i: myNum)
            System.out.print(i + " ");
    }
}