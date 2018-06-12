using System;
using System.Collections.Generic;
using System.Text;

namespace GuessANumber
{
    interface IPlayer
    {
        void InitiliseGame(int playerId, int numberOfPlayer, int highestGuessableNumber, int numberOfRounds);

        Guess MakeGuess(int roundNumber);

        void UpdateGuessedValues(Guess[] guesses);

        string GetName();

        bool IsEnabled { get; }
    }
}
