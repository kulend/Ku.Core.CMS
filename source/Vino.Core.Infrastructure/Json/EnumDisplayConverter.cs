using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Ku.Core.Infrastructure.Json
{
    public class EnumDisplayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Value");
                writer.WriteNull();
                writer.WritePropertyName("Name");
                writer.WriteNull();
                writer.WritePropertyName("Title");
                writer.WriteNull();
                writer.WriteEndObject();
            }
            else
            {
                var name = value.ToString();
                writer.WriteStartObject();
                writer.WritePropertyName("Value");
                writer.WriteValue(value);
                writer.WritePropertyName("Name");
                writer.WriteValue(name);
                writer.WritePropertyName("Title");
                string title = name;
                var field = value.GetType().GetField(name);
                var attr = field?.GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                {
                    title = attr.Name;
                }
                writer.WriteValue(title);

                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsEnum;
        }
    }
}
