using System;
using System.Collections.Generic;
using System.Text;

namespace GuessANumber.Players
{
    class TimeoutBot : IPlayer
    {
        private int _playerId;
        private int _numberOfPlayer;
        private int _highestGuessableNumber;
        private int _numberOfRounds;
        private string _name;

        public TimeoutBot(string name)
        {
            _name = name;
        }

        public bool IsEnabled => false;

        public void InitiliseGame(int playerId, int numberOfPlayer, int highestGuessableNumber, int numberOfRounds)
        {
            _highestGuessableNumber = highestGuessableNumber;
            _numberOfPlayer = numberOfPlayer;
            _playerId = playerId;
            _numberOfRounds = numberOfRounds;
        }

        public Guess MakeGuess(int roundNumber)
        {
            for (int i = 0; i < 1000000000; i++)
            {
            }

            var rand = new Random();

            return new Guess()
            {
                GuessedNumber = _highestGuessableNumber,
                GuessedPosition = 1,
                PlayerId = _playerId
            };
        }

        public void UpdateGuessedValues(Guess[] guesses)
        {
            return; // I'm way too dumb for this
        }

        public string GetName()
        {
            return _name;
        }
    }
}
