using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPD_Test
{
    public class Anomaly
    {
        private string name = "";

        private double distance;

        private double angle;

        private double width;

        private double height;

        private bool isDefect;

        public string Name { get => name; set => name = value; }
        public double Distance { get => distance; set => distance = value; }
        public double Angle { get => angle; set => angle = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public bool IsDefect { get => isDefect; set => isDefect = value; }

        public override string ToString()
        {
            string str = "";

            str += "Name: " + name + "\n";
            str += "Distance: " + distance.ToString() + "\n";
            str += "Angle: " + angle.ToString() + "\n";
            str += "Width: " + width.ToString() + "\n";
            str += "Height: " + height.ToString() + "\n";
            str += "IsDefect: " + isDefect.ToString() + "\n";

            return str;
        }
    }
}
