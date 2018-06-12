using System;
using System.Collections.Generic;
using System.Text;

namespace GuessANumber.Players
{
    class Player3 : IPlayer
    {
    private int _playerId;
    private int _numberOfPlayer;
    private int _highestGuessableNumber;
    private int _numberOfRounds;
    private string _name;

    public Player3()
    {
        _name = "";
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
        var rand = new Random();

        return new Guess()
        {
            GuessedNumber = rand.Next(1, _highestGuessableNumber + 1),
            GuessedPosition = rand.Next(1, _numberOfPlayer + 1),
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
