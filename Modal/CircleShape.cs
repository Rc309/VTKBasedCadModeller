using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modal
{
    public class CircleShape
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Radius { get; set; }



        //To create circle
        public vtkPolyData CreateCircle(double centerX, double centerY, double radius, int numSegments = 50)
        {
            var points = vtkPoints.New();
            double angleIncrement = 2 * Math.PI / numSegments;

            // Create the points along the circle's perimeter
            for (int i = 0; i < numSegments; i++)
            {
                double angle = i * angleIncrement;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                points.InsertNextPoint(x, y, 0);
            }

            // Close the circle by adding the first point again
            double firstAngle = 0;
            points.InsertNextPoint(centerX + radius * Math.Cos(firstAngle), centerY + radius * Math.Sin(firstAngle), 0);

            var polyData = vtkPolyData.New();
            polyData.SetPoints(points);

            var cellArray = vtkCellArray.New();
            cellArray.InsertNextCell(numSegments + 1);

            for (int i = 0; i <= numSegments; i++)
            {
                cellArray.InsertCellPoint(i);
            }

            polyData.SetLines(cellArray);

            return polyData;
        }

    }
}
