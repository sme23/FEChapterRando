using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEChapterRando.DataClasses
{
    abstract class Module
    {
        public string filePath = "";
        public string ToYAML()
        {
            ISerializer serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            return serializer.Serialize(this);
        }

    }
}
