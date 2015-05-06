using System;
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
        CallAdd();
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

    private static void CallAdd()
    {
      const string filepath = @"C:\src\private\tmp\foersteordens_small.xml";

      var filename = DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
      var writeToFile = @"C:\src\private\tmp\foersteordens_small_added_" + filename + ".xml";

      var xmlnsNames = new[] { "FørsteordensregisterVersionsdata" };

      var reader = new AddKursvaernElements(filepath, "kursværnOverfGl", xmlnsNames);
      reader.WriteToFile(writeToFile);
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
