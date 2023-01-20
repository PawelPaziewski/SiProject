package pl.kul;

import lombok.RequiredArgsConstructor;

import java.util.InputMismatchException;
import java.util.Scanner;


@RequiredArgsConstructor
class FromUserDataScanner {

    private final Scanner scanner;

    public FromUserData readData() {
        try {
            FromUserData.FromUserDataBuilder builder = FromUserData.builder();
            builder.withRo(readRo());
            builder.withTrainingSetSize(readSetSize());
            builder.withBipolar(readBipolarParameter());
            builder.withA(readAParameterOfFunction());
            builder.withB(readBParameterOfFunction());
            boolean userSpecifiedVector = readWillUserSpecifyVector();
            builder.withUserSpecifiedVector(userSpecifiedVector);

            if (userSpecifiedVector) {
                builder.withW0(readW0());
                builder.withW1(readW1());
                builder.withW2(readW2());
            } else {
                double minW = readMinW();
                builder.withMinW(minW);
                builder.withMaxW(readMaxW(minW));
            }

            return builder.build();
        } catch (InputMismatchException exception) {
            System.err.println("\nWprowadzono niepoprawne dane. Spróbuj ponownie");
            scanner.nextLine();
            return readData();
        }
    }

    private double readRo() {
        System.out.println("Podaj wartość współczynnika ro (Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private double readMinW() {
        System.out.println("Podaj początek przedziału z którego mają być wylosowane inicjalne wektora wag (oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private double readMaxW(double min) {
        System.out.println("Podaj koniec przedziału z którego mają być wylosowane inicjalne wektora wag (Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        float result = scanner.nextFloat();
        if (result <= min) {
            return readMaxW(min);
        }
        return result;
    }

    private double readW0() {
        System.out.println("Podaj w0 (Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private double readW1() {
        System.out.println("Podaj w1 (Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private double readW2() {
        System.out.println("Podaj w2 (Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private boolean readWillUserSpecifyVector() {
        System.out.println("Czy chcesz podać współczynniki wektora wag? (Wpisz 'true' lub 'false)");
        return scanner.nextBoolean();
    }

    private double readAParameterOfFunction() {
        System.out.println("Podaj parametr a dla funkcji liniowej(Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private double readBParameterOfFunction() {
        System.out.println("Podaj parametr b dla funkcji liniowej(Oddziel część całkowitą od dziesiętnej liczby przy pomocy przecinka)");
        return scanner.nextFloat();
    }

    private boolean readBipolarParameter() {
        System.out.println("Czy funkcja ma działać w trybie bipolarnym? (Wpisz 'true' lub 'false)");
        return scanner.nextBoolean();
    }

    private int readSetSize() {
        System.out.println("Podaj rozmiar zbioru uczącego (Użyj liczby całkowitej)");
        int setSize = scanner.nextInt();
        if (setSize < 1) {
            System.out.println("Rozmiar zbioru uczącego musi być większy od 0!");
            return readSetSize();
        }
        return setSize;
    }

}
