using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点名器
{
    class Person
    {
        private string name;
        private string ID;
        private string _class;
        private string ImagePath;
        private string sex;
        private bool IsDefault;//若为真，则ImagePath无效
        public Person(string n, string _sex, string _ID, string c, string ip)
        {
            name = n;
            sex = _sex;
            ID = _ID;
            _class = c;
            ImagePath = ip;
        }
        public void SetName(string n)
        {
            name = n;
        }
        public void SetSex(string _sex)
        {
            sex = _sex;
        }
        public void SetClass(string c)
        {
            _class = c;
        }
        public void SetPersonID(string _ID)
        {
            ID = _ID;
        }
        public void SetPersonnalImagePath(string ip)
        {
            ImagePath = ip;
        }
        public string GetName()
        {
            return name;
        }
        public string GetSex()
        {
            return sex;
        }
        public string GetClass()
        {
            return _class;
        }
        public string GetPersonID()
        {
            return ID;
        }
        public string GetPersonnalImagePath()
        {
            return ImagePath;
        }
    }
}
