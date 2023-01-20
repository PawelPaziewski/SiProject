package pl.kul.dataPrinter;

public class ToConsolePrinter implements Printer {

    @Override
    public void print(String text) {
        System.out.println(text);
    }
}
