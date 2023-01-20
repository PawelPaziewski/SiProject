package pl.kul.predicate;

import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
public class LinearFunction implements Function {

    private final double a;
    private final double b;

    @Override
    public double getValueOfFunctionForArgument(int x) {
        return x * a + b;
    }
}
