using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
   
    public class CategoryName : IEquatable<CategoryName>
    {
        public string Value { get; }

        public CategoryName(string value)
        {
            // Example validation - ensure name is not empty or null
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Category name cannot be empty or null.", nameof(value));
            }

            Value = value;
        }

        public bool Equals(CategoryName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CategoryName)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static implicit operator string(CategoryName name)
        {
            return name.Value;
        }

        public static explicit operator CategoryName(string value)
        {
            return new CategoryName(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
