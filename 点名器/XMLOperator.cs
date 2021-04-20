using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;

namespace 点名器
{
    class XMLOperator
    {
        XmlDocument Doc;
        XmlReader reader;
        string FilePath;
        public XMLOperator(string FilePath)
        {
            //Doc = new XmlDocument();
            this.FilePath = FilePath;
            Doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            if (System.IO.File.Exists(FilePath))
            {
                reader = XmlReader.Create(FilePath, settings);
                Doc.Load(reader);
                reader.Close();
            }
        }

        public ArrayList GetAllInfomationInPerson()
        {
            ArrayList tempPer = new ArrayList();
            XmlNode xn = Doc.SelectSingleNode("TotalPerson");
            XmlNodeList xnl = xn.ChildNodes;
            foreach(XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1;
                string t_name = xe.GetAttribute("Name").ToString();
                string t_id = xe.GetAttribute("ID").ToString();

                XmlNodeList xnl0 = xe.ChildNodes;
                string t_sex = xnl0.Item(0).InnerText;
                string t_class = xnl0.Item(1).InnerText;
                string t_ip = xnl0.Item(2).InnerText;
                Person temPerson = new Person(t_name, t_sex, t_id, t_class, t_ip);
                tempPer.Add(temPerson);
            }
            
            return tempPer;
        }
        public Person[] GetAllInfomationInPerson_Array()
        {
            ArrayList tempPer = new ArrayList();
            XmlNode xn = Doc.SelectSingleNode("TotalPerson");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1;
                string t_name = xe.GetAttribute("Name").ToString();
                string t_id = xe.GetAttribute("ID").ToString();

                XmlNodeList xnl0 = xe.ChildNodes;
                string t_sex = xnl0.Item(0).InnerText;
                string t_class = xnl0.Item(1).InnerText;
                string t_ip = xnl0.Item(2).InnerText;
                Person temPerson = new Person(t_name, t_sex, t_id, t_class, t_ip);
                tempPer.Add(temPerson);
            }

            return (Person[])tempPer.ToArray();
        }
        public void CreateFile(ArrayList per)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
            var el = xmlDoc.CreateElement("TotalPerson");
            xmlDoc.AppendChild(el);
            foreach (Person p in per)
            {
                XmlElement element1 = xmlDoc.CreateElement("Person");
                XmlAttribute attr_name = xmlDoc.CreateAttribute("Name");
                attr_name.Value = p.GetName();
                element1.Attributes.Append(attr_name);
                XmlAttribute attr_ID = xmlDoc.CreateAttribute("ID");
                attr_ID.Value = p.GetPersonID();
                element1.Attributes.Append(attr_ID);
                el.AppendChild(element1);
                XmlElement element2 = xmlDoc.CreateElement("Sex");
                element2.InnerText = p.GetSex();
                element1.AppendChild(element2);
                XmlElement element3 = xmlDoc.CreateElement("Class");
                element3.InnerText = p.GetClass();
                element1.AppendChild(element3);
                XmlElement element4 = xmlDoc.CreateElement("ImagePath");
                element4.InnerText = p.GetPersonnalImagePath();
                element1.AppendChild(element4);
            }
            xmlDoc.Save(FilePath);
        }
    }
}
