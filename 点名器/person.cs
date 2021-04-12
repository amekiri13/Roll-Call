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
        private string localSection;
        private bool IsDefault;//若为真，则ImagePath无效
        public Person(string n, string _sex, string _ID, string c, string ip)
        {
            name = n;
            sex = _sex;
            ID = _ID;
            _class = c;
            ImagePath = ip;
        }
        public Person(string n, string _sex, string _ID, string c, string ip, string section)
        {
            name = n;
            sex = _sex;
            ID = _ID;
            _class = c;
            ImagePath = ip;
            localSection = section;
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
        public void SetLocalSection(string s)
        {
            localSection = s;
        }
        public string GetLocalSection()
        {
            return localSection;
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
        public bool WriteFile(string filepath)
        {
            if (IniOperate.Write(localSection, "name", name, filepath) == 0) return false;
            if (IniOperate.Write(localSection, "sex", sex, filepath) == 0) return false;
            if (IniOperate.Write(localSection, "class", _class, filepath) == 0) return false;
            if (IniOperate.Write(localSection, "ID", ID, filepath) == 0) return false;
            if (IniOperate.Write(localSection, "ImagePath", ImagePath, filepath) == 0) return false;
            return true;
        }

    }
}
