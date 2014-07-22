using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;

namespace passkeyring
{
    public class KeyRingController
    {
        private void CreateNewFile(string file)
        {
            using (StreamWriter fs = new StreamWriter(file, false))
            {
                string s = @"<?xml version=""1.0"" encoding=""utf-8""?>" + System.Environment.NewLine;
                s += "<KeyItems></KeyItems>";
                fs.Write(s);
                fs.Close();
            }
        }

        public List<KeyRingItem> GetKeyRing()
        {

            if (!File.Exists("keyring.xml"))
            {
                CreateNewFile("keyring.xml");
            }

            List<KeyRingItem> lst = null;

            try
            {
                XElement keyring = XElement.Load("keyring.xml");
                lst = (from kr in keyring.Elements("KeyItem")
                       orderby kr.Element("name").Value
                       select new KeyRingItem
                       {
                           Name = (string)kr.Element("name"),
                           Username = EncryptionHelper.Decrypt((string)kr.Element("username")),
                           Password = EncryptionHelper.Decrypt((string)kr.Element("password")),
                           Desc = (string)kr.Element("desc")
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return lst;
        }

        public void AddToKeyRing(KeyRingItem kri)
        {
            XDocument doc = XDocument.Load("keyring.xml");

            string encUser = EncryptionHelper.Encrypt(kri.Username);
            string encPass = EncryptionHelper.Encrypt(kri.Password);

            doc.Element("KeyItems").Add(new XElement("KeyItem", new XElement("name", kri.Name), new XElement("username", encUser), new XElement("password", encPass), new XElement("desc", kri.Desc)));

            doc.Save("keyring.xml");
        }

        public void DeleteKeyRingItem(string name)
        {
            XElement sites = XElement.Load("keyring.xml");

            var keyitem = (from s in sites.Elements("KeyItem")
                        where s.Element("name").Value.Equals(name)
                        select s);

            if (keyitem != null)
            {
                keyitem.Remove();
            }

            sites.Save("keyring.xml");
        }

        public void EditKeyRingItem(KeyRingItem kri)
        {
            XElement keyring = XElement.Load("keyring.xml");

            var keyitem = (from s in keyring.Elements("KeyItem")
                           where s.Element("name").Value.Equals(kri.Name)
                           select s).SingleOrDefault();

            if (keyitem != null)
            {
                string encUser = EncryptionHelper.Encrypt(kri.Username);
                string encPass = EncryptionHelper.Encrypt(kri.Password);

                keyitem.Element("desc").SetValue(kri.Desc);
                keyitem.Element("username").SetValue(encUser);
                keyitem.Element("password").SetValue(encPass);
            }

            keyring.Save("keyring.xml");
        }
    }
}
