using AdminLib.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdminLib
{
    public class ServiceProvider
    {
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
            }
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
        private void AddProviderToXml(Provider pro)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement elem = doc.CreateElement("provider");

            XmlElement LogoUrl = doc.CreateElement("LogoUrl");
            LogoUrl.InnerText = pro.LogUrl;

            XmlElement Name = doc.CreateElement("Company name");
            Name.InnerText = pro.Name;

            XmlElement Percent = doc.CreateElement("Percent");
            Percent.InnerText = pro.Percent.ToString();

            XmlElement Prefices = doc.CreateElement("Prefices");
            foreach (int item in pro.Prefix)
            {
                XmlElement Prefix = doc.CreateElement("Prefix");
                Prefix.InnerText = item.ToString();
                Prefix.AppendChild(Prefix);
            }
            elem.AppendChild(LogoUrl);
            elem.AppendChild(Name);
            elem.AppendChild(Percent);
            elem.AppendChild(Prefices);

            doc.AppendChild(elem);
            doc.Save("Providers.xml");

        }
    }
}
