using System;
using System.Collections.Generic;
using System.Text;

namespace GuessANumber
{
    public struct Guess
    {
        public int GuessedNumber { get; set; }

        public int GuessedPosition { get; set; }

        public int PlayerId { get; set; }

        public bool IsInvalid { get; set; }
    }
}
