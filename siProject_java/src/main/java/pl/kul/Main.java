package pl.kul;

import pl.kul.dataGenerator.TrainingSetGenerator;
import pl.kul.dataPrinter.ToConsolePrinter;
import pl.kul.dataPrinter.ToFilePrinter;
import pl.kul.predicate.LinearFunction;
import pl.kul.predicate.PointAboveFunctionPredicate;

import java.util.List;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        FromUserData fromUserData = new FromUserDataScanner(new Scanner(System.in)).readData();

        WeightsVector vector = prepareInitialWeightsVector(fromUserData);
        List<InputData> trainingSet = prepareTrainingSet(fromUserData);

        Perceptron perceptron = Perceptron.builder().withRo(fromUserData.getRo())
                .withBipolar(fromUserData.isBipolar()).build();

        List<OneStepData> oneStepData = perceptron.findVectorOfWeights(trainingSet, vector);

        printResult(trainingSet, oneStepData);
    }

    private static WeightsVector prepareInitialWeightsVector(FromUserData fromUserData) {
        if (fromUserData.isUserSpecifiedVector()) {
            return new WeightsVector(fromUserData.getW0(), fromUserData.getW1(), fromUserData.getW2());
        } else {
            double maxW = fromUserData.getMaxW();
            double minW = fromUserData.getMinW();
            double w0 = minW + Math.random() * (maxW - minW);
            double w1 = minW + Math.random() * (maxW - minW);
            double w2 = minW + Math.random() * (maxW - minW);
            return new WeightsVector(w0, w1, w2);
        }
    }

    private static List<InputData> prepareTrainingSet(FromUserData fromUserData) {
        return new TrainingSetGenerator(fromUserData.isBipolar())
                .generate(fromUserData.getTrainingSetSize(),
                        new PointAboveFunctionPredicate(new LinearFunction(fromUserData.getA(), fromUserData.getB())));
    }

    private static void printResult(List<InputData> trainingSet, List<OneStepData> oneStepData) {
        String textToPrint = new StringBuilder(Const.HEADER)
                .append("\n\n")
                .append(TrainingSetFormatter.format(trainingSet))
                .append("\n")
                .append(ResultsFormatter.format(oneStepData, trainingSet.size()))
                .append("\n")
                .toString();
        new ToConsolePrinter().print(textToPrint);
        new ToFilePrinter().print(textToPrint);
    }
}