using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Collections.ObjectModel;

namespace GPD_Test
{
    internal class CSV_Format
    {

        public static List<Anomaly> GetData(string path)
        {

            List<Anomaly> data = new();

            using (var reader = new StreamReader(path))
            {

                var headers = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    
                    var values = line.Split(';');

                    if(values.Length != 6)
                    { 
                        throw new Exception("Неверный формат данных"); 
                    }


                    Anomaly anomaly = new()
                    {
                        Name = (string)values[0],

                        Distance = Convert.ToDouble(values[1]),

                        Angle = Convert.ToDouble(values[2]),

                        Width = Convert.ToDouble(values[3]),

                        Height = Convert.ToDouble(values[4]),

                        IsDefect = YesNoToBool(values[5])
                    };

                    data.Add(anomaly);

                }
            }

            return data;
        }

        private static bool YesNoToBool(string txt)
        {
            if (txt.ToLower() == "yes")
                return true;

            else return false;

        }
    }
}
