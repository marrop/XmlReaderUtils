using System;
using System.Xml;

namespace RemoveXMLElements
{
  public class XmlReaderWriterBase
  {
    protected readonly string Filepath;
    protected readonly string ElementToSkip;
    protected readonly string[] XmlnsNames;

    protected XmlReaderSettings ReaderSettings = new XmlReaderSettings {IgnoreWhitespace = true};
    protected XmlWriterSettings WriterSettings = new XmlWriterSettings {Indent = true};

    public XmlReaderWriterBase(String filepath, String elementToSkip, String[] xmlnsNames)
    {
      Filepath = filepath;
      ElementToSkip = elementToSkip;
      XmlnsNames = xmlnsNames;
    }
  }
}