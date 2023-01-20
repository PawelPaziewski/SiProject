package pl.kul;

import lombok.AccessLevel;
import lombok.Builder;
import lombok.RequiredArgsConstructor;

import java.util.ArrayList;
import java.util.List;

@Builder(setterPrefix = "with")
@RequiredArgsConstructor(access = AccessLevel.PRIVATE)
class Perceptron {

    private final double ro;
    private final boolean bipolar;
    private final YCounter yCounter = new YCounter();

    public Result countStep(InputData input, WeightsVector weightsVector) {
        double signal = weightsVector.countSignal(input);
        int y = yCounter.countY(signal, bipolar);
        boolean test = input.getD() == y;

        return Result.builder()
                .withSignal(signal)
                .withY(y)
                .withCorrect(test)
                .build();
    }

    public List<OneStepData> findVectorOfWeights(List<InputData> trainingSet, WeightsVector initialVector) {
        List<OneStepData> oneStepData = new ArrayList<>();
        int trainingSetSize = trainingSet.size();
        int correctCounter = 0;

        int index = 0;
        WeightsVector actualVector = initialVector;

        while (correctCounter < trainingSetSize) {
            InputData checkedElement = trainingSet.get(index % trainingSetSize);
            Result result = countStep(checkedElement, actualVector);

            oneStepData.add(OneStepData.builder()
                    .withInputData(checkedElement)
                    .withWeightsVector(actualVector)
                    .withResult(result)
                    .build());


            if (result.isCorrect()) {
                correctCounter++;
            } else {
                correctCounter = 0;
                actualVector = actualVector.countNewVector(checkedElement, result, ro);
            }
            index++;
        }

        return oneStepData;
    }
}
