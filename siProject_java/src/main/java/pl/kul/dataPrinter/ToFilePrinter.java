package pl.kul.dataPrinter;

import lombok.SneakyThrows;
import pl.kul.Const;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.time.LocalDateTime;

public class ToFilePrinter implements Printer {

    @Override
    @SneakyThrows
    public void print(String text) {
        BufferedWriter writer = new BufferedWriter(new FileWriter(prepareFilename()));
        writer.write(text);
        writer.close();
    }

    private String prepareFilename() {
        return LocalDateTime.now().format(Const.FORMATTER) + Const.FILE_DEFAULT_NAME;
    }
}
