package pl.kul.dataGenerator;


import lombok.RequiredArgsConstructor;
import pl.kul.InputData;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Predicate;

@RequiredArgsConstructor
public class TrainingSetGenerator {

    private final InputDataRandomCreator creator = new InputDataRandomCreator();
    private final boolean isBipolar;

    public List<InputData> generate(int setSize, Predicate<InputData> predicate) {
        ArrayList<InputData> dane = new ArrayList<>();
        for (int i = 0; i < setSize; i++) {
            InputData dataWithD = prepareData(predicate);
            while (dane.contains(dataWithD)) {
                dataWithD = prepareData(predicate);
            }
            dane.add(dataWithD);
        }
        return dane;
    }

    private InputData prepareData(Predicate<InputData> predicate) {
        InputData data = creator.createData();
        boolean test = predicate.test(data);
        int d = test ? 1 : (isBipolar ? -1 : 0);
        InputData dataWithD = data.withD(d);
        return dataWithD;
    }
}
