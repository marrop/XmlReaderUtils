using System;
using System.Linq;
using System.Xml;

namespace RemoveXMLElements
{
  public class XmlReaderSkipElement : XmlReaderWriterBase
  {
    public XmlReaderSkipElement(String filepath, String elementToSkip, String[] xmlnsNames)
      : base(filepath, elementToSkip, xmlnsNames)
    {
    }

    public void WriteToFile(String writeToFilepath)
    {
      using (var writer = XmlWriter.Create(writeToFilepath, WriterSettings))
      using (var reader = XmlReader.Create(Filepath, ReaderSettings))
      {
        while (reader.Read())
        {
          switch (reader.NodeType)
          {
            case XmlNodeType.Element:
              if (reader.IsEmptyElement)
              {
                writer.WriteStartElement(reader.Name);
                writer.WriteEndElement();
              }
              else
              {

                if (reader.Name.Equals(ElementToSkip))
                {
                  var tmpReader = reader.ReadSubtree();
                  while (tmpReader.Read())
                  {
                  }
                }
                else
                {
                  if (XmlnsNames.Contains(reader.Name))
                  {
                    var name = reader.Name;
                    reader.MoveToNextAttribute();
                    writer.WriteStartElement(name, reader.Value);
                  }
                  else
                  {
                    writer.WriteStartElement(reader.Name);
                    while (reader.MoveToNextAttribute())
                    {
                      //if (reader.Name.Equals("xmlns"))
                      //  break;

                      writer.WriteAttributes(reader, false);
                    }
                  }
                }
              }
              break;
            case XmlNodeType.Text:
              //write the text in the node
              writer.WriteString(reader.Value);
              break;
            case XmlNodeType.CDATA:
              break;
            case XmlNodeType.ProcessingInstruction:
              writer.WriteProcessingInstruction(reader.Name, reader.Value);
              break;
            case XmlNodeType.Comment:
              writer.WriteComment(reader.Value);
              break;
            case XmlNodeType.Whitespace:
              writer.WriteWhitespace(reader.Value);
              break;
            case XmlNodeType.SignificantWhitespace:
              break;
            case XmlNodeType.EndElement:
              if (reader.Name != ElementToSkip)
              {
                writer.WriteEndElement();
              }
              break;
          }
        }
        
        writer.Close();
      }
    }
  }
}
