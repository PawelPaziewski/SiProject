package pl.kul;


import lombok.AccessLevel;
import lombok.Builder;
import lombok.Getter;
import lombok.RequiredArgsConstructor;

@Getter
@Builder(setterPrefix = "with")
@RequiredArgsConstructor(access = AccessLevel.PRIVATE)
class FromUserData {
    private final double ro;
    private final double a;
    private final double b;
    private final boolean bipolar;
    private final int trainingSetSize;
    private final boolean userSpecifiedVector;
    private final double minW;
    private final double maxW;
    private final double w0;
    private final double w1;
    private final double w2;
}
