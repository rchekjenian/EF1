using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Domain.Common
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            return ((ValueObject)obj)
                .GetEqualityComponents()
                .SequenceEqual(GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
