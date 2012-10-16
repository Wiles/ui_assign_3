using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStats
{
    public static class Extensions
    {
        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> entities)
        {
            foreach (var t in entities)
            {
                collection.Add(t);
            }

            return collection;
        }
    }
}
