using System;
using System.IO;
using System.Threading.Tasks.Dataflow;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class LogFile: ILogFile
    {
        private TextWriter TW;
        public DateTime? DT = null;

        public LogFile(TextWriter tw)
        {
            TW = tw;
        }

        private DateTime? getTime()
        {
            if (DT == null)
            {
                return DateTime.Now;
            }
            else
            {
                return DT;
            }
        }

        public void LogDoorLocked(int id)
        {
            TW.WriteLine("{0}: Time for door locked with RFid: {1}",getTime().ToString(), id);
            TW.Flush();
        }

        public void LogDoorUnlocked(int id)
        {
            TW.WriteLine("{0}: Time for door Unlocked with RFid: {1}", getTime().ToString(), id);
            TW.Flush();
        }

    }
}