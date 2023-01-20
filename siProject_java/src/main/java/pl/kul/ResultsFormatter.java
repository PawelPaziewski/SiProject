package pl.kul;

import java.util.List;

class ResultsFormatter {

    protected static String format(int stepNumber, int setSize, InputData input, WeightsVector weightsVector, Result result) {
        return String.format(Const.RESULT_FORMAT, stepNumber, countEraNumber(stepNumber, setSize),
                input.getX0(), input.getX1(), input.getX2(), input.getD(),
                weightsVector.getW0(), weightsVector.getW1(), weightsVector.getW2(),
                result.getSignal(), result.getY(), formatBooleanToYesOrNo(result.isCorrect()));
    }

    public static String format(List<OneStepData> data, int setSize) {
        StringBuilder stringBuilder = new StringBuilder(getTableHeader())
                .append("\n");
        for (int i = 0; i < data.size(); i++) {
            OneStepData singleLine = data.get(i);
            stringBuilder.append(format(i, setSize, singleLine.getInputData(), singleLine.getWeightsVector(), singleLine.getResult()))
                    .append("\n");
        }
        return stringBuilder.toString();
    }

    protected static String getTableHeader() {
        return Const.RESULT_HEADER;
    }

    protected static int countEraNumber(int stepNumber, int setSize) {
        return (stepNumber / setSize) + 1;
    }

    protected static String formatBooleanToYesOrNo(boolean logicalValue) {
        return logicalValue ? "YES" : "NO";
    }
}
