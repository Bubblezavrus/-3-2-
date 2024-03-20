using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Лаба_3_2сем_
{
    public class Triangle
    {
        public double[] xCoords = new double[3];
        public double[] yCoords = new double[3];
        private double a, b, c;
        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            xCoords[0] = x1;
            yCoords[0] = y1;
            xCoords[1] = x2;
            yCoords[1] = y2;
            xCoords[2] = x3;
            yCoords[2] = y3;

            Sides();
        }
        private void Sides() 
        {
            a = Math.Sqrt(Math.Pow(xCoords[1] - xCoords[0], 2) + Math.Pow(yCoords[1] - yCoords[0], 2));
            b = Math.Sqrt(Math.Pow(xCoords[2] - xCoords[1], 2) + Math.Pow(yCoords[2] - yCoords[1], 2));
            c = Math.Sqrt(Math.Pow(xCoords[0] - xCoords[2], 2) + Math.Pow(yCoords[0] - yCoords[2], 2));
        }

        public bool equals(Triangle other) 
        {   
       return a == other.a && b == other.b && c == other.c;  
        }
       public double TrianglePerimeter()
        {
            return a+b+c;
           
        }
        public double TriangleSquare()
        {
            double halfP = (a + b + c) / 2;
            return Math.Sqrt(halfP * (halfP - a) * (halfP - b) * (halfP - c));
        }
        public double [] TriangleHeights()
        {
            double halfP = (a + b + c) / 2;
            double heightA = (2 * Math.Sqrt(halfP * (halfP - a) * (halfP - b) * (halfP - c))) / a;
            double heightB = (2*Math.Sqrt(halfP * (halfP - a) * (halfP - b) * (halfP - c))) / b;
            double heightC = (2 * Math.Sqrt(halfP * (halfP - a) * (halfP - b) * (halfP - c))) / c;
            return new double[] { heightA, heightB, heightC };
        }
        public double [] TriangleMedians()
        {
            double medianC = (0.5)*Math.Sqrt(2*Math.Pow(a, 2)+2* Math.Pow(b, 2)-Math.Pow(c,2));
            double medianB = (0.5) * Math.Sqrt(2 * Math.Pow(c, 2) + 2 * Math.Pow(a, 2) - Math.Pow(b, 2));
            double medianA = (0.5) * Math.Sqrt(2 * Math.Pow(c, 2) + 2 * Math.Pow(b, 2) - Math.Pow(a, 2));
            return new double[] { medianA, medianB, medianC };
        }
        public double [] TriangleBisectors()
        {
            double BisectorC = Math.Sqrt(a * b * (a + b + c) * (a + b - c)) / (a + b);
            double BisectorB = Math.Sqrt(a * c * (a + b + c) * (a + c - b)) / (a + c);
            double BisectorA = Math.Sqrt(b * c * (a + b + c) * (b + c - a)) / (b + c);
            return new double[] { BisectorA, BisectorB, BisectorC };

        }
        public double InscribedCircleRadius()
        {
            double halfP = (a + b + c) / 2;
            double InscribedCircleRadius = Math.Sqrt(((halfP-a)*(halfP-b)*(halfP-c))/halfP);
            return InscribedCircleRadius;
        }
        public double CircumscribedCircle()
        {
            double halfP = (a + b + c) / 2;
            double CircumscribedCircleRadius = (a * b * c) / (4 * Math.Sqrt(halfP * (halfP - a) * (halfP - b) * (halfP - c)));
            return CircumscribedCircleRadius;
        }
        public string TriangleType()
        {
            bool equilateralTriangle = a == b && b == c && a == c;
            bool rectangularTriangle = Math.Round(Math.Pow(b, 2),2) == Math.Pow(c, 2) + Math.Pow(a, 2);
            bool isoscelesTriangle = a == b | b == c | a == c;
            bool Trianglewithsharpcorners = Math.Pow(b, 2) < Math.Pow(a, 2) + Math.Pow(c, 2);
            bool Trianglewithdumbcorners = Math.Pow(b, 2) > Math.Pow(a, 2) + Math.Pow(c, 2);

            if (equilateralTriangle)
                return "Рівносторонній трикутник";
            else if (rectangularTriangle & isoscelesTriangle)
                return "Прямокутний рівнобедрений трикутник";
            else if (isoscelesTriangle)
                return "Рівнобедрений трикутник";
            else if (rectangularTriangle)
                return "Прямокутний трикутник";
            else if (Trianglewithsharpcorners)
                return "Гострий трикутник";
            else if (Trianglewithdumbcorners)
                return "Тупокутний трикутник";
            else
                return "Невизначено тип трикутника";
        }
        public void RotateTriangle(double angle, string abc)
        {
            double centerX;
            double centerY;
            switch  (abc) 
            { 
                case "a":
                    { centerX = xCoords[0]; centerY = yCoords[0]; break; }
                case "b":
                    { centerX = xCoords[1]; centerY = yCoords[1]; break; }
                case "c":
                    { centerX = xCoords[2]; centerY = yCoords[2]; break; }
                default: { throw new ArgumentException();}
            }

            double angleRad = (angle * Math.PI) / 180;
            for (int i = 0; i < 3; i++)
            {
                double newposX = Math.Round(centerX + (xCoords[i] - centerX) * Math.Round(Math.Cos(angleRad), 2) - (yCoords[i] - centerY) * Math.Round(Math.Sin(angleRad), 2),3);
                double newposY = Math.Round(centerY + (xCoords[i] - centerX) * Math.Round(Math.Sin(angleRad),2) + (yCoords[i] - centerY) * Math.Round(Math.Cos(angleRad), 2),3);
                xCoords[i] = newposX;
                yCoords[i] = newposY;
            }
            Console.WriteLine("Новi координати вершин пiсля обертання:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Вершина {i + 1} має новi координати:({xCoords[i]};{yCoords[i]})");
            }

        }
        public static void SaveToJson(Triangle triangle, string Path)
        {
            string json = JsonConvert.SerializeObject(triangle);
            File.WriteAllText(Path, json);
            Console.WriteLine($"Object saved {Path}.");
        }
        
    }

}






