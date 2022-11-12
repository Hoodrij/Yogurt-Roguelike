using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BigMath;

namespace FlowJob
{
    [DebuggerDisplay("{Name}")]
    public struct Mask : IComparable<Int256>, IEquatable<Int256>
    {
        public bool IsEmpty => value == 0;
        
        private Int256 value;

        public void Set(byte other)
        {
            Int256 otherMask = Int256.One << other;
            value |= otherMask;
        }
        
        public void UnSet(byte other)
        {
            Int256 otherMask = Int256.One << other;
            value &= ~otherMask;
        }
        
        public bool Has(byte other)
        {
            Int256 otherMask = Int256.One << other;
            return (value & otherMask) == otherMask;
        }

        public readonly bool HasAny(Mask other)
        {
            return (value & other.value) != 0;
        }

        public readonly bool HasAll(Mask other)
        {
            return (value & other.value) == other.value;
        }

        public void Clear()
        {
            value = 0;
        }

        public Mask And(Mask other)
        {
            Mask result = default;
            result.value = value | other.value;
            return result;
        }

        public IEnumerable<byte> GetBytes()
        {
            for (byte i = 0; i < byte.MaxValue; i++)
            {
                if (Has(i))
                {
                    yield return i;
                }
            }
        }
        
        public static Mask operator <<(Mask mask, int shift)
        {
            mask.value <<= shift;
            return mask;
        }
        
        public static Mask operator >>(Mask mask, int shift)
        {
            mask.value >>= shift;
            return mask;
        }
        
        public static Mask operator |(Mask mask, Mask other)
        {
            return mask.And(other);
        }
        
        public override string ToString()
        {
            return value.ToString();
        }
        
        public int CompareTo(Int256 other)
        {
            return value.CompareTo(other);
        }

        public bool Equals(Int256 other)
        {
            return value.Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        
        private string Name
        {
            get
            {
                string components = string.Concat(GetBytes().Select(b => $"{((ComponentID)b).Name} "));
                return components;
            }
        }
    }
}