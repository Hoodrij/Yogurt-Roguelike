using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FlowJob
{
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct UInt256 : IComparable<UInt256>
    {
        public static readonly UInt256 Zero = 0ul;
        public static readonly UInt256 One = 1ul;
        public static readonly UInt256 MinValue = Zero;
        public static readonly UInt256 MaxValue = ~Zero;
        public static readonly UInt256 UInt128MaxValue = new(ulong.MaxValue, ulong.MaxValue);

        /* in little endian order so u3 is the most significant ulong */
        [FieldOffset(0)] private readonly ulong u0;
        [FieldOffset(8)] private readonly ulong u1;
        [FieldOffset(16)] private readonly ulong u2;
        [FieldOffset(24)] private readonly ulong u3;

        private UInt256(ulong u0 = 0, ulong u1 = 0, ulong u2 = 0, ulong u3 = 0)
        {
            this.u0 = u0;
            this.u1 = u1;
            this.u2 = u2;
            this.u3 = u3;
        }
        
        // It avoids c#'s way of shifting a 64-bit number by 64-bit, i.e. in c# a << 64 == a, in our version a << 64 == 0.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong Lsh(ulong a, int n)
        {
            var n1 = n >> 1;
            var n2 = n - n1;
            return (a << n1) << n2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong Rsh(ulong a, int n)
        {
            var n1 = n >> 1;
            var n2 = n - n1;
            return (a >> n1) >> n2;
        }

        private static void Lsh(in UInt256 x, int n, out UInt256 res)
        {
            if ((n % 64) == 0)
            {
                switch (n)
                {
                    case 0:
                        res = x;
                        return;
                    case 64:
                        x.Lsh64(out res);
                        return;
                    case 128:
                        x.Lsh128(out res);
                        return;
                    case 192:
                        x.Lsh192(out res);
                        return;
                    default:
                        res = Zero;
                        return;
                }
            }

            res = Zero;
            ulong z0 = res.u0, z1 = res.u1, z2 = res.u2, z3 = res.u3;
            ulong a = 0, b = 0;
            // Big swaps first
            if (n > 192)
            {
                if (n > 256)
                {
                    res = Zero;
                    return;
                }

                x.Lsh192(out res);
                n -= 192;
                goto sh192;
            }
            else if (n > 128)
            {
                x.Lsh128(out res);
                n -= 128;
                goto sh128;
            }
            else if (n > 64)
            {
                x.Lsh64(out res);
                n -= 64;
                goto sh64;
            }
            else
            {
                res = x;
            }

            // remaining shifts
            a = Rsh(res.u0, 64 - n);
            z0 = Lsh(res.u0, n);

            sh64:
            b = Rsh(res.u1, 64 - n);
            z1 = Lsh(res.u1, n) | a;

            sh128:
            a = Rsh(res.u2, 64 - n);
            z2 = Lsh(res.u2, n) | b;

            sh192:
            z3 = Lsh(res.u3, n) | a;

            res = new UInt256(z0, z1, z2, z3);
        }

        public void LeftShift(int n, out UInt256 res)
        {
            Lsh(this, n, out res);
        }

        public static UInt256 operator <<(in UInt256 a, int n)
        {
            a.LeftShift(n, out UInt256 res);
            return res;
        }

        public static void Rsh(in UInt256 x, int n, out UInt256 res)
        {
            // n % 64 == 0
            if ((n & 0x3f) == 0)
            {
                switch (n)
                {
                    case 0:
                        res = x;
                        return;
                    case 64:
                        x.Rsh64(out res);
                        return;
                    case 128:
                        x.Rsh128(out res);
                        return;
                    case 192:
                        x.Rsh192(out res);
                        return;
                    default:
                        res = Zero;
                        return;
                }
            }

            res = Zero;
            ulong z0 = res.u0, z1 = res.u1, z2 = res.u2, z3 = res.u3;
            ulong a = 0, b = 0;
            // Big swaps first
            if (n > 192)
            {
                if (n > 256)
                {
                    res = Zero;
                    return;
                }

                x.Rsh192(out res);
                z0 = res.u0;
                z1 = res.u1;
                z2 = res.u2;
                z3 = res.u3;
                n -= 192;
                goto sh192;
            }
            else if (n > 128)
            {
                x.Rsh128(out res);
                z0 = res.u0;
                z1 = res.u1;
                z2 = res.u2;
                z3 = res.u3;
                n -= 128;
                goto sh128;
            }
            else if (n > 64)
            {
                x.Rsh64(out res);
                z0 = res.u0;
                z1 = res.u1;
                z2 = res.u2;
                z3 = res.u3;
                n -= 64;
                goto sh64;
            }
            else
            {
                res = x;
                z0 = res.u0;
                z1 = res.u1;
                z2 = res.u2;
                z3 = res.u3;
            }

            // remaining shifts
            a = Lsh(res.u3, 64 - n);
            z3 = Rsh(res.u3, n);

            sh64:
            b = Lsh(res.u2, 64 - n);
            z2 = Rsh(res.u2, n) | a;

            sh128:
            a = Lsh(res.u1, 64 - n);
            z1 = Rsh(res.u1, n) | b;

            sh192:
            z0 = Rsh(res.u0, n) | a;

            res = new UInt256(z0, z1, z2, z3);
        }

        public void RightShift(int n, out UInt256 res) => Rsh(this, n, out res);

        public static UInt256 operator >>(in UInt256 a, int n)
        {
            a.RightShift(n, out UInt256 res);
            return res;
        }

        internal void Lsh64(out UInt256 res)
        {
            res = new UInt256(0, u0, u1, u2);
        }

        internal void Lsh128(out UInt256 res)
        {
            res = new UInt256(0, 0, u0, u1);
        }

        internal void Lsh192(out UInt256 res)
        {
            res = new UInt256(0, 0, 0, u0);
        }

        internal void Rsh64(out UInt256 res)
        {
            res = new UInt256(u1, u2, u3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Rsh128(out UInt256 res)
        {
            res = new UInt256(u2, u3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Rsh192(out UInt256 res)
        {
            res = new UInt256(u3);
        }

        public static void Not(in UInt256 a, out UInt256 res)
        {
            {
                ulong u0 = ~a.u0;
                ulong u1 = ~a.u1;
                ulong u2 = ~a.u2;
                ulong u3 = ~a.u3;
                res = new UInt256(u0, u1, u2, u3);
            }
        }
        
        public static void Or(in UInt256 a, in UInt256 b, out UInt256 res)
        {
            {
                res = new UInt256(a.u0 | b.u0, a.u1 | b.u1, a.u2 | b.u2, a.u3 | b.u3);
            }
        }

        public static UInt256 operator |(in UInt256 a, in UInt256 b)
        {
            Or(a, b, out UInt256 res);
            return res;
        }

        public static void And(in UInt256 a, in UInt256 b, out UInt256 res)
        {
            {
                res = new UInt256(a.u0 & b.u0, a.u1 & b.u1, a.u2 & b.u2, a.u3 & b.u3);
            }
        }

        public static UInt256 operator &(in UInt256 a, in UInt256 b)
        {
            And(a, b, out UInt256 res);
            return res;
        }

        public static void Xor(in UInt256 a, in UInt256 b, out UInt256 res)
        {
            {
                res = new UInt256(a.u0 ^ b.u0, a.u1 ^ b.u1, a.u2 ^ b.u2, a.u3 ^ b.u3);
            }
        }

        public static UInt256 operator ^(in UInt256 a, in UInt256 b)
        {
            Xor(a, b, out UInt256 res);
            return res;
        }

        public static UInt256 operator ~(in UInt256 a)
        {
            Not(in a, out UInt256 res);
            return res;
        }

        public static bool operator ==(in UInt256 a, in UInt256 b) => a.Equals(b);

        public static bool operator !=(in UInt256 a, in UInt256 b) => !(a == b);

        public static implicit operator UInt256(ulong value) => new UInt256(value, 0ul, 0ul, 0ul);

        public static bool operator <(in UInt256 a, in UInt256 b) => LessThan(in a, in b);
        public static bool operator <=(in UInt256 a, in UInt256 b) => !LessThan(in b, in a);
        public static bool operator >(in UInt256 a, in UInt256 b) => LessThan(in b, in a);
        public static bool operator >=(in UInt256 a, in UInt256 b) => !LessThan(in a, in b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool LessThan(in UInt256 a, in UInt256 b)
        {
            if (a.u3 != b.u3)
                return a.u3 < b.u3;
            if (a.u2 != b.u2)
                return a.u2 < b.u2;
            if (a.u1 != b.u1)
                return a.u1 < b.u1;
            return a.u0 < b.u0;
        }

        public int CompareTo(UInt256 b) => this < b ? -1 : Equals(b) ? 0 : 1;
        
        public override int GetHashCode() => System.HashCode.Combine(u0, u1, u2, u3);

        public bool Equals(UInt256 other)
        {
            return u0 == other.u0 && u1 == other.u1 && u2 == other.u2 && u3 == other.u3;
        }

        public override bool Equals(object obj)
        {
            return obj is UInt256 other && Equals(other);
        }
    }
}
