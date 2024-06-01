using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modal
{
    public class ArcShape
    {
        public double Point1X { get; set; }
        public double Point1Y { get; set; }
        public double Point2X { get; set; }
        public double Point2Y { get; set; }


        //To Create Arc
        public vtkPolyData CreateArc(double[] worldStartPos, double[] worldEndPos)
        {
            var points = vtkPoints.New();
            double centerX = (worldStartPos[0] + worldEndPos[0]) / 2;
            double centerY = (worldStartPos[1] + worldEndPos[1]) / 2;
            double radius = Math.Sqrt(Math.Pow(worldEndPos[0] - worldStartPos[0], 2) + Math.Pow(worldEndPos[1] - worldStartPos[1], 2)) / 2;

            // Calculate the angles of the start and end points relative to the center
            double startAngle = Math.Atan2(worldStartPos[1] - centerY, worldStartPos[0] - centerX) * 180 / Math.PI;
            double endAngle = Math.Atan2(worldEndPos[1] - centerY, worldEndPos[0] - centerX) * 180 / Math.PI;

            // Ensure end angle is greater than start angle (clockwise direction)
            if (endAngle < startAngle)
            {
                endAngle += 360;
            }

            // Number of segments to approximate the arc
            int numSegments = 50;

            double angleIncrement = (endAngle - startAngle) / numSegments;

            for (int i = 0; i <= numSegments; i++)
            {
                double angle = Math.PI * (startAngle + i * angleIncrement) / 180; // Convert to radians
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);

                points.InsertNextPoint(x, y, 0); // Add the point to the collection
            }

            var polyData = vtkPolyData.New();
            polyData.SetPoints(points);

            var cellArray = vtkCellArray.New();
            cellArray.InsertNextCell(numSegments + 1); // Create a cell for the arc
            for (int i = 0; i <= numSegments; i++)
            {
                cellArray.InsertCellPoint(i); // Add each point to the cell array
            }

            polyData.SetLines(cellArray); // Add the lines to the polyData
            return polyData;
        }
    }
}
