﻿namespace Trickbox
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Unions are possible, however, only in structs and only if we do not
    /// use any reference-type inside that struct.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    public struct Color : IEquatable<Color>, IComparable<Color>
    {
        #region Fields

        // 1st byte
        [FieldOffset(0)]
        readonly Byte alpha;
        // 2nd byte ...
        [FieldOffset(1)]
        readonly Byte red;
        [FieldOffset(2)]
        readonly Byte green;
        // 4th and last byte ...
        [FieldOffset(3)]
        readonly Byte blue;
        // combining 1st to 4th byte -- one (32-bit) integer
        [FieldOffset(0)]
        readonly Int32 hashcode;

        #endregion

        #region ctor
        
        public Color(Byte r, Byte g, Byte b)
        {
            hashcode = 0;
            alpha = 255;
            red = r;
            blue = b;
            green = g;
        }
        
        public Color(Byte r, Byte g, Byte b, Byte a)
        {
            hashcode = 0;
            alpha = a;
            red = r;
            blue = b;
            green = g;
        }

        #endregion
        
        #region Properties
        
        public Int32 Value
        {
            get { return hashcode; }
        }
        
        public Byte A
        {
            get { return alpha; }
        }
        
        public Double Alpha
        {
            get { return alpha / 255.0; }
        }
        
        public Byte R
        {
            get { return red; }
        }
        
        public Byte G
        {
            get { return green; }
        }
        
        public Byte B
        {
            get { return blue; }
        }

        #endregion

        #region Equality
        
        public static Boolean operator ==(Color a, Color b)
        {
            return a.hashcode == b.hashcode;
        }
        
        public static Boolean operator !=(Color a, Color b)
        {
            return a.hashcode != b.hashcode;
        }
        
        public Boolean Equals(Color other)
        {
            return this.hashcode == other.hashcode;
        }
        
        public override Boolean Equals(Object obj)
        {
            if (obj is Color)
                return this.Equals((Color)obj);

            return false;
        }

        Int32 IComparable<Color>.CompareTo(Color other)
        {
            return hashcode - other.hashcode;
        }
        
        public override Int32 GetHashCode()
        {
            return hashcode;
        }

        #endregion
    }
}
