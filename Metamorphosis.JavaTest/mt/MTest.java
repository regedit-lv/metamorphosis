package mt;

import generated.*;
import java.util.Iterator;
import java.util.List;
import java.util.ArrayList;


public class MTest {

    public static void main(String[] args) {
        System.out.println("Hello, World");

        ATest at = new ATest();
        
        at.ss = new SubStruct();

        at.as = new ArrayList<String>();

        at.as.add("one");
        at.as.add("two");
        at.as.add("thirday");        

        String s = at.toXml(null, null);
        System.out.println(s);

        ATest at2 = new ATest();

        at2.fromXml(s, null);

        s = at2.toXml(null, null);
        System.out.println(s);

   }

}