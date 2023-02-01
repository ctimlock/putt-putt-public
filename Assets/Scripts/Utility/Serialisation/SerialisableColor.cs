using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Serialisation
{
    [Serializable]
    public struct SerialisableColor : IComparable, IComparable<SerialisableColor>, IConvertible, IEquatable<SerialisableColor>
    {
        public float r;
        public float b;
        public float g;
        public float a;

        public SerialisableColor(Color colour)
        {
            this.r = colour.r;
            this.g = colour.g;
            this.b = colour.b;
            this.a = colour.a;
        }

        public static implicit operator Color(SerialisableColor serialiasableColour)
        {
            return new Color(
                serialiasableColour.r,
                serialiasableColour.g,
                serialiasableColour.b,
                serialiasableColour.a
            );
        }

        public static implicit operator SerialisableColor(Color color)
        {
            return new SerialisableColor(color);
        }

        public int CompareTo(object toCompare)
        {
            // TODO: Not Implemented
            return -1;
        }

        public int CompareTo(SerialisableColor toCompare)
        {
            var rComparison = this.r.CompareTo(toCompare.r);
            if (r != 0) return rComparison;
    
            var gComparison = this.g.CompareTo(toCompare.g);
            if (g != 0) return gComparison;

            var bComparison = this.b.CompareTo(toCompare.b);
            if (b != 0) return bComparison;

            var aComparison = this.a.CompareTo(toCompare.a);
            if (a != 0) return aComparison;

            return 0;
        }

        public bool Equals(SerialisableColor toCompare)
        {
            return CompareTo(toCompare) == 1;
        }

        public object ToType(Type type, IFormatProvider formatProvider)
        {
            throw new NotImplementedException("SerialisableColor cannot be converted to an object");
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider formatProvider)
        {
            return (r + g + b + a) > 0;
        }

        public byte ToByte(IFormatProvider formatProvider)
        {
            throw new NotImplementedException("SerialisableColor cannot be converted to a byte");
        }

        public char ToChar(IFormatProvider formatProvider)
        {
            throw new NotImplementedException("SerialisableColor cannot be converted to a char");
        }

        public DateTime ToDateTime(IFormatProvider formatProvider)
        {
            throw new NotImplementedException("SerialisableColor cannot be converted to a DateTime");
        }

        public int ToInt(IFormatProvider formatProvider)
        {
            return (int) (r + g + b + a);
        }

        public decimal ToDecimal(IFormatProvider formatProvider)
        {
            return (decimal) (r + g + b + a);
        }

        public double ToDouble(IFormatProvider formatProvider)
        {
            return (double) (r + g + b + a);
        }

        public Int16 ToInt16(IFormatProvider formatProvider)
        {
            return (Int16) (r + g + b + a);
        }

        public Int32 ToInt32(IFormatProvider formatProvider)
        {
            return (Int32) (r + g + b + a);
        }

        public Int64 ToInt64(IFormatProvider formatProvider)
        {
            return (Int64) (r + g + b + a);
        }

        public SByte ToSByte(IFormatProvider formatProvider)
        {
            throw new NotImplementedException("SerialisableColor cannot be converted to a SByte");
        }

        public Single ToSingle(IFormatProvider formatProvider)
        {
            return (Single) (r + g + b + a);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return (r + g + b + a).ToString();
        }

        public UInt16 ToUInt16(IFormatProvider formatProvider)
        {
            return (UInt16) (r + g + b + a);
        }

        public UInt32 ToUInt32(IFormatProvider formatProvider)
        {
            return (UInt32) (r + g + b + a);
        }
    
        public UInt64 ToUInt64(IFormatProvider formatProvider)
        {
            return (UInt64) (r + g + b + a);
        }
    }
}