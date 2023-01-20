package pl.kul;

import java.util.List;

class TrainingSetFormatter {

    public static String format(List<InputData> data) {
        StringBuilder stringBuilder = new StringBuilder(getHeader()).append("\n");

        for (InputData d : data) {
            stringBuilder.append(String.format(Const.TRAINING_SET_FORMAT, d.getX0(), d.getX1(), d.getX2(), d.getD()))
                    .append("\n");
        }

        return stringBuilder.toString();
    }

    protected static String getHeader() {
        return Const.TRAINING_SET_HEADER;
    }
}
