using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using Kitware.VTK;
using WindowsFormsApp1.Modal;  



namespace WindowsFormsApp1
{
    public partial class VTKBasedCADModelllerUI : Form
    {
        private vtkRenderer renderer;         
        private vtkActor selectedActor;      
        private vtkRenderWindowInteractor interactor1; 
        private bool isSketchMode = false;      
        private bool isDrawingCircle = false;
        private bool isDrawingLine = false;
        private bool isDrawingPoint = false;
        private bool isDrawingArc = false;
        private bool isDrawingEllipse = false;
        private bool isHandlingShapeClick = false;
        private bool isUpdatingPropertiesPanel = false; 
        private bool isDragging = false;
        private Button closeButton;  
        private Point dragStartPoint;   
        private double startX, startY;
        private Dictionary<vtkActor, object> actorShapeMapping = new Dictionary<vtkActor, object>();   
        public delegate void ShapeClickedEventHandler(object sender, EventArgs e); 
        public event ShapeClickedEventHandler ShapeClicked; 
       
        public VTKBasedCADModelllerUI()
        {
            InitializeComponent();          
            this.Load += Form1_Load;  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupRenderWindowControl();   // Call setup during form load
            InitializePropertiesPanel();   //for property panel                                        
            this.ShapeClicked += HandleShapeClick;   
        }

        //To render window controller 
        private void RenderWindowControl1_Load(object sender, EventArgs e)
        {
            this.renderer = vtkRenderer.New();
            this.renderWindowControl1.RenderWindow.AddRenderer(this.renderer);
            this.renderer.ResetCamera();    
            var picker = vtkPropPicker.New(); 
            this.renderWindowControl1.RenderWindow.GetInteractor().SetPicker(picker);
            this.renderer.SetBackground(0.678, 0.847, 0.902); // Background color
          
            this.renderWindowControl1.RenderWindow.GetInteractor().LeftButtonPressEvt += (s, args) =>
            {
                picker.Pick(this.renderWindowControl1.RenderWindow.GetInteractor().GetEventPosition()[0],
                            this.renderWindowControl1.RenderWindow.GetInteractor().GetEventPosition()[1], 0, this.renderer);

                var pickedActor = picker.GetActor(); 
                if (pickedActor != null)
                {
                    if (selectedActor != null)
                    {
                        selectedActor.GetProperty().SetColor(0.0, 0.0, 0.0); 
                    }
                    
                    selectedActor = pickedActor;
                    selectedActor.GetProperty().SetColor(1.0, 0.0, 0.0);                  
                    OnShapeClicked(pickedActor);  // delegant for property panel 
                }
        
                     this.renderWindowControl1.RenderWindow.Render(); 
            };
        }


        // To allow user to drag and down primitive shapes 
        private void SetupRenderWindowControl()
        {           
            DrawPlane("XY");
            interactor1 = renderWindowControl1.RenderWindow.GetInteractor();       
            var interactor = renderWindowControl1.RenderWindow.GetInteractor();
            
            interactor.LeftButtonPressEvt += (s, args) =>
            {
                var eventPos = interactor.GetEventPosition();
                startX = eventPos[0];
                startY = eventPos[1];
            };
          
            interactor.LeftButtonReleaseEvt += (s, args) =>
            {
                if (isSketchMode)
                {
                    var eventPos = interactor.GetEventPosition();
                    int endX = eventPos[0];
                    int endY = eventPos[1];
                    
                    var worldStart = new vtkCoordinate();
                    worldStart.SetCoordinateSystemToDisplay();
                    worldStart.SetValue(startX, startY, 0);

                    var worldEnd = new vtkCoordinate();
                    worldEnd.SetCoordinateSystemToDisplay();
                    worldEnd.SetValue(endX, endY, 0);

                    var worldStartPos = worldStart.GetComputedWorldValue(renderer);
                    var worldEndPos = worldEnd.GetComputedWorldValue(renderer);
                  
                    double radius = Math.Sqrt(Math.Pow(worldEndPos[0] - worldStartPos[0], 2) + Math.Pow(worldEndPos[1] - worldStartPos[1], 2));
                    double centerX = (worldStartPos[0] + worldEndPos[0]) / 2;
                    double centerY = (worldStartPos[1] + worldEndPos[1]) / 2;
                  
                    if (isDrawingCircle)
                    {
                        DrawCircle(centerX, centerY, radius);  
                        isDrawingCircle = false;               
                    }
                    else if (isDrawingLine)
                    {
                        DrawLine(worldStartPos, worldEndPos);  
                        isDrawingLine = false;                 
                    }
                    else if (isDrawingPoint)
                    {
                        DrawPoint(worldStartPos);  
                        isDrawingPoint = false;    
                    }
                    else if (isDrawingArc)
                    {
                        DrawArc(worldStartPos, worldEndPos);  
                        isDrawingArc = false;                
                    }
                    else if (isDrawingEllipse)
                    {
                        DrawEllipse(worldStartPos, worldEndPos);  
                        isDrawingEllipse = false;               
                    }

                    renderWindowControl1.RenderWindow.Render();  
                }

            };

            ConfigureInteraction(); 
            renderWindowControl1.RenderWindow.Render();
        }

        //To show peroperties panel
        private void InitializePropertiesPanel()
        {  
            propertiesPanel = new Panel();
            propertiesPanel.Location = new System.Drawing.Point(this.ClientSize.Width - 350, 80);
            propertiesPanel.Size = new System.Drawing.Size(250, this.ClientSize.Height - 274);
            propertiesPanel.BorderStyle = BorderStyle.FixedSingle;
            propertiesPanel.BackColor = System.Drawing.Color.LightGray;

            closeButton = new Button();
            closeButton.Text = "X";
            closeButton.Size = new System.Drawing.Size(20, 20);
            closeButton.Location = new System.Drawing.Point(propertiesPanel.Width - 25, 5);

            closeButton.Click += (sender, e) =>
            {
                propertiesPanel.Visible = false;
            };

            propertiesPanel.Controls.Add(closeButton);
            this.Controls.Add(propertiesPanel);
            propertiesPanel.Visible = false;        
            propertiesPanel.MouseDown += PropertiesPanel_MouseDown;
            propertiesPanel.MouseMove += PropertiesPanel_MouseMove;
            propertiesPanel.MouseUp += PropertiesPanel_MouseUp;
   
        }

        //Handles the MouseDown event for the propertiesPanel to enable dragging.
        private void PropertiesPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; 
            }
        }

        //To handle property panel floating movement handle event 
        private void PropertiesPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {             
                Point newLocation = propertiesPanel.Location;
                newLocation.X += e.X - dragStartPoint.X;
                newLocation.Y += e.Y - dragStartPoint.Y;
                propertiesPanel.Location = newLocation; 
            }
        }

        private void PropertiesPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; 
            }
        }

        // Method to handle shape click wvent 
        public void HandleShapeClick(object sender, EventArgs e)
        {
            Console.WriteLine("HandleShapeClick");
           
            if (isHandlingShapeClick)
            {
                return;
            }

            isHandlingShapeClick = true;

            if (sender is vtkActor actor)
            {              
                selectedActor = actor;         
                UpdatePropertiesPanel(selectedActor, actorShapeMapping[selectedActor]);
                propertiesPanel.Visible = true;
                propertiesPanel.BringToFront();
            }
            else
            {
                Console.WriteLine("Clicked object is not a recognized shape.");
            }

            isHandlingShapeClick = false;
        }

        // onshape clicked event listener
        private void OnShapeClicked(vtkActor actor)
        {
            if (!isHandlingShapeClick)
            {                         
                if (selectedActor == actor)
                {
                    UpdatePropertiesPanel(actor, actorShapeMapping[actor]);
                    propertiesPanel.Visible = true;
                    propertiesPanel.BringToFront();
                    return;
                }
    
                if (selectedActor != null)
                {
                    DeselectActor(selectedActor);
                }
        
                 HighlightActor(actor);

                selectedActor = actor;  
                ShapeClicked?.Invoke(actor, EventArgs.Empty);
            }
        }

        // Highlight selected shape in red 
        private void HighlightActor(vtkActor actor)
        {         
            vtkProperty property = actor.GetProperty();
            property.SetColor(1.0, 0.0, 0.0); 
            renderWindowControl1.RenderWindow.Render();
        }

        //deselect drawing
        private void DeselectActor(vtkActor actor)
        {   
            vtkProperty property = actor.GetProperty();
            property.SetColor(0.0, 0.0, 0.0);       
            selectedActor = null;
        }

        // Method to add event listeners to the shapes in the renderer
        private void AddShapeClickHandlers(vtkActor actor, object shape)
        {
            if (renderWindowControl1 != null && renderWindowControl1.RenderWindow != null)
            {    
                vtkRenderWindowInteractor interactor = renderWindowControl1.RenderWindow.GetInteractor();
                interactor.LeftButtonPressEvt += (sender, args) =>
                {
                    OnShapeClicked(actor);
                };
            }
            actorShapeMapping[actor] = shape;
        }


        // Update the constraint values 
        private void UpdatePropertiesPanel(vtkActor actor, object shape)
        {   
            if (isUpdatingPropertiesPanel)
                return;

            isUpdatingPropertiesPanel = true;
           
            try
            {
                if (shape != null)
                {                    
                    propertiesPanel.Controls.Clear();
                    propertiesPanel.Controls.Add(closeButton);                     
                    Label selectedShapeLabel = new Label();
                    selectedShapeLabel.Text = "Selected Shape: " + shape.GetType().Name;
                    selectedShapeLabel.AutoSize = true;
                    selectedShapeLabel.Location = new System.Drawing.Point(10, 10);
                    propertiesPanel.Controls.Add(selectedShapeLabel);
                    
                    var updatedValues = new Dictionary<string, double>();//  dictionaries to store updated values from text boxes

                    if (shape is CircleShape circle)
                    {
                        updatedValues["CenterX"] = circle.CenterX;
                        updatedValues["CenterY"] = circle.CenterY;
                        updatedValues["Radius"] = circle.Radius;

                        AddLabelAndTextBox("X:", circle.CenterX.ToString(), 10, "CenterX", updatedValues);
                        AddLabelAndTextBox("Y:", circle.CenterY.ToString(), 40, "CenterY", updatedValues);
                        AddLabelAndTextBox("Radius:", circle.Radius.ToString(), 70, "Radius", updatedValues);
                        
                        AddApplyButton(130, (s, e) =>
                        {
                            circle.CenterX = updatedValues["CenterX"];
                            circle.CenterY = updatedValues["CenterY"];
                            circle.Radius = updatedValues["Radius"];
                            UpdateCircleShape(actor, circle); 
                        });
                    }
                    else if (shape is LineShape line)
                    {
                        updatedValues["Point1X"] = line.Point1X;
                        updatedValues["Point1Y"] = line.Point1Y;
                        updatedValues["Point2X"] = line.Point2X;
                        updatedValues["Point2Y"] = line.Point2Y;

                        AddLabelAndTextBox("Point1 X:", line.Point1X.ToString(), 10, "Point1X", updatedValues);
                        AddLabelAndTextBox("Point1 Y:", line.Point1Y.ToString(), 40, "Point1Y", updatedValues);
                        AddLabelAndTextBox("Point2 X:", line.Point2X.ToString(), 70, "Point2X", updatedValues);
                        AddLabelAndTextBox("Point2 Y:", line.Point2Y.ToString(), 100, "Point2Y", updatedValues);
               
                        AddApplyButton(160, (s, e) =>
                        {
                            line.Point1X = updatedValues["Point1X"];
                            line.Point1Y = updatedValues["Point1Y"];
                            line.Point2X = updatedValues["Point2X"];
                            line.Point2Y = updatedValues["Point2Y"];
                            UpdateLineShape(actor, line); 
                        });
                    }
                    else if (shape is PointShape point)
                    {
                        updatedValues["X"] = point.X;
                        updatedValues["Y"] = point.Y;

                        AddLabelAndTextBox("X:", point.X.ToString(), 10, "X", updatedValues);
                        AddLabelAndTextBox("Y:", point.Y.ToString(), 40, "Y", updatedValues);

                        AddApplyButton(100, (s, e) =>
                        {
                            point.X = updatedValues["X"];
                            point.Y = updatedValues["Y"];
                            UpdatePointShape(actor, point);
                        });
                    }
                    else if (shape is ArcShape arc)
                    {
                        updatedValues["Point1X"] = arc.Point1X;
                        updatedValues["Point1Y"] = arc.Point1Y;
                        updatedValues["Point2X"] = arc.Point2X;
                        updatedValues["Point2Y"] = arc.Point2Y;

                        AddLabelAndTextBox("Point1 X:", arc.Point1X.ToString(), 10, "Point1X", updatedValues);
                        AddLabelAndTextBox("Point1 Y:", arc.Point1Y.ToString(), 40, "Point1Y", updatedValues);
                        AddLabelAndTextBox("Point2 X:", arc.Point2X.ToString(), 70, "Point2X", updatedValues);
                        AddLabelAndTextBox("Point2 Y:", arc.Point2Y.ToString(), 100, "Point2Y", updatedValues);
    
                        AddApplyButton(160, (s, e) =>
                        {
                            arc.Point1X = updatedValues["Point1X"];
                            arc.Point1Y = updatedValues["Point1Y"];
                            arc.Point2X = updatedValues["Point2X"];
                            arc.Point2Y = updatedValues["Point2Y"];
                            UpdateArcShape(actor, arc);
                        });
                    }
                    else if (shape is EllipseShape ellipse)
                    {
                        updatedValues["Point1X"] = ellipse.Point1X;
                        updatedValues["Point1Y"] = ellipse.Point1Y;
                        updatedValues["Point2X"] = ellipse.Point2X;
                        updatedValues["Point2Y"] = ellipse.Point2Y;

                        AddLabelAndTextBox("Point1 X:", ellipse.Point1X.ToString(), 10, "Point1X", updatedValues);
                        AddLabelAndTextBox("Point1 Y:", ellipse.Point1Y.ToString(), 40, "Point1Y", updatedValues);
                        AddLabelAndTextBox("Point2 X:", ellipse.Point2X.ToString(), 70, "Point2X", updatedValues);
                        AddLabelAndTextBox("Point2 Y:", ellipse.Point2Y.ToString(), 100, "Point2Y", updatedValues);

                        AddApplyButton(160, (s, e) =>
                        {
                            ellipse.Point1X = updatedValues["Point1X"];
                            ellipse.Point1Y = updatedValues["Point1Y"];
                            ellipse.Point2X = updatedValues["Point2X"];
                            ellipse.Point2Y = updatedValues["Point2Y"];
                            UpdateEllipseShape(actor, ellipse); 
                        });
                    }
                }
                shape = null;
            }
            finally
            {
                isUpdatingPropertiesPanel = false;
            }
        }

        //Method to add label in the property panel
        private void AddLabelAndTextBox(string labelText, string textBoxText, int y, string key, Dictionary<string, double> updatedValues)
        {  
            int labelX = propertiesPanel.Width - 180;
            int textBoxX = propertiesPanel.Width - 100;     
            y += 30; // Increase the y coordinate for the label and text box
    
            Label label = new Label();
            label.Text = labelText;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(labelX, y);
            propertiesPanel.Controls.Add(label);
  
            TextBox textBox = new TextBox();
            textBox.Text = textBoxText;
            textBox.Size = new System.Drawing.Size(80, 20);
            textBox.Location = new System.Drawing.Point(textBoxX, y);
            textBox.TextChanged += (s, e) =>
            {
                if (double.TryParse((s as TextBox).Text, out double value))
                {
                    updatedValues[key] = value;
                }
            };
            propertiesPanel.Controls.Add(textBox);
        }

        private void AddApplyButton(int y, EventHandler clickHandler)
        {
            Button applyButton = new Button();
            applyButton.Text = "Apply";
            applyButton.Size = new System.Drawing.Size(80, 30);
            applyButton.Location = new System.Drawing.Point(propertiesPanel.Width - 100, y);
            applyButton.Click += clickHandler;
            propertiesPanel.Controls.Add(applyButton);
        }
    
        //To disable zoom in - zoom out,rotate 
        private void ConfigureInteraction()
        {
            try
            {
                if (isSketchMode)
                {                   
                    interactor1.SetInteractorStyle(vtkInteractorStyleTrackballActor.New()); //To stop rotation, zoom in - zoom out
                   
                }
                else
                {
                        vtkInteractorStyleTrackballCamera style = vtkInteractorStyleTrackballCamera.New();
                        style.SetMotionFactor(10);
                        interactor1.SetInteractorStyle(style);
                        int[] lastPos = null;             // Variables to keep track of last mouse position

                        // Set up mouse events for screen rotation
                        interactor1.MouseMoveEvt += (s, args) =>
                        {
                            int[] pos = interactor1.GetEventPosition(); // Get current mouse position

                            if (lastPos == null)
                            {
                                // Initialize lastPos on the first event
                                lastPos = pos;
                                return;
                            }

                            // Check if the Shift key is pressed
                            int shiftKeyState = interactor1.GetShiftKey();
                            bool isShiftKeyPressed = (shiftKeyState != 0); // Check if shiftKeyState is not equal to 0

                            if (isShiftKeyPressed)
                            {
                                int dx = pos[0] - lastPos[0];
                                int dy = pos[1] - lastPos[1];
                                renderer.GetActiveCamera().Azimuth(dx * 0.5);
                                renderer.GetActiveCamera().Elevation(dy * 0.5);
                                renderer.ResetCameraClippingRange();
                                renderWindowControl1.RenderWindow.Render();
                            }

                            lastPos = pos;     // Update lastPos to current position
                        };

                        // Reset lastPos when mouse button is pressed
                        interactor1.LeftButtonPressEvt += (s, args) =>
                        {
                            lastPos = null;
                        };

                        interactor1.RightButtonPressEvt += (s, args) =>
                        {
                            lastPos = null;
                        };

                    }


            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ConfigureInteraction: {ex.Message}");
            }
        }


        //sketch toostrip
        private void SketchToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            isSketchMode = true; // Activate sketch mode
            ShowPanel(sketchPanel);         
            ConfigureInteraction();
           
        }


        //View toostrip
        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            isSketchMode = false; // Deactivate sketch mode
            ShowPanel(viewPanel);      
            ConfigureInteraction();
           
        }

        //plain toostrip
        private void PlainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(plainPanel);
        }

        //Save toostrip
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(savePanel);
          
        }

        //Delete toostrip
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(deletePanel);
        }

        //show panel
        private void ShowPanel(Panel panelToShow)
        {
            sketchPanel.Visible = panelToShow == sketchPanel;
            plainPanel.Visible = panelToShow == plainPanel;
            viewPanel.Visible = panelToShow == viewPanel;
            savePanel.Visible = panelToShow == savePanel;
            deletePanel.Visible = panelToShow == deletePanel;
        }

        //Viw Button clcik event listener
        private void ViewButton_Click(object sender, EventArgs e)
        {
          
            isSketchMode = false; 
            ConfigureInteraction();
          
        }

        //Circle Button clcik event listener
        private void CircleButton_Click(object sender, EventArgs e)
        {
            if (isSketchMode)
            {
                isDrawingCircle = true;  
                propertiesTextBox.Text = "Drawing Circle"; 
                
            }
        }

        //Line Button clcik event listener
        private void LineButton_Click(object sender, EventArgs e)
        {
            if (isSketchMode)
            {
                isDrawingLine = true;  
            }
        }

        //Point Button clcik event listener
        private void PointButton_Click(object sender, EventArgs e)
        {
            if (isSketchMode)
            {
                isDrawingPoint = true;        
            }
        }

        //Arc Button clcik event listener
        private void ArcButton_Click(object sender, EventArgs e)
        {
            if (isSketchMode)
            {
                isDrawingArc = true;  
            }
        }

        //Ellipse Button clcik event listener
        private void EllipseButton_Click(object sender, EventArgs e)
        {
            if (isSketchMode)
            {
                isDrawingEllipse = true;  
            }
        }

        //Save Button clcik event to save VTK file
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // writer
                vtkRenderWindow renderWindow = renderWindowControl1.RenderWindow;
                vtkRenderWindowInteractor interactor = renderWindow.GetInteractor();
                vtkWindowToImageFilter windowToImageFilter = vtkWindowToImageFilter.New();
                windowToImageFilter.SetInput(renderWindow);
                windowToImageFilter.Update();

                // Open file dialog to choose save location and filename
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "VTK Files (*.vti)|*.vti|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;

                    // Save the image as a VTK file
                    vtkDataSetWriter writer = vtkDataSetWriter.New();
                    writer.SetFileName(fileName);
                    writer.SetInputConnection(windowToImageFilter.GetOutputPort());
                    writer.Write();

                    MessageBox.Show("File saved successfully: " + fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {            
                MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // connector to create circle 
        private void DrawCircle(double centerX, double centerY, double radius)
        {
            var Shape = new CircleShape();
            var polyData = Shape.CreateCircle(centerX, centerY, radius);

            // set up the mapper, actor, and renderer 
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            var actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 0); // Black color

            var circleShape = new CircleShape { CenterX = centerX, CenterY = centerY, Radius = radius };
            AddShapeClickHandlers(actor, circleShape);
      
            renderer.AddActor(actor);
            renderer.ResetCamera(); // Ensure the circle is visible
            renderWindowControl1.RenderWindow.Render(); // Render the scene
        }

        // Update the circle shape in the renderer
        private void UpdateCircleShape(vtkActor actor, CircleShape circle)
        {
            renderer.RemoveActor(actor);        

            var polyData = circle.CreateCircle(circle.CenterX, circle.CenterY, circle.Radius);
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            actor.SetMapper(mapper);
            renderer.AddActor(actor);
            renderWindowControl1.RenderWindow.Render(); 
        }

        // connector to create Line
        private void DrawLine(double[] worldStartPos, double[] worldEndPos)
        {
            var lineShape = new LineShape(); 
            var polyData = lineShape.CreateLine(worldStartPos, worldEndPos); 

            // set up the mapper, actor, and renderer
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            var actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 0); // Black color

            var lineShape1 = new LineShape
            {
                Point1X = worldStartPos[0],
                Point1Y = worldStartPos[1],
                Point2X = worldEndPos[0],
                Point2Y = worldEndPos[1]
            };
             AddShapeClickHandlers(actor, lineShape1);

            renderer.AddActor(actor);
            renderer.ResetCamera(); 
            renderWindowControl1.RenderWindow.Render(); 
            
        }


        // Update the line shape in the renderer
        private void UpdateLineShape(vtkActor actor, LineShape line)
        {
            renderer.RemoveActor(actor);

            var polyData = line.CreateLine(new double[] { line.Point1X, line.Point1Y }, new double[] { line.Point2X, line.Point2Y });
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            actor.SetMapper(mapper);
            renderer.AddActor(actor);
            renderWindowControl1.RenderWindow.Render(); 
        }

        // connector to create Point
        private void DrawPoint(double[] worldStartPos)
        {              
           var pointShape = new PointShape();
           var polyData = pointShape.CreatePoint(worldStartPos);

           // set up the mapper, actor, and renderer
           var mapper = vtkPolyDataMapper.New();
           mapper.SetInputConnection(polyData.GetProducerPort());

           var actor = vtkActor.New();
           actor.SetMapper(mapper);
           actor.GetProperty().SetPointSize(10);
           actor.GetProperty().SetColor(0, 0, 0); 
       
           var pointShape1 = new PointShape { X = worldStartPos[0], Y = worldStartPos[1] };
           AddShapeClickHandlers(actor, pointShape1);

           renderer.AddActor(actor);
           renderer.ResetCamera(); 
           renderWindowControl1.RenderWindow.Render();
       

        }

        // Update the point shape in the renderer
        private void UpdatePointShape(vtkActor actor, PointShape point)
        {         
            renderer.RemoveActor(actor);
            var polyData = point.CreatePoint(new double[] { point.X, point.Y });
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            actor.SetMapper(mapper);
            actor.GetProperty().SetPointSize(10); 
            actor.GetProperty().SetColor(0, 0, 0); 

            renderer.AddActor(actor);
            renderWindowControl1.RenderWindow.Render(); 
        }

        // connector to create arc
        private void DrawArc(double[] worldStartPos, double[] worldEndPos)
        {           
           var arcShape = new ArcShape(); 
           var polyData = arcShape.CreateArc(worldStartPos, worldEndPos); 

           // set up the mapper, actor, and renderer
           var mapper = vtkPolyDataMapper.New();
           mapper.SetInputConnection(polyData.GetProducerPort());

           var actor = vtkActor.New();
           actor.SetMapper(mapper);
           actor.GetProperty().SetColor(0, 0, 0); 

           var arcShape1 = new ArcShape
           {
               Point1X = worldStartPos[0],
               Point1Y = worldStartPos[1],
               Point2X = worldEndPos[0],
               Point2Y = worldEndPos[1]
           };
           AddShapeClickHandlers(actor, arcShape1);

           renderer.AddActor(actor);
           renderer.ResetCamera(); 
           renderWindowControl1.RenderWindow.Render(); 
    
        }

        // Update the arc shape in the renderer
        private void UpdateArcShape(vtkActor actor, ArcShape arc)
        {
            renderer.RemoveActor(actor);
            var polyData = arc.CreateArc(new double[] { arc.Point1X, arc.Point1Y }, new double[] { arc.Point2X, arc.Point2Y });
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 0); // Set the arc color to black

            renderer.AddActor(actor);
            renderWindowControl1.RenderWindow.Render(); // Re-render the scene
        }



        // connector to create Ellipse
        private void DrawEllipse(double[] worldStartPos, double[] worldEndPos)
        {                  
            var ellipseShape = new EllipseShape(); 
            var polyData = ellipseShape.CreateEllipse(worldStartPos, worldEndPos); 

            //set up the mapper, actor, and renderer
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            var actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 0); // Set the ellipse color to black

            var ellipseShape1 = new EllipseShape
            {
                Point1X = worldStartPos[0],
                Point1Y = worldStartPos[1],
                Point2X = worldEndPos[0],
                Point2Y = worldEndPos[1]
            };
            AddShapeClickHandlers(actor, ellipseShape1);
            renderer.AddActor(actor);                      
            renderWindowControl1.RenderWindow.Render(); 
        
        }

        // Update the ellipse shape in the renderer
        private void UpdateEllipseShape(vtkActor actor, EllipseShape ellipse)
        {
            renderer.RemoveActor(actor);

            // Create a new ellipse with the updated points
            var polyData = ellipse.CreateEllipse(new double[] { ellipse.Point1X, ellipse.Point1Y }, new double[] { ellipse.Point2X, ellipse.Point2Y });
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polyData.GetProducerPort());

            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 0); 

            renderer.AddActor(actor);
            renderWindowControl1.RenderWindow.Render(); 
        }

        // Delete Button click event 
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedActor != null) // If an actor is selected
            {
                this.renderer.RemoveActor(selectedActor); 
                selectedActor = null;
                this.renderer.ResetCamera();
                this.renderWindowControl1.RenderWindow.Render(); 
            }
        }

        // To draw plain 
        private void DrawPlane(string plane)
        {
            var mapper = vtkPolyDataMapper.New();
            var actor = vtkActor.New();
            actor.SetMapper(mapper);

            // Adjust the orientation and position based on the selected plane
            switch (plane)
            {
                case "XY":
                    actor.SetOrientation(0, 0, 0);  // No rotation for XY
                    actor.SetPosition(0, 0, 0);     // Position at origin
                    break;

                case "YZ":
                    actor.SetOrientation(0, 90, 0);  // Rotate for YZ
                    actor.SetPosition(0, 0, 0);     
                    break;

                case "XZ":
                    actor.SetOrientation(90, 0, 0);  // Rotate for XZ
                    actor.SetPosition(0, 0, 0);     
                    break;
            }

            renderWindowControl1.RenderWindow.Render();
        }


        // XY button click event 
        private void XYButton_Click(object sender, EventArgs e)
        {          
            DrawPlane("XY");
         
            var camera = renderer.GetActiveCamera();
            camera.SetPosition(0, 0, 1);  // Look down from the Z-axis
            camera.SetViewUp(0, 1, 0);   // Orient along the Y-axis

            var interactor = renderWindowControl1.RenderWindow.GetInteractor();
            interactor.SetInteractorStyle(vtkInteractorStyleTrackballCamera.New()); // Use fixed camera
            renderWindowControl1.RenderWindow.Render();  
        }


        //YZ button click event 
        private void YZButton_Click(object sender, EventArgs e)
        {
            DrawPlane("YZ");
           
            var camera = renderer.GetActiveCamera();
            camera.SetPosition(1, 0, 0);  // Look down from the X-axis
            camera.SetViewUp(0, 0, 1);   // Orient along Z-axis

            var interactor = renderWindowControl1.RenderWindow.GetInteractor();
            interactor.SetInteractorStyle(vtkInteractorStyleTrackballCamera.New()); 
            renderWindowControl1.RenderWindow.Render();  
        }

        //XZ Button click event
        private void ZXButton_Click(object sender, EventArgs e)
        {
            DrawPlane("XZ");
            
            var camera = renderer.GetActiveCamera();
            camera.SetPosition(0, 1, 0);  
            camera.SetViewUp(0, 0, 1); 

            var interactor = renderWindowControl1.RenderWindow.GetInteractor();
            interactor.SetInteractorStyle(vtkInteractorStyleTrackballCamera.New());
            renderWindowControl1.RenderWindow.Render();  
        }


    }
}


