using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.csv
{
    public class Csv
    {
        //first name,last name,num of planned to come,phoneNumber,address,num of coming,category
        public List<Tuple<string, string, int, string, string, int, string>> read(String csv_path)
        {
            using (var reader = new StreamReader(csv_path))
            {
                List<Tuple<string, string, int, string, string, int, string>> completeList =
                    new List<Tuple<string, string, int, string, string, int, string>>();
                reader.ReadLine();//to skip the headline
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var firstName = values[0];
                    var lastName = values[1];
                    int numShouldCome = Int32.Parse(values[2]);
                    var phoneNumber = values[3];
                    var address = values[4];
                    int numIsComing = Int32.Parse(values[5]);
                    var category = values[6];
                    completeList.Add(Tuple.Create(firstName, lastName, numShouldCome, phoneNumber, address, numIsComing, category));
                }
                return completeList;
            }
        }

        public void write(String csv_path, List<Tuple<string, string, int, string, string, int, string>> myTuple)
        {
            using (var w = new StreamWriter(csv_path))
            {
                foreach (var t in myTuple)
                {
                    var firstName = t.Item1;
                    var lastName = t.Item2;
                    int numShouldCome = t.Item3;
                    var phoneNumber = t.Item4;
                    var address = t.Item5;
                    int numIsComing = t.Item6;
                    var category = t.Item7;
                    var line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                        firstName, lastName, numShouldCome, phoneNumber, address, numIsComing, category);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }
    }
}
