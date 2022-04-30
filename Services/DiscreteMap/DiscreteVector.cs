using System;

namespace Services.DiscreteMap
{
    public class DiscreteVector : IEquatable<DiscreteVector>
    {
        public int X { get; }

        public int Y { get; }

        public int Level { get; }

        public DiscreteVector(int x, int y, int l)
        {
            X = x;
            Y = y;
            Level = l;
        }

        #region Math

        public static DiscreteVector operator + (DiscreteVector first, DiscreteVector second) => new DiscreteVector(first.X + second.X, first.Y + second.Y, first.Level + second.Level);

        public static DiscreteVector operator + (DiscreteVector vector, int value) => new DiscreteVector(vector.X + value, vector.Y + value, vector.Level + value);

        public static DiscreteVector operator * (int value, DiscreteVector vector) => new DiscreteVector(vector.X * value, vector.Y * value, vector.Level * value);

        public static DiscreteVector operator - (DiscreteVector first, DiscreteVector second) => first + -1 * second;

        #endregion

        #region IEquatable

        public bool Equals(DiscreteVector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DiscreteVector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Level;
                return hashCode;
            }
        }

        #endregion        
    }
}
