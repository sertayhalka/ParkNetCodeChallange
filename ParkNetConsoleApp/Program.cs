using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace ParkNetConsoleApp
{
    public class Word
    {
        public string Charchter { get; set; }
        public int Repeat { get; set; }
    }
    public static class AllOperatinons
    {
        public static void WriteAndCreateXmlFile(string input)
        {
            var fileData = "";

            var doc = new XmlDocument();
            var docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            var productsNode = doc.CreateElement("Words");
            doc.AppendChild(productsNode);

            var wrodList = input.Split(null);
            var output = wrodList.GroupBy(x => x).Select(x => new Word { Charchter = x.Key, Repeat = x.Count() }).OrderBy(x => x.Repeat);
            foreach (var item in output)
            {
                fileData += item.Charchter + " : " + item.Repeat + Environment.NewLine;

                XmlNode productNode = doc.CreateElement("Word");
                XmlAttribute productAttribute = doc.CreateAttribute("Text");
                productAttribute.Value = item.Charchter;
                productNode.Attributes.Append(productAttribute);
                productsNode.AppendChild(productNode);


                XmlAttribute productsAttribute = doc.CreateAttribute("Count");
                productsAttribute.Value = item.Repeat.ToString();
                productNode.Attributes.Append(productsAttribute);
                productsNode.AppendChild(productNode);
            }

            doc.Save(@"C:\Users\ataha\Documents\words.xml");

        }
        public static string ReadMobyDickTextFromUrl()
        {

           return new WebClient().DownloadString("http://www.gutenberg.org/files/2701/2701-0.txt");

        }

    }
   
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Okuma Başladı..");

            AllOperatinons.WriteAndCreateXmlFile(AllOperatinons.ReadMobyDickTextFromUrl());

            Console.WriteLine("Yazma İşlemi Bitti");

            Console.Read();
        }
    }
}
