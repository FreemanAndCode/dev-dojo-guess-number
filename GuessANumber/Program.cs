using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GuessANumber.Players;

namespace GuessANumber
{
    class Program
    {
        static IPlayer[] Players = new IPlayer[]
        {
            new DumbBot("Dumb Bot"),
            new AgroBot("Agro Bot"),
            new TimeoutBot("Timeout Bot"),
            new Player1(),
            new Player2(),
            new Player3(),
            new Player4(),
            new Player5(),
            new Player6(),
            new Player7(),
            new Player8(),
            new Player9(),
            new Player10(),
        };

        static void Main(string[] args)
        {
            var game = new GameState();

            game.Play();

            Console.ReadLine();
        }

        public class GameState
        {
            private List<PlayerGameState> _playerStates;
            private int _highestGuessableNumber = 20;

            public void Play()
            {
                _playerStates = new List<PlayerGameState>();

                foreach (var player in Players)
                {
                    if (!player.IsEnabled)
                    {
                        continue;
                    }

                    _playerStates.Add(new PlayerGameState(player));
                }

                var playerId = 1;
                var numberOfTimesToPlay = 50;

                foreach (var playerGameState in _playerStates)
                {
                    playerGameState.PlayerId = playerId++;
                    playerGameState.Player.InitiliseGame(playerGameState.PlayerId, _playerStates.Count, _highestGuessableNumber, numberOfTimesToPlay);
                }

                for (var i = 0; i < numberOfTimesToPlay; i++)
                {
                    PlayARound(i);
                    DrawResults();
                }
            }

            private void PlayARound(int roundNumber)
            {
                var guesses = new List<Guess>();

                // GetEachPersonsGuess
                foreach (var playerState in _playerStates)
                {
                    var guess = new Guess();

                    Thread t = new Thread((() => { guess = playerState.Player.MakeGuess(roundNumber); }));

                    t.Start();

                    if (!t.Join(2000))
                    {
                        guess.IsInvalid = true;
                        guess.GuessedNumber = 0;
                        playerState.DisqualifiedReason = "Too Slow!";
                    }

                    // validate the guess
                    if (!guess.IsInvalid && (guess.PlayerId != playerState.PlayerId || guess.GuessedNumber > _highestGuessableNumber || guess.GuessedNumber < 1))
                    {
                        playerState.DisqualifiedReason = "Whatcha playing at?";
                        //playerState.Score = 0;
                        guess.IsInvalid = true;
                        guess.GuessedNumber = 0;
                    }

                    guesses.Add(guess);
                }

                // Calculate their score
                foreach (var guess in guesses)
                {
                    if (guess.IsInvalid)
                    {
                        continue;
                    }

                    var score = 0;

                    // check if there is a duplicate guess
                    bool isUnique =
                        !guesses.Any(g => g.PlayerId != guess.PlayerId && g.GuessedNumber == guess.GuessedNumber);

                    if (isUnique)
                    {
                        score = guess.GuessedNumber;
                    }

                    // Check if their position guess is right
                    var guessesGreaterThanMine = guesses.Count(x => x.GuessedNumber > guess.GuessedNumber);

                    if (guessesGreaterThanMine == guess.GuessedPosition - 1)
                    {
                        score = score * 2;
                    }

                    
                    _playerStates.Single(x => x.PlayerId == guess.PlayerId).Score += score;
                }

                // Let the players know what happened
                foreach (var playerGameState in _playerStates)
                {
                    playerGameState.Player.UpdateGuessedValues(guesses.ToArray());
                }
            }

            public void DrawResults()
            {
                Console.Clear();

                foreach (var playerGameState in _playerStates.OrderByDescending(x => x.Score))
                {
                    Console.WriteLine($"{playerGameState.PlayerName} : {playerGameState.Score} {playerGameState.DisqualifiedReason}");
                }
            }
        }

        public class PlayerGameState
        {
            public PlayerGameState(IPlayer player)
            {
                this.Player = player;
                this.Score = 0;
                this.DisqualifiedReason = string.Empty;
                this.PlayerName = player.GetName();
            }

            public int PlayerId { get; set; }

            public string PlayerName { get; private set; }

            public IPlayer Player { get; private set; }

            public int Score { get; set; }

            public string DisqualifiedReason { get; set; }
        }
    }
}
