package pl.kul;

class YCounter {

    int countY(double signal, boolean isBipolar) {
        return signal > Const.BORDER_VALUE ? 1 : (isBipolar ? -1 : 0);
    }
}
