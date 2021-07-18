using System;

namespace DiscordHaxx
{
    class CheckException : Exception
    {
        public string Issue { get; private set; }

        public CheckException(string issue)
        {
            Issue = issue;
        }
    }
}
