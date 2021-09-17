using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding_Algorithms.NiceMilk;
namespace Coding_Algorithms
{ // steps fo solving the Nice Milk Problem (10117):
    //step1- identifying all polygons consisting of 4 vertices each, for each of the sides of the original polygon:
    //2 vertices making the side + two vertices that are the intersection points of the line parallel to the side  and 
    // the two sides adjacent to that side, make a new polygon.
    //step2-ordering the resulting polygons by polygon area.
    //step3-starting from the largest area we start adding polygon areas according to the max number of allowed dips.
    //step4-starting from the last area added, we subtract from its area all the intersections areas that could have occured 
    //with any of the previous polygon areas.

    class SolveNiceMilk
    {
        static void Main(string[] args)
        {
         

            // input: polygon vertices,number of dips,height of milk in milk bowel
            int numVertices, NumDips, HeightBowel;
            string temp;
            string[] tokens;
            int x, y;
            GeometryHelper gh = new GeometryHelper();
            List<Point2D> vertices = new List<Point2D>();

            Console.Write("number of Vertices:");
            numVertices = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.Write("number of Dips:");
            NumDips = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Height of Bowel:");
            HeightBowel = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("Enter Vertices' Coordinates");

            for (int i = 0; i < numVertices; i++)
            {
                Console.Write("V{0}(x,y):", i + 1);
                temp = Console.ReadLine();
                tokens = temp.Split(" ");
                x = Int32.Parse(tokens[0]);
                y = Int32.Parse(tokens[1]);
                vertices.Add(new Point2D(x, y));
            }
            Point2D p1, p2, p3, p4,insct1,insct2;
            List<ConvexPolygon2D> allpolygons = new List<ConvexPolygon2D>();
            List<Point2D> allpolygonsTemp = new List<Point2D>();
            p1 = new Point2D(0, 0);
            p2 = new Point2D(0, 0);
            p3 = new Point2D(0, 0);
            p4 = new Point2D(0, 0);
            insct1 = new Point2D(0, 0);
            insct2 = new Point2D(0, 0);

            ConvexPolygon2D poly = new ConvexPolygon2D(gh.orderClockWise(vertices.ToArray()));

            //preparation for step1
            for (int i = 0; i < poly.Corners.Length; i++)
            {
                // processing four points at a time
                // p2-p3 is the side we want to find its parallel at distance HeightBowel
                //p1-p2,p3-p4 are two sides we want to find the intersection of the parallel line with them
                p1 = poly.Corners[i % poly.Corners.Length];
                p2 = poly.Corners[(i + 1) % poly.Corners.Length];
                p3 = poly.Corners[(i + 2) % poly.Corners.Length];
                p4 = poly.Corners[(i + 3) % poly.Corners.Length];

                //coordinates for four points located on two lines parallel to p2p3
                double x1p2=0, x2p2=0, x1p3=0, x2p3 = 0;
                double y1p2 = 0, y2p2 = 0, y1p3 = 0, y2p3 = 0;

                //slope opf p2p3
                double slope = (p2.Y - p3.Y) / (p2.X - p3.X);

                // if p2p3 is not parallel to the y-axis or the x-axis
                if (slope != 0 && slope != double.PositiveInfinity && slope != double.NegativeInfinity)
                {
                    //we solve using an equation of two circles one centered at p2 with a radius of HeightBowel
                    //the other centered at p3 with a radius of HeightBowel
                    // then we substitute the resulting four x coordinates in the equation of the 
                    //line parallel to p2p3 which has slope -1/slope, to get to the four y coordinates
                    // the result is two parallel lines one of them is outside the polygon and therfore rejected
                    x1p2 = p2.X + Math.Sqrt((HeightBowel * HeightBowel) / (1 + (1 / (slope * slope))));
                    x2p2 = p2.X - Math.Sqrt((HeightBowel * HeightBowel) / (1 + (1 / (slope * slope))));
                    x1p3 = p3.X + Math.Sqrt((HeightBowel * HeightBowel) / (1 + (1 /( slope * slope))));
                    x2p3 = p3.X - Math.Sqrt((HeightBowel * HeightBowel) / (1 + (1 / (slope * slope))));

                    y1p2 = p2.Y - (1 / slope) * (x1p2 - p2.X);
                    y2p2 = p2.Y - (1 / slope) * (x2p2 - p2.X);
                    y1p3 = p3.Y - (1 / slope) * (x1p3 - p3.X);
                    y2p3 = p3.Y - (1 / slope) * (x2p3 - p3.X);
                }
                else if (slope == 0)
                {
                    x1p2 = p2.X;
                    x2p2 = p2.X;
                    x1p3 = p3.X;
                    x2p3 = p3.X;
                    y1p2 = p2.Y + HeightBowel;
                    y2p2 = p2.Y - HeightBowel;
                    y1p3 = p3.Y + HeightBowel;
                    y2p3 = p3.Y - HeightBowel;
                }
                else if (slope == double.PositiveInfinity || slope == double.NegativeInfinity)
                {

                    y1p2 = p2.Y;
                    y2p2 = p2.Y;
                    y1p3 = p3.Y;
                    y2p3 = p3.Y;

                    x1p2 = p2.X+ HeightBowel;
                    x2p2 = p2.X- HeightBowel;
                    x1p3 = p3.X+ HeightBowel;
                    x2p3 = p3.X- HeightBowel;
                }

                //intersection points
                Point2D intersect1, intersect2, intersect3, intersect4, intersect5, intersect6, intersect7, intersect8, intersect9;
               //temporary polygons intersecting with the main polygon,one of them is rejacted(zero intersection)
                List<Point2D> FirstIntersectPoly = new List<Point2D>(0);
                List<Point2D> SecondIntersectPoly = new List<Point2D>(0);

                intersect1 = new Point2D(0, 0);
                intersect2 = new Point2D(0, 0);
                intersect3 = new Point2D(0, 0);
                intersect4 = new Point2D(0, 0);
                intersect5 = new Point2D(0, 0);
                intersect6 = new Point2D(0, 0);
                intersect7 = new Point2D(0, 0);
                intersect8 = new Point2D(0, 0);
                intersect9 = new Point2D(0, 0);
                
                intersect1 = gh.GetIntersectionPoint(p1, p2,new Point2D(x1p2,y1p2),new Point2D(x1p3,y1p3),true);
                intersect2 = gh.GetIntersectionPoint(p3, p4, new Point2D(x1p2, y1p2), new Point2D(x1p3, y1p3), true);
                intersect3 = gh.GetIntersectionPoint(p2, p3, new Point2D(x1p2, y1p2), new Point2D(x1p3, y1p3), true);

                //no need to calculate this because the line is not parallel, just for checking purpose
                intersect4 = gh.GetIntersectionPoint(p1, p2, new Point2D(x1p2, y1p2), new Point2D(x2p3, y2p3), true);
                intersect5 = gh.GetIntersectionPoint(p3, p4, new Point2D(x1p2, y1p2), new Point2D(x2p3, y2p3), true);
                intersect6 = gh.GetIntersectionPoint(p2, p3, new Point2D(x1p2, y1p2), new Point2D(x2p3, y2p3), true);

                intersect7 = gh.GetIntersectionPoint(p1, p2, new Point2D(x2p2, y2p2), new Point2D(x2p3, y2p3), true);
                intersect8 = gh.GetIntersectionPoint(p3, p4, new Point2D(x2p2, y2p2), new Point2D(x2p3, y2p3), true);
                intersect9 = gh.GetIntersectionPoint(p2, p3, new Point2D(x2p2, y2p2), new Point2D(x2p3, y2p3), true);

                if (intersect3 != null)
                {
                  
                    FirstIntersectPoly.Add(p2);
                    FirstIntersectPoly.Add(p3);
                    FirstIntersectPoly.Add(intersect4);
                    FirstIntersectPoly.Add(intersect5);

                 
                    SecondIntersectPoly.Add(p2);
                    SecondIntersectPoly.Add(p3);
                    SecondIntersectPoly.Add(intersect7);
                    SecondIntersectPoly.Add(intersect8);
                }

                else if(intersect6 != null)
                {
                   
                    FirstIntersectPoly.Add(p2);
                    FirstIntersectPoly.Add(p3);
                    FirstIntersectPoly.Add(intersect1);
                    FirstIntersectPoly.Add(intersect2);

                  
                    SecondIntersectPoly.Add(p2);
                    SecondIntersectPoly.Add(p3);
                    SecondIntersectPoly.Add(intersect7);
                    SecondIntersectPoly.Add(intersect8);

                }
                else if (intersect9 != null)
                {
                   
                    FirstIntersectPoly.Add(p2);
                    FirstIntersectPoly.Add(p3);
                    FirstIntersectPoly.Add(intersect1);
                    FirstIntersectPoly.Add(intersect2);

                   
                    SecondIntersectPoly.Add(p2);
                    SecondIntersectPoly.Add(p3);
                    SecondIntersectPoly.Add(intersect4);
                    SecondIntersectPoly.Add(intersect5);

                }

                ConvexPolygon2D CheckPoly1, CheckPoly2;
                CheckPoly1 = new ConvexPolygon2D(FirstIntersectPoly.ToArray());
                CheckPoly2 = new ConvexPolygon2D(SecondIntersectPoly.ToArray());

                double checkArea1, checkArea2;

                checkArea1 = gh.PolygonArea(gh.GetIntersectionPolygons(poly,CheckPoly1));

                checkArea2 = gh.PolygonArea(gh.GetIntersectionPolygons(poly, CheckPoly2));

            //step1

                if (checkArea2 == 0 )
                {

                    allpolygons.Add(new ConvexPolygon2D(gh.orderClockWise(FirstIntersectPoly.ToArray())));
                    

                }
                else if (checkArea1==0)
                {
                    allpolygons.Add(new ConvexPolygon2D(gh.orderClockWise(SecondIntersectPoly.ToArray())));

                    
                }

                FirstIntersectPoly.Clear();
                SecondIntersectPoly.Clear();
           
            }

            //step2
            ConvexPolygon2D [] sorted =  allpolygons.OrderBy(v => (gh.PolygonArea(v))).ToArray();

           
            double maxArea = 0;

            //step3
            for (int i = sorted.Length - 1; i > sorted.Length - 1 - NumDips; i--)
            {
                maxArea += gh.PolygonArea(sorted[i]) ;//adding all areas accourding to NumDips and starting from the largest area
            }

            //step4
            for (int i = sorted.Length - NumDips; i < sorted.Length ; i++)
            {
               
                for (int j = i; j < sorted.Length-1; j++)
                {
                    //subtracting intersection areas that were previously added
                    maxArea -= gh.PolygonArea(gh.GetIntersectionPolygons(sorted[i], sorted[j+1]));
                }
            }

            Console.WriteLine("max area = {0}", maxArea);

           

        }
    }
}

    