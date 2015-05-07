using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace RemoveXMLElements
{
  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        //CallAdd();
        CallAdd11();
        //CallRemove();
      }
      catch (XmlException xe)
      {
        Console.WriteLine("XML Parsing Error: " + xe);
      }
      catch (IOException ioe)
      {
        Console.WriteLine("File I/O Error: " + ioe);
      }
    }

    private static void CallAdd11()
    {
      const string filepath11Psl = @"C:\src\private\tmp\version11Psl.xml";

      var filename = DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
      var writeToFile11Psl = @"C:\src\private\tmp\version11Psl_added_" + filename + ".xml";

      var xmlnsNames = new[] { "FørsteordensregisterVersionsdata" };

      // Psl
      var readerFlx = new AddKursvaernElements(filepath11Psl, "kursværnOverfGl", xmlnsNames);
      var stopwatch = Stopwatch.StartNew();
      readerFlx.WriteToFile(writeToFile11Psl);
      stopwatch.Stop();
      Console.WriteLine(stopwatch.ElapsedMilliseconds);

      Console.ReadKey();
    }

    private static void CallAdd()
    {
      const string filepathFlx = @"C:\src\private\tmp\foersteordensFlx.xml";
      const string filepathPsl = @"C:\src\private\tmp\foersteordensPsl.xml";

      var filename = DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
      var writeToFileFlx = @"C:\src\private\tmp\foersteordensFlx_added_" + filename + ".xml";
      var writeToFilePsl = @"C:\src\private\tmp\foersteordensPsl_added_" + filename + ".xml";

      var xmlnsNames = new[] { "FørsteordensregisterVersionsdata" };

      // Flx
      var readerFlx = new AddKursvaernElements(filepathFlx, "kursværnOverfGl", xmlnsNames);
      var stopwatch = Stopwatch.StartNew();
      readerFlx.WriteToFile(writeToFileFlx);
      stopwatch.Stop();
      Console.WriteLine(stopwatch.ElapsedMilliseconds);

      // Psl
      var readerPsl = new AddKursvaernElements(filepathPsl, "kursværnOverfGl", xmlnsNames);
      readerPsl.WriteToFile(writeToFilePsl);
      stopwatch.Stop();
      Console.WriteLine(stopwatch.ElapsedMilliseconds);

      Console.ReadKey();
    }

    private static void CallRemove()
    {
      // Alle underelementer vil også blive fjernet
      const string elementToSkip = "Kursværn";

      const string filepath = @"C:\src\private\tmp\StandardRegistrePsl_smaller.xml";

      var filename = DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
      var writeToFile = @"C:\src\private\tmp\StandardRegistrePsl_" + filename + ".xml";

      var xmlnsNames = new[] { "FørsteordensregisterVersionsdata" };

      var reader = new XmlReaderSkipElement(filepath, elementToSkip, xmlnsNames);
      reader.WriteToFile(writeToFile);
    }
  }
}
