package pl.kul.predicate;

import lombok.RequiredArgsConstructor;
import pl.kul.InputData;

import java.util.function.Predicate;

@RequiredArgsConstructor
public class PointAboveFunctionPredicate implements Predicate<InputData> {

    private final Function function;

    @Override
    public boolean test(InputData inputData) {
        return inputData.getX2() > function.getValueOfFunctionForArgument(inputData.getX1());
    }
}
