using System;
using System.Windows.Forms;

public class circleCalcMain {
    static void Main(string[] args) {
        System.Console.WriteLine("Welcome to the Main method of the Circle Calculator program.");
        circleCalcInterface circapp = new circleCalcInterface();
        Application.Run(circapp);
        System.Console.WriteLine("Main method will now shutdown.");
    }
}