using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SJFSchedulingAlgorithm
{
    class SchedulingAlgorithms
    {
        Stopwatch Hesapla = new Stopwatch();
        Program p = new Program();

        public List<Process> Sırala()
        {
            //processler Arrival Timelarına göre sıralanır.
            List<Process> processList = p.FileOperations();
            Process temp;
            for (int i = 0; i < processList.Count; i++)
            {
                for (int j = i + 1; j < processList.Count; j++)
                {
                    if (processList[i].arrivalTime > processList[j].arrivalTime || processList[i].arrivalTime == processList[j].arrivalTime && processList[i].burstTime > processList[j].burstTime)
                    {
                        temp = processList[i];
                        processList[i] = processList[j];
                        processList[j] = temp;
                    }
                }
            }
            return processList;
        }



        public void SCFN() //non-pre-emptive 
        {
            Hesapla.Start();

            Process temp;
            List<Process> processList = Sırala();

            Console.WriteLine("______________________________________");
            Console.WriteLine("Gant Chart SCF(Non-Preemptive)");
            Console.WriteLine("______________________________________");



            int counter = 0;
            int minBrT = int.MaxValue;
            Process ptemp;
            int processIndex = 0;


            for (int i = 0; i < processList.Count; i++)
            {
                Console.Write(processList[i].processName + "\t");
                if (processList[i].arrivalTime < counter)
                    Console.Write(counter + "\t");
                else
                {
                    Console.Write(processList[i].arrivalTime + "\t");
                    counter = processList[i].arrivalTime;
                }

                counter += processList[i].burstTime;
                Console.Write(counter + "\t");
                Console.WriteLine();
                //burst time ı küçük olan processin indexi tutulur.
                for (int j = i + 1; j < processList.Count; j++)
                {
                    if (processList[j].arrivalTime <= counter)
                    {
                        if (processList[j].burstTime < minBrT)
                        {
                            minBrT = processList[j].burstTime;
                            processIndex = j;
                        }
                    }
                }

                minBrT = int.MaxValue;
                if ((i + 1) < processList.Count)
                {
                    ptemp = processList[i + 1];
                    processList[i + 1] = processList[processIndex];
                    processList[processIndex] = ptemp;
                }
                //burst timelarına göre düzenlenir ve gant charta eklenir.
            }
            Hesapla.Stop();
            Console.Write("SCFN algoritma çalışma süresi:");
            TimeSpan HesaplananZaman = Hesapla.Elapsed;
            string HesaplamaSonucu = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", HesaplananZaman.Hours, HesaplananZaman.Minutes,
                HesaplananZaman.Seconds, HesaplananZaman.Milliseconds);
            Console.WriteLine(HesaplamaSonucu);
        }



        public void SCFP()
        {
            Hesapla.Start();

            List<Process> processList = Sırala();
            Process ptemp;

            Console.WriteLine("______________________________________");
            Console.WriteLine("Gant Chart SCF(Preemptive)");
            Console.WriteLine("______________________________________");


            int counter = 0;
            int minBrT = int.MaxValue;
            int processValue = 0;

            //arrival time ı gelen processler arasında en düşük burst timelı olan process çalışır.

            for (int i = 0; i < processList.Count; i++)
            {
                if (processList[i].burstTime != 0)
                {


                    if (processList[i].arrivalTime < counter)
                    {
                        Console.Write(processList[i].processName + "\t");
                        Console.Write(counter + "\t");
                    }

                    else
                    {
                        Console.Write(processList[i].processName + "\t");
                        Console.Write(processList[i].arrivalTime + "\t");
                        counter = processList[i].arrivalTime;
                    }

                    if ((i + 1) < processList.Count && processList[i].burstTime != 0)
                    {
                        counter = processList[i + 1].arrivalTime;
                        Console.Write(counter + "\t");
                        Console.WriteLine();
                        processList[i].burstTime = (processList[i].burstTime - (processList[i + 1].arrivalTime - processList[i].arrivalTime));

                    }
                    else
                    {
                        counter += processList[i].burstTime;
                        Console.Write(counter + "\t");

                        for (int k = 0; k < processList.Count - 1; k++)
                        {
                            for (int m = k + 1; m < processList.Count - 1; m++)
                            {

                                if (processList[k].burstTime >= processList[m].burstTime)
                                {

                                    ptemp = processList[k];
                                    processList[k] = processList[m];
                                    processList[m] = ptemp;
                                }

                            }
                        }

                        if (processList[i].burstTime != 0)
                            for (int x = 0; x < processList.Count - 1; x++)
                            {
                                Console.Write(processList[x].processName + "\t");
                                Console.Write(counter + "\t");

                                counter += processList[x].burstTime;
                                Console.Write(counter + "\t");
                                Console.WriteLine();
                            }
                    }


                }
            }
            Hesapla.Stop();
            Console.Write("SCFP algoritma çalışma süresi");
            TimeSpan HesaplananZaman = Hesapla.Elapsed;
            string HesaplamaSonucu = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", HesaplananZaman.Hours, HesaplananZaman.Minutes,
                HesaplananZaman.Seconds, HesaplananZaman.Milliseconds);
            Console.WriteLine(HesaplamaSonucu);

        }
    }
}
