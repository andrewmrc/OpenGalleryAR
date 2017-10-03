using System;
using UnityEngine;

public struct OnlineMapsVector2d
{
    public double x;
    public double y;

    public OnlineMapsVector2d(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(OnlineMapsVector2d val)
    {
        return new Vector2((float)val.x, (float)val.y);
    }

    public static implicit operator OnlineMapsVector2d(Vector2 vector)
    {
        return new OnlineMapsVector2d(vector.x, vector.y);
    }

    public static OnlineMapsVector2d operator -(OnlineMapsVector2d v1, OnlineMapsVector2d v2)
    {
        return new OnlineMapsVector2d(v1.x - v2.x, v1.y - v2.y);
    }

    public static OnlineMapsVector2d operator +(OnlineMapsVector2d v1, OnlineMapsVector2d v2)
    {
        return new OnlineMapsVector2d(v1.x + v2.x, v1.y + v2.y);
    }

    public static bool operator ==(OnlineMapsVector2d lhs, OnlineMapsVector2d rhs)
    {
        return SqrMagnitude(lhs - rhs) < Double.Epsilon;
    }

    public static bool operator !=(OnlineMapsVector2d lhs, OnlineMapsVector2d rhs)
    {
        return SqrMagnitude(lhs - rhs) >= Double.Epsilon;
    }

    public static OnlineMapsVector2d Lerp(OnlineMapsVector2d a, OnlineMapsVector2d b, double t)
    {
        if (t < 0) t = 0;
        else if (t > 1) t = 1;
        return new OnlineMapsVector2d(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
    }

    public static double SqrMagnitude(OnlineMapsVector2d a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public double SqrMagnitude()
    {
        return x * x + y * y;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() << 2;
    }

    public override bool Equals(object other)
    {
        if (!(other is OnlineMapsVector2d)) return false;
        OnlineMapsVector2d vector2 = (OnlineMapsVector2d)other;
        return x.Equals(vector2.x) && y.Equals(vector2.y);
    }
}