﻿using System;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace RemoveXMLElements
{
  public class AddKursvaernElements : XmlReaderWriterBase
  {
    private readonly string _elementToPrefix;

    public AddKursvaernElements(String filepath, String elementToPrefix, String[] xmlnsNames)
      : base(filepath, "", xmlnsNames)
    {
      _elementToPrefix = elementToPrefix;
    }

    public void WriteToFile(String writeToFilepath)
    {
      using (var writer = XmlWriter.Create(writeToFilepath, WriterSettings))
      using (var reader = XmlReader.Create(Filepath, ReaderSettings))
      {
        var prefix = false;
        var elementName = "";
        var elementValue = "0.00";

        var change = false;
        var changeName = "";
        var changeValue = "";
        
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
                if (reader.Name.Equals(_elementToPrefix))
                {
                  elementName = reader.Name;
                  prefix = true;
                }
                else if (reader.Name.Equals("kursværnOverfNy"))
                {
                  changeName = reader.Name;
                  change = true;
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
                      writer.WriteAttributes(reader, false);
                    }
                  }
                }
              }
              break;
            case XmlNodeType.Text:
              //write the text in the node
              if (prefix)
                elementValue = reader.Value;
              else if (change)
                changeValue = reader.Value;
              else
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
              if (prefix)
              {
                // Tilføj nye kursværnselementer
                writer.WriteElementString("kursværnOverfIndenForAftaleGl", elementValue);
                writer.WriteElementString("kursværnOverfIndenForAftaleNy", "0.00");
                writer.WriteElementString("kursværnOverfUdenForAftaleGl", "0.00");
                writer.WriteElementString("kursværnOverfUdenForAftaleNy", "0.00");
                writer.WriteElementString("kursværnOverfUdenOmkostningerGl", "0.00");
                writer.WriteElementString("kursværnOverfUdenOmkostningerNy", "0.00");
                writer.WriteElementString("kursværnOverfDeloverførselAlderssumGl", "0.00");
                writer.WriteElementString("kursværnOverfDeloverførselAlderssumNy", "0.00");
                
                // Tilføj
                if (elementName.Equals(_elementToPrefix))
                  writer.WriteElementString("kursværnInternOverførselGl", elementValue);
                
                prefix = false;
              }
              else if (change)
              {
                if (changeName.Equals("kursværnOverfNy"))
                  writer.WriteElementString("kursværnInternOverførselNy", elementValue);

                change = false;
              }
              else
                writer.WriteEndElement();
              break;
          }
        }

        writer.Close();
      }
    }
  }
}