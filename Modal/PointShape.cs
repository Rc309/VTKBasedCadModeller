using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modal
{
    public class PointShape
    {
        public double X { get; set; }
        public double Y { get; set; }

        //To create Point 
        public vtkPolyData CreatePoint(double[] coordinates)
        {
            var points = vtkPoints.New();       // Create a vtkPoints object to store the points of the plus si
            double size = 0.001;              // Define the size of the plus sign

            // Create points for the plus sign
            points.InsertNextPoint(coordinates[0] - size, coordinates[1], 0);
            points.InsertNextPoint(coordinates[0] + size, coordinates[1], 0);
            points.InsertNextPoint(coordinates[0], coordinates[1] - size, 0); 
            points.InsertNextPoint(coordinates[0], coordinates[1] + size, 0);

            // Create lines to connect the points and form the plus sign
            var lines = vtkCellArray.New();
            lines.InsertNextCell(2);
            lines.InsertCellPoint(0);
            lines.InsertCellPoint(1);
            lines.InsertNextCell(2);
            lines.InsertCellPoint(2);
            lines.InsertCellPoint(3);

            // Create a vtkPolyData object
            var polyData = vtkPolyData.New();
            polyData.SetPoints(points);
            polyData.SetLines(lines);

            return polyData;
        }
    }
}
