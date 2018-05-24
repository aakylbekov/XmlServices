using AdminLib.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdminLib
{
    public class ServiceProvider
    {
        public ServiceProvider() : this("") { }
        public ServiceProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
                this.path = Path.Combine(@"\\dc\Студенты\ПКО\SEB-171.2\C#", "OperatorsFrank.xml");
            else
                this.path = path;
        }
        List<Provider> Providers = new List<Provider>();
        List<int> ProvidersPrefix = new List<int>();
        public void Addprovider()
        {
            Provider prov = new Provider();

            Console.WriteLine("Insert company name: ");
            prov.Name = Console.ReadLine();

            Console.WriteLine("Insert company logo: ");
            prov.LogUrl = Console.ReadLine();

            Console.WriteLine("Insert percentage: ");
            prov.Percent = Double.Parse(Console.ReadLine());

            Console.WriteLine("Insert list prefix: " + "of company(for exit press - Enter): ");

            bool exit = true;
            int pre = 0;
            do
            {
                exit = Int32.TryParse(Console.ReadLine(), out pre);
                if (exit && IsExistPref(pre))
                {
                    prov.Prefix.Add(pre);
                }
            } while (!exit);

            //bool proverka = isExists(prov);
            if (isExistsProv(prov))
            {
                Providers.Add(prov);
                ProvidersPrefix.AddRange(prov.Prefix);
                AddProviderToXml(prov);
            }
        }
        public void EditProvider()
        {
            //найти провайдера - 1
            Console.WriteLine("enter provider name");
            SearchProviderByNameForEdit(Console.ReadLine());

        }
        public void DeleteProvider()
        {

        }
        public void SearchProviderByNameForEdit(string name)
        {
            XmlDocument xd = getDocument();
            XmlElement root = xd.DocumentElement;
            bool find = false;
            foreach (XmlElement item in root)
            {
               // find = false;
                foreach (XmlNode i in item.ChildNodes)
                {
                    if (i.Name == "Name" && i.InnerText == name)
                    {
                        find = true;
                    }
                }
                if (find)
                {
                    XmlElement el = Edit(item);
                    //item.RemoveAll();
                    //foreach (XmlElement ch in el.ChildNodes)
                    //{
                    //    item.AppendChild(el);
                    //}
                    break;
                }
            }
            if (find)
                xd.Save(path);
            //  return null;
        }
        public XmlElement Edit(XmlElement prov)
        {
            foreach (XmlElement item in prov.ChildNodes)
            {
                Console.Write(item.Name + ": (" + item.InnerText + ") - ");
                string cn = Console.ReadLine();
                if (!string.IsNullOrEmpty(cn))
                    item.InnerText = cn;
            }
            return prov;
        }
        private bool isExistsProv(Provider pro)
        {
            if (Providers
                .Where(w => w.Name == pro.Name)
                .Count() > 0)
            {
                Console.WriteLine("that provider exist");
                return false;
            }
            return true;

        }
        private bool IsExistPref(int pref)
        {
            if (ProvidersPrefix.Where(item => item == pref).Count() > 0)
            {
                Console.WriteLine("that prefix exist");
                return false;
            }
            return true;
        }
        private string path { get; set; }
        private void AddProviderToXml(Provider pro)
        {
            XmlDocument doc = getDocument();
            XmlElement elem = doc.CreateElement("provider");

            XmlElement LogoUrl = doc.CreateElement("LogoUrl");
            LogoUrl.InnerText = pro.LogUrl;

            XmlElement Name = doc.CreateElement("Name");
            Name.InnerText = pro.Name;

            XmlElement Percent = doc.CreateElement("Percent");
            Percent.InnerText = pro.Percent.ToString();

            XmlElement Prefices = doc.CreateElement("Prefices");
            foreach (int item in pro.Prefix)
            {
                XmlElement Prefix = doc.CreateElement("Prefix");
                Prefix.InnerText = item.ToString();
                Prefices.AppendChild(Prefix);
            }
            elem.AppendChild(LogoUrl);
            elem.AppendChild(Name);
            elem.AppendChild(Percent);
            elem.AppendChild(Prefices);
            //XmlElement root = doc.DocumentElement;
            //root.AppendChild(elem);
            doc.DocumentElement.AppendChild(elem);
            doc.Save(path);

        }

        public XmlDocument getDocument()
        {
            XmlDocument xd = new XmlDocument();

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                xd.Load(path);
            }
            else
            {
                //1
                //FileStream fs = fi.Create();
                //fs.Close();

                //2
                XmlElement xl = xd.CreateElement("Providers");
                xd.AppendChild(xl);
                xd.Save(path);
            }
            return xd;
        }
    }
}
