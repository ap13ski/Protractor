using System;

namespace VectorLibrary
{
    //=========================================================================
    // Summary:
    //   Provides methods (including static) for basic vector operations
    //   in 2D space.
    //=========================================================================

    public class Vector
    {
        private double x1;  // X-coordinate of the vector's tail
        private double y1;  // Y-coordinate of the vector's tail
        private double x2;  // X-coordinate of the vector's head
        private double y2;  // Y-coordinate of the vector's head
        private double m;   // Vector magnitude
        private double a;   // Vector angle relative to the X-axis [degrees]

        public double X1
        {
            get { return x1; }
            private set { x1 = value; }
        }

        public double Y1
        {
            get { return y1; }
            private set { y1 = value; }
        }

        public double X2
        {
            get { return x2; }
            private set { x2 = value; }
        }

        public double Y2
        {
            get { return y2; }
            private set { y2 = value; }
        }

        public double M
        {
            get { return m; }
            private set
            {
                if (value < 0)
                    { m = 0; }
                else
                    { m = value; }
            }
        }

        public double A
        {
            get { return a; }
            private set { a = value; }
        }


        public Vector(double x1, double y1, double x2, double y2)
        {
            SetVectorByPoints(x1, y1, x2, y2);
        }

        public Vector(double x1, double y1, double m, double a, int f)
        {
            SetVectorByAngle(x1, y1, m, a);
        }

        public void SetVectorByPoints(double x1, double y1, double x2, double y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            if (x1 == x2 && y1 == y2) // M == 0
            {
                M = 0;
                A = 0;
            }
            else
            {
                M = GetMagnitude(x1, y1, x2, y2);
                A = GetAngleByPoints(x1, y1, x2, y2);
            }
        }

        public void SetVectorByPoints(Vector v)
        {
            SetVectorByPoints(v.X1, v.Y1, v.X2, v.Y2);
        }
        
        public void SetVectorByAngle(Vector v)
        {
            SetVectorByAngle(v.X1, v.Y1, v.M, v.A);
        }
        
        public void SetVectorByAngle(double x1, double y1, double m, double a)
        {
            X1 = x1;
            Y1 = y1;
            if (m == 0)
            {
                M = 0;
                A = 0;
                X2 = X1;
                Y2 = Y1;
            }
            else
            {
                M = m;
                A = a;
                double Vx = M * Math.Cos(A * Math.PI / 180);
                double Vy = M * Math.Sin(A * Math.PI / 180);
                X2 = X1 + Vx;
                Y2 = Y1 + Vy;
            }
        }

        public static double GetMagnitude(double x1, double y1, double x2, double y2)
        {
            return Math.Pow(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2), 0.5);
        }

        public static double GetAngleByPoints(double x1, double y1, double x2, double y2)
        {
            return Vector.Acos(x1, y1, x2, y2);
        }

        // Shifts the vector to the coordinate origin (parallel shift).
        public void MoveToCenter()
        {
            SetVectorByPoints(0, 0, X2 - X1, Y2 - Y1);
        }

        // Shifts the vector by horizontal and vertical increments
        // (parallel shift).
        public void Move(double dx, double dy)
        {
            SetVectorByPoints(X1 + dx, Y1 + dy, X2 + dx, Y2 + dy);
        }

        // Moves the initial point of a vector to the specified
        // point (parallel shift).
        public void MoveToPoint(double x, double y)
        {
            SetVectorByPoints(x, y, X2 + x - X1, Y2 + y - Y1);
        }

        public static Vector CopyAndPasteAtPoint(Vector v, double new_x, double new_y)
        {
            double dx = v.X2 - v.X1;
            double dy = v.Y2 - v.Y1;
            Vector vector = new Vector(new_x, new_y, new_x + dx, new_y + dy);
            
            return vector;
        }
        
        public Vector CopyAndRotate(double delta)
        {
            Vector v = new Vector(X1, Y1, X2, Y2);
            v.SetVectorByAngle(v.X1, v.Y1, v.M, v.A + delta);
            
            return v;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector v = CopyAndPasteAtPoint(v2, v1.X2, v1.Y2);
            v.SetVectorByPoints(v1.X1, v1.Y1, v.X2, v.Y2);
            
            return v;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector vector = CopyAndPasteAtPoint(v2, v1.X1, v1.Y1);
            vector.SetVectorByPoints(vector.X2, vector.Y2, v1.X2, v1.Y2);
            return vector;
        }

        public static Vector operator *(Vector v, double k)
        {
            Vector vector = new Vector(v.X1, v.Y1, v.X2, v.Y2);
            
            if (k < 0)
                { vector.SetVectorByAngle(vector.X1, vector.Y1, vector.M * Math.Abs(k), vector.A + 180); }
            else 
                { vector.SetVectorByAngle(vector.X1, vector.Y1, vector.M * k, vector.A); }
            
            return vector;
        }

        public static Vector operator *(double k, Vector v)
        {
            return v * k;
        }

        public static double Acos(double x1, double y1, double x2, double y2)
        {
            double acos;
            double m = Math.Pow(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2), 0.5);

            if (x2 >= x1 && y2 >= y1)           // I
            {
                acos = Math.Acos((x2 - x1) / m) * 180 / Math.PI;
            }
            else if (x2 < x1 && y2 >= y1)       // II
            {
                acos = Math.Acos((x2 - x1) / m) * 180 / Math.PI;
            }
            else if (x2 < x1 && y2 < y1)        // III
            {
                acos = Math.Acos((x1 - x2) / m) * 180 / Math.PI + 180;
            }
            else //(x2 >= x1 && y2 < y1)        // IV
            {
                acos = Math.Acos((x1 - x2) / m) * 180 / Math.PI + 180;
            }
            
            return acos;
        }

        public static double Acos(Vector v)
        {
            return Acos(v.X1, v.Y1, v.X2, v.Y2);
        }

    }
}
