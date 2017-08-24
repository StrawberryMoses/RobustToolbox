﻿using OpenTK;
using System;
using System.Runtime.InteropServices;

namespace SS14.Shared.Maths
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2i : IEquatable<Vector2i>
    {
        /// <summary>
        /// The X component of the Vector2i.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The Y component of the Vector2i.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// Construct a vector from its coordinates.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Compare a vector to another vector and check if they are equal.
        /// </summary>
        /// <param name="other">Other vector to check.</param>
        /// <returns>True if the two vectors are equal.</returns>
        public bool Equals(Vector2i other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Compare a vector to an object and check if they are equal.
        /// </summary>
        /// <param name="obj">Other object to check.</param>
        /// <returns>True if Object and vector are equal.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2i && Equals((Vector2i) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A unique hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static Vector2i operator -(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2i operator +(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X + b.X, a.Y + b.Y);
        }

        public static implicit operator Vector2(Vector2i vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static explicit operator Vector2i(Vector2 vector)
        {
            return new Vector2i((int)vector.X, (int)vector.Y);
        }
    }
}