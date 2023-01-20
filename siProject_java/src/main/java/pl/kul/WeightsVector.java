package pl.kul;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

@Getter
@RequiredArgsConstructor
class WeightsVector {
    private final double w0;
    private final double w1;
    private final double w2;

    double countSignal(InputData input) {
        return w0 * input.getX0() + w1 * input.getX1() + w2 * input.getX2();
    }

    WeightsVector countNewVector(InputData actualInputData, Result result, double ro) {
        double factor = ro * (actualInputData.getD() - result.getY());

        double newW0 = w0 + factor * actualInputData.getX0();
        double newW1 = w1 + factor * actualInputData.getX1();
        double newW2 = w2 + factor * actualInputData.getX2();

        return new WeightsVector(newW0, newW1, newW2);
    }
}
