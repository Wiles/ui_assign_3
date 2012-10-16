using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace HockeyStats
{
    public class CsvSerializer
    {
        public string Serialize<T>(IEnumerable<T> entities) where T : class
        {
            var sb = new StringBuilder();

            var properties = new Dictionary<int, PropertyInfo>();

            foreach (var p in typeof(T).GetTypeInfo().DeclaredProperties)
            {
                var attr = p.GetCustomAttribute(typeof(CsvColumnAttribute)) as CsvColumnAttribute;

                if (attr != null)
                {
                    properties.Add(attr.Ordinal, p);
                }
            }

            foreach (var entity in entities)
            {
                var s = new StringBuilder();
                bool first = true;

                foreach (var kvp in properties.OrderBy(k => k.Key))
                {
                    if (!first)
                    {
                        s.Append(",");
                    }
                    var type = kvp.Value.PropertyType;
                    if (type == typeof(DateTime))
                    {
                        s.Append(((DateTime)kvp.Value.GetValue(entity)).ToString("s"));
                    }
                    else
                    {
                        s.Append(kvp.Value.GetValue(entity).ToString());
                    }

                    first = false;
                }

                sb.AppendLine(s.ToString());
            }

            return sb.ToString();
        }

        public IEnumerable<T> Deserialize<T>(string csv) where T : class, new()
        {
            var list = new List<T>();

            var properties = new Dictionary<int, PropertyInfo>();

            foreach (var p in typeof(T).GetTypeInfo().DeclaredProperties)
            {
                var attr = p.GetCustomAttribute(typeof(CsvColumnAttribute)) as CsvColumnAttribute;

                if (attr != null)
                {
                    properties.Add(attr.Ordinal, p);
                }
            }

            var array = properties.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToArray();

            var lines = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var txt = line.Split(",".ToCharArray());
                    var entity = new T();

                    foreach (var kvp in properties.OrderBy(k => k.Key))
                    {
                        var type = kvp.Value.PropertyType;
                        if (type == typeof(string))
                        {
                            kvp.Value.SetValue(entity, txt[kvp.Key]);
                        }
                        else if (type == typeof(int))
                        {
                            kvp.Value.SetValue(entity, int.Parse(txt[kvp.Key]));
                        }
                        else if (type == typeof(DateTime))
                        {
                            kvp.Value.SetValue(entity, DateTime.ParseExact(txt[kvp.Key], "s", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            throw new InvalidOperationException(type.Name);
                        }
                    }

                    list.Add(entity);
                }
            }

            return list;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CsvColumnAttribute : Attribute
    {
        public CsvColumnAttribute(int ordinal)
        {
            this.Ordinal = ordinal;
        }

        public int Ordinal { get; set; }
    }
}
