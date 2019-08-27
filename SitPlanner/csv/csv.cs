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
        public List<Tuple<string, string, int, int, string, int, string>> read(String csv_path)
        {
            using (var reader = new StreamReader(csv_path))
            {
                List<Tuple<string, string, int, int, string, int, string>> completeList =
                    new List<Tuple<string, string, int, int, string, int, string>>();
                
                reader.ReadLine();//to skip the headline
                while (!reader.EndOfStream)
                {
                    string firstName = " ";
                    string lastName = " ";
                    string address = " ";
                    string category = " ";
                    int numShouldCome = 0;
                    int phoneNumber = 0;
                    int numOfComing = 0;
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[0].All(Char.IsLetterOrDigit))
                    {
                        firstName = values[0];
                    }
                    if (values[1].All(Char.IsLetterOrDigit))
                    {
                        lastName = values[1];
                    }
                    if (values[2].All(Char.IsDigit))
                    {
                        numShouldCome = Int32.Parse(values[2]);
                    }
                    if (values[3].All(Char.IsDigit))
                    {
                        phoneNumber = Int32.Parse(values[3]);
                    }
                    if (values[4].All(Char.IsLetterOrDigit))
                    {
                        address = values[4];
                    }
                    if (values[5].All(Char.IsDigit))
                    {
                        numOfComing = Int32.Parse(values[5]);
                    }
                    if (values[6].All(Char.IsLetterOrDigit))
                    {
                        category = values[6];
                    }
                    else {continue;}
                    completeList.Add(Tuple.Create(firstName, lastName, numShouldCome, phoneNumber, address, numOfComing, category));
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
