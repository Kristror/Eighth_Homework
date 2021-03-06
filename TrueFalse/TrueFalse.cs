﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrueFalseLib
{
    public class TrueFalse
    {
        string fileName;
        List<Question> list;
        int numQuestion = -1;
        public int rightAns = 0;
        public string FileName
        {
            set { fileName = value; }
        }
        public TrueFalse(string fileName)
        {
            this.fileName = fileName;
            list = new List<Question>();
        }
        public void Add(string text, bool trueFalse)
        {
            list.Add(new Question(text, trueFalse));
        }
        public void Remove(int index)
        {
            if (list != null && index < list.Count && index >= 0) list.RemoveAt(index);
        }

        public void Check(bool anwser)
        {
            if (numQuestion < list.Count)
            {
                if (anwser == list[numQuestion].trueFalse) rightAns++;
            }
        }

        public Question this[int index]
        {
            get { return list[index]; }
        }
        public void Save()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Question>));
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, list);
            fStream.Close();
        }

        public string Next()
        {
            numQuestion++;
            if (numQuestion < list.Count)
            {
                var quest = list[numQuestion];
                return quest.text;
            }
            else return "Конец";
        }

        public void Restart()
        {
            numQuestion = -1;
            rightAns = 0;
        }

        public void SaveAs(string path)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Question>));
            
                Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                xmlFormat.Serialize(fStream, list);
                fStream.Close();
        }
        public void Load()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Question>));
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            list = (List<Question>)xmlFormat.Deserialize(fStream);
            fStream.Close();
        }
        public int Count
        {
            get { return list.Count; }
        }

        
    }
}
