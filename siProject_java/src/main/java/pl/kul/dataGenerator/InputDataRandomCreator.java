package pl.kul.dataGenerator;

import pl.kul.Const;
import pl.kul.InputData;

import java.util.Random;

class InputDataRandomCreator {

    private final Random random = new Random(System.currentTimeMillis());

    InputData createData() {
        int x1 = pickANumber();
        int x2 = pickANumber();
        return new InputData(x1, x2, 0);
    }

    private int pickANumber() {
        return random.nextInt(Const.MAX_NUMBER - Const.MIN_NUMBER + 1) + Const.MIN_NUMBER;
    }
}
