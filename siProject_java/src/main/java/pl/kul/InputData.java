package pl.kul;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.ToString;

@RequiredArgsConstructor
@Getter
@ToString
public class InputData {
    private final int x0 = Const.STABILIZER;
    private final int x1;
    private final int x2;
    private final int d;

    public InputData withD(int d) {
        return new InputData(this.x1, this.x2, d);
    }
}
