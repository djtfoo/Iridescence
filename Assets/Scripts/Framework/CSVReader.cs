using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public static class CSVReader {

	public static string[,] GetCSVGridString(string text)
    {
        string[] lines = text.Split("\n"[0]);

        // find the rows and columns inside the CSV
        int rows = lines.Length;    // Y
        int columns = SplitCSVLine(lines[0]).Length;  // X

        string[,] CSVgrid = new string[rows + 1, columns + 1];
        for (int y = 0; y < lines.Length; ++y)  // row
        {
            string[] line = SplitCSVLine(lines[y]);
            for (int x = 0; x < line.Length; ++x)   // column
            {
                CSVgrid[y,x] = line[x];
            }
        }

        return CSVgrid;
    }

    public static int[,] GetCSVGridInt(string text)
    {
        string[] lines = text.Split("\n"[0]);

        // find the rows and columns inside the CSV
        int rows = lines.Length;    // Y
        int columns = SplitCSVLine(lines[0]).Length;  // X

        int[,] CSVgrid = new int[rows + 1, columns + 1];
        for (int y = 0; y < lines.Length; ++y)  // row
        {
            string[] line = SplitCSVLine(lines[y]);
            for (int x = 0; x < line.Length; ++x)   // column
            {
                int.TryParse(line[x], out CSVgrid[y, x]);
            }
        }

        return CSVgrid;
    }

    private static string[] SplitCSVLine(string line)
    {
        string[] values = Regex.Split(line, ",");

        return values;
    }

}
