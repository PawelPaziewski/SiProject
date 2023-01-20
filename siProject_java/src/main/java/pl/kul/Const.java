package pl.kul;

import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeFormatterBuilder;

public class Const {
    public static final int BORDER_VALUE = 0;
    public static final int MAX_NUMBER = 10;
    public static final int MIN_NUMBER = -10;
    public static final int STABILIZER = 1;
    public static final String RESULT_FORMAT = "%8d | %8d || %8d | %8d | %8d || %8d || %8.2f | %8.2f | %8.2f || %8.2f | %8d || %s";
    public static final String RESULT_HEADER_FORMAT = "%8s | %8s || %8s | %8s | %8s || %8s || %8s | %8s | %8s || %8s | %8s || %s";
    public static final String RESULT_HEADER = String.format(RESULT_HEADER_FORMAT, "Epoka", "t", "x0(t)", "x1(t)", "x2(t)", "d(t)", "w0(t)", "w1(t)", "w2(t)", "s(t)", "y(t)", "ok?");

    public static final String TRAINING_SET_FORMAT = "%8d | %8d | %8d || %8d";
    public static final String TRAINING_SET_HEADER_FORMAT = "%8s | %8s | %8s || %8s";
    public static final String TRAINING_SET_HEADER = String.format(TRAINING_SET_HEADER_FORMAT, "x0(t)", "x1(t)", "x2(t)", "d(t)");

    public static final String FILE_DEFAULT_NAME = "WYNIKI.txt";

    public static final String HEADER = "Projekt wykonany samodzielnie" + "\n" + "Skład zespołu: Łukasz Ponachajba, Filip Pawłowicz, Paweł Paziewski-Kopczewski";
    public static final DateTimeFormatter FORMATTER = new DateTimeFormatterBuilder().appendPattern("yyyy-MM-dd_HH mm").toFormatter();
}
