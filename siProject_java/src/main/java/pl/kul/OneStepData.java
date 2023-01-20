package pl.kul;

import lombok.AccessLevel;
import lombok.Builder;
import lombok.Getter;
import lombok.RequiredArgsConstructor;

@Getter
@Builder(setterPrefix = "with")
@RequiredArgsConstructor(access = AccessLevel.PRIVATE)
class OneStepData {
    private final InputData inputData;
    private final WeightsVector weightsVector;
    private final Result result;
}
