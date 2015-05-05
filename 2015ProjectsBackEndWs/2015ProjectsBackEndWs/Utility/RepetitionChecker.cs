using System;
using System.Collections.Generic;
using System.Linq;
using _2015ProjectsBackEndWs.Utility.Struct;

namespace _2015ProjectsBackEndWs.Utility
{
    public static class RepetitionChecker
    {
        private const int MinutesBeforeDeleting = 30;
        private static readonly List<MessageData> LastCalls = new List<MessageData>();

        private static void RemoveOldEntries()
        {
            LastCalls.RemoveAll(c => (c.Sent - DateTime.Now).Minutes >= MinutesBeforeDeleting);
        }

        /// <summary>
        ///     Some calls can't be processed more than once in a short period
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool Check(string hash)
        {
            var result = false;
            RemoveOldEntries();
            if (LastCalls.Any(c => c.Hash == hash)) result = true;
            else LastCalls.Add(new MessageData(hash, DateTime.Now));
            return result;
        }
    }
}