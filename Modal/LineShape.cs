using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modal
{
    public class LineShape
    {
        public double Point1X { get; set; }
        public double Point1Y { get; set; }
        public double Point2X { get; set; }
        public double Point2Y { get; set; }


        // To create Line 
        public vtkPolyData CreateLine(double[] start, double[] end)
        {
            var points = vtkPoints.New();
            points.InsertNextPoint(start[0], start[1], 0); // Start point in world coordinates
            points.InsertNextPoint(end[0], end[1], 0);     // End point in world coordinates

            var polyData = vtkPolyData.New();
            polyData.SetPoints(points);

            var cellArray = vtkCellArray.New();
            cellArray.InsertNextCell(2); // Line with 2 points
            cellArray.InsertCellPoint(0);
            cellArray.InsertCellPoint(1);

            polyData.SetLines(cellArray);

            return polyData;
        }


    }
}
