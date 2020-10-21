using System;
using System.IO;
using System.Threading.Tasks.Dataflow;

namespace Ladeskab.Libary
{
    public class LogFile
    {
        public void LogDoorLocked(int id)
        {
            using (StreamWriter writer = new StreamWriter("log.txt"))
            {
                DateTime DateString = DateTime.Now;
                writer.WriteLine("{0}: Time for door locked with RFid: {1}", DateString.ToString(), id);
            }
        }

        public void LogDoorUnlocked(int id)
        {
            using (StreamWriter writer = new StreamWriter("log.txt"))
            {
                DateTime DateString = DateTime.Now;
                Console.WriteLine("{0}: Time for door Unlocked with RFid: {1}", DateString.ToString(), id);
            }
        }

    }
}