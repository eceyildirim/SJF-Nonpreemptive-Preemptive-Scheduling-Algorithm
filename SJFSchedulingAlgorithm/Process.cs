using System;
using System.Collections.Generic;
using System.Text;

namespace SJFSchedulingAlgorithm
{
    public class Process
    {
        public string processName;
        public int start;
        public int burstTime;
        public int end;
        public int priority;
        public int turnAround;
        public int arrivalTime;
        public int iterator;
        public Process(string name, int arrivalTime, int burstTime, int priority)
        {
            processName = name;
            this.burstTime = burstTime;
            this.priority = priority;
            this.arrivalTime = arrivalTime;
        }
    }
}
