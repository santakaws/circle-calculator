using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;




public class circleCalcInterface: Form
{private Label welcome = new Label();
 private Label author = new Label();
 private Label sequencemessage = new Label();
 private TextBox sequenceinputarea = new TextBox();
 private Label circumferenceinfo = new Label();
 private Label surfaceareainfo = new Label();
 private Button computebutton = new Button();
 private Button clearbutton = new Button();
 private Button exitbutton = new Button();
 private Panel headerpanel = new Panel();
 private Graphicpanel displaypanel = new Graphicpanel();
 private Panel controlpanel = new Panel();
 private Size maximumfibonacciinterfacesize = new Size(1024,800);
 private Size minimumfibonacciinterfacesize = new Size(1024,800);
 private enum Status {Initial_display,Successful_calculation,Error};
 private static Status outcome = Status.Initial_display;
 private enum Execution_state {Executing, Waiting_to_terminate};             
 private Execution_state current_state = Execution_state.Executing;
 private static System.Timers.Timer exit_clock = new System.Timers.Timer();
 

 public circleCalcInterface() {
    MaximumSize = maximumfibonacciinterfacesize;
    MinimumSize = minimumfibonacciinterfacesize;
    //Initialize text strings
    Text = "Circle Computing System";
    welcome.Text = "Welcome to the Circle Calculator";
    author.Text = "Author: Brennon Hahs";
    sequencemessage.Text = "Enter a radius:";
    sequenceinputarea.Text = "Enter number";
    circumferenceinfo.Text = "The circumference will be displayed here.";
    surfaceareainfo.Text = "The surface area will be displayed here";
    computebutton.Text = "Compute";
    clearbutton.Text = "Clear";
    exitbutton.Text = "Exit";
    
    //Set sizes
    Size = new Size(400,240);
    welcome.Size = new Size(800,44);
    author.Size = new Size(375,34);
    sequencemessage.Size = new Size(400,36);
    sequenceinputarea.Size = new Size(200,30);
    circumferenceinfo.Size = new Size(900,80); //This label has a large height to accommodate 2 lines output text.
    surfaceareainfo.Size = new Size(900,80);
    computebutton.Size = new Size(120,60);
    clearbutton.Size = new Size(120,60);
    exitbutton.Size = new Size(120,60);
    headerpanel.Size = new Size(1024,200);
    displaypanel.Size = new Size(1024,400);
    controlpanel.Size = new Size(1024,200);
    
    //Set colors
    headerpanel.BackColor = Color.LightGreen;
    displaypanel.BackColor = Color.LightPink;
    controlpanel.BackColor = Color.LightSkyBlue;
    computebutton.BackColor = Color.Aquamarine;
    clearbutton.BackColor = Color.Aquamarine;
    //exitbutton.BackColor = Color.Aquamarine;
    exitbutton.BackColor = Color.FromArgb(0xA1,0xD4,0xAA);
    
    //Set fonts
    welcome.Font = new Font("Times New Roman",33,FontStyle.Bold);
    author.Font = new Font("Times New Roman",26,FontStyle.Regular);
    sequencemessage.Font = new Font("Arial",26,FontStyle.Regular);
    sequenceinputarea.Font = new Font("Arial",19,FontStyle.Regular);
    circumferenceinfo.Font = new Font("Arial",26,FontStyle.Regular);
    surfaceareainfo.Font = new Font("Arial",26,FontStyle.Regular);
    computebutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    clearbutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    exitbutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    
    //Set position of text within a label
    welcome.TextAlign = ContentAlignment.MiddleCenter;
    author.TextAlign = ContentAlignment.MiddleCenter;
    sequencemessage.TextAlign = ContentAlignment.MiddleLeft;
    circumferenceinfo.TextAlign = ContentAlignment.MiddleLeft;
    surfaceareainfo.TextAlign = ContentAlignment.MiddleLeft;

    //Set locations
    headerpanel.Location = new Point(0,0);
    welcome.Location = new Point(125,26);
    author.Location = new Point(330,100);
    sequencemessage.Location = new Point(100,60);
    sequenceinputarea.Location = new Point(600,60);
    circumferenceinfo.Location = new Point(100,200);
    surfaceareainfo.Location = new Point(100,300);
    computebutton.Location = new Point(200,50);
    clearbutton.Location = new Point(450,50);
    exitbutton.Location = new Point(720,50);
    headerpanel.Location = new Point(0,0);
    displaypanel.Location = new Point(0,200);
    controlpanel.Location = new Point(0,600);

    //Associate the Compute button with the Enter key of the keyboard
    AcceptButton = computebutton;

    //Add controls to the form
    Controls.Add(headerpanel);
    headerpanel.Controls.Add(welcome);
    headerpanel.Controls.Add(author);
    Controls.Add(displaypanel);
    displaypanel.Controls.Add(sequencemessage);
    displaypanel.Controls.Add(sequenceinputarea);
    displaypanel.Controls.Add(circumferenceinfo);
    displaypanel.Controls.Add(surfaceareainfo);
    Controls.Add(controlpanel);
    controlpanel.Controls.Add(computebutton);
    controlpanel.Controls.Add(clearbutton);
    controlpanel.Controls.Add(exitbutton);

    //Register the event handler.  In this case each button has an event handler, but no other 
    //controls have event handlers.
    computebutton.Click += new EventHandler(computefibnumber);
    clearbutton.Click += new EventHandler(cleartext);
    exitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

    //Configure the clock that controls the shutdown      //<== New in version 2.2
    exit_clock.Enabled = false;     //Clock is turned off at start program execution.
    exit_clock.Interval = 3500;     //7500ms = 7.5seconds.  Clock will tick at intervals of 7.5 seconds
    exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.

    //Open this user interface window in the center of the display.
    CenterToScreen();

}//End of constructor Fibuserinterface

 //Method to execute when the compute button receives an event, namely: receives a mouse click
 protected void computefibnumber(Object sender, EventArgs events) {
    double radius;        //Formerly: uint radius;
    string coutput;
    string saoutput;

    try
       {radius = int.Parse(sequenceinputarea.Text);
        if(radius < 0)
            {Console.WriteLine("Negative number input received.  Please try again.");
             coutput = "Negative number received.  Please try again.";
             saoutput = "";
             outcome = Status.Error;
            }
        else
            {double circumference = circleCalcLogic.computeCircumference(radius);
             double surfacearea = circleCalcLogic.computeSurfaceArea(radius);
             if (circumference == 0 || surfacearea == 0)
                   {coutput = "Either circumference or surface area is too large";
                    saoutput = "";
                    //The next assignment is need to avoid the sequence of a large number input (error) followed by
                    //a small number input without first clicking "Clear".
                    radius = 0;
                    outcome = Status.Error;
                   }
             else
                   {coutput = "The circumference is: " + circumference;
                    saoutput = "The surface area is: " + surfacearea;
                    outcome = Status.Successful_calculation;
                   }
            }
       }//End of try
    catch(FormatException malformed_input)
       {Console.WriteLine("Non-integer input received.  Please try again.\n{0}",malformed_input.Message);
        coutput = "Invalid input: no Fibonacci number computed.";
        saoutput = "";
        outcome = Status.Error;
       }//End of catch
     catch(OverflowException too_big)
       {Console.WriteLine("The value inputted is too large.  Try again.\n{0}",too_big.Message);
        coutput = "The input number was too large.";
        saoutput = "";
        outcome = Status.Error;
       }//End of catch
    circumferenceinfo.Text = coutput;
    surfaceareainfo.Text = saoutput;
    displaypanel.Invalidate();
}//End of computefibnumber
   
 //Method to execute when the clear button receives an event, namely: receives a mouse click
 protected void cleartext(Object sender, EventArgs events)
   {sequenceinputarea.Text = ""; //Empty string
    circumferenceinfo.Text = "The circumference will be displayed here.";
    surfaceareainfo.Text = "The surface area will be displayed here";
    outcome = Status.Initial_display;
    displaypanel.Invalidate();
   }//End of cleartext

//Method to execute when the exit button receives an event, namely: receives a mouse click  <== New in version 2.2
protected void stoprun(Object sender, EventArgs events)
   {switch(current_state)
    {case Execution_state.Executing:
             exit_clock.Interval= 3500;     //7500ms = 7.5 seconds
             exit_clock.Enabled = true;
             exitbutton.Text = "Resume";
             current_state = Execution_state.Waiting_to_terminate;
             break;
     case Execution_state.Waiting_to_terminate:
             exit_clock.Enabled = false;
             exitbutton.Text = "Exit";
             current_state = Execution_state.Executing;
             break;
     }//End of switch statement
  }//End of method stoprun.  In C Sharp language "method" means "function".

protected void shutdown(System.Object sender, EventArgs even)                   //<== Revised for version 2.2
    {//This function is called when the clock makes its first "tick", 
     //which occurs 3.5 seconds after the clock starts.
     Close();       //That means close the main user interface window.
    }//End of method shutdown

 public class Graphicpanel: Panel
 {private Brush paint_brush = new SolidBrush(System.Drawing.Color.Green);
  public Graphicpanel() 
        {Console.WriteLine("A graphic enabled panel was created");}  //Constructor writes to terminal
  protected override void OnPaint(PaintEventArgs ee)
  {  Graphics graph = ee.Graphics;
     switch(outcome)
     {case Status.Initial_display: Console.WriteLine("Initial view of the UI is displayed");
           break;
  }//End of switch
  //The next statement looks like recursion, but it really is not recursion.
  //In fact, it calls the method with the same name located in the super class.
  base.OnPaint(ee);
  }//End of OnPaint
 }//End of class Graphicpanel

}//End of clas Fibuserinterface
