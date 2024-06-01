using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modal
{
    public class EllipseShape
    {
        public double Point1X { get; set; }
        public double Point1Y { get; set; }
        public double Point2X { get; set; }
        public double Point2Y { get; set; }


        //To create Ellipse 
        public vtkPolyData CreateEllipse(double[] worldStartPos, double[] worldEndPos, int numSegments = 100)
        {
            // Calculate the semi-major and semi-minor axes and the center of the ellipse
            double semiMajorAxis = Math.Abs(worldEndPos[0] - worldStartPos[0]) / 2;
            double semiMinorAxis = Math.Abs(worldEndPos[1] - worldStartPos[1]) / 2;
            double centerX = (worldStartPos[0] + worldEndPos[0]) / 2;
            double centerY = (worldStartPos[1] + worldEndPos[1]) / 2;

            var points = vtkPoints.New();
            double angleIncrement = 2 * Math.PI / numSegments;

            // Generate the points for the ellipse
            for (int i = 0; i < numSegments; i++)
            {
                double angle = i * angleIncrement; // Angle in radians
                double x = semiMajorAxis * Math.Cos(angle) + centerX;
                double y = semiMinorAxis * Math.Sin(angle) + centerY;

                points.InsertNextPoint(x, y, 0); // Add point to vtkPoints
            }

            var polyData = vtkPolyData.New();
            polyData.SetPoints(points);

            var cellArray = vtkCellArray.New();
            cellArray.InsertNextCell(numSegments); // Create a cell for the ellipse
            for (int i = 0; i < numSegments; i++)
            {
                cellArray.InsertCellPoint(i); // Add each point to the cell array
            }

            polyData.SetLines(cellArray);

            return polyData;
        }

    }
}
