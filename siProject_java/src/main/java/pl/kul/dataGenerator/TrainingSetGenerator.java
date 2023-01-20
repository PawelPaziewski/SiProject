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
            InputData data = creator.createData();
            boolean test = predicate.test(data);
            int d = test ? 1 : (isBipolar ? -1 : 0);
            dane.add(data.withD(d));
        }
        return dane;
    }
}
