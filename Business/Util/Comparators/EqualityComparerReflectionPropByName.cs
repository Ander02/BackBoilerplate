using System.Collections.Generic;
using System.Reflection;

namespace Business.Util.Comparators
{
    public class EqualityComparerPropertyInfoName : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y) => x.Name.Equals(y.Name);

        public int GetHashCode(PropertyInfo obj) => obj.Name.GetHashCode();
    }
}
