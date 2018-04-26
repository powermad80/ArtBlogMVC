using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace ArtBlogMVC
{
    public static class ConfigData
    {
        public static string Username()
        {
            return XDocument.Load("Config.xml").Descendants("username").First().Value;
        }

        public static string Password()
        {
            return XDocument.Load("Config.xml").Descendants("password").First().Value;
        }
    }
}
