using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SJFSchedulingAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            SchedulingAlgorithms SA = new SchedulingAlgorithms();

            Thread threadSCFN = new Thread(new ThreadStart(SA.SCFN));
            threadSCFN.Start();
            threadSCFN.Join();

            Thread threadSCFP = new Thread(new ThreadStart(SA.SCFP));
            threadSCFP.Start();
            threadSCFP.Join();

            Console.ReadKey();


        }
        public List<Process> FileOperations()
        {
            List<Process> processList = new List<Process>();

            string dosya_yolu = @"processInfo.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                string[] line = yazi.Split(',');

                Process process = new Process(line[0], int.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3]));
                processList.Add(process);

                yazi = sw.ReadLine();

            }

            sw.Close();
            fs.Close();

            return processList;
        }
    }
}
