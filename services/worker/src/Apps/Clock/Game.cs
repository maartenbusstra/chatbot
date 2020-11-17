using System;
using System.Linq;
using System.Collections.Generic;
using bot.Models;

namespace bot.Apps.Clock
{
    public class Game
    {
        private static Player UserToPlayer(User user) =>
          new Player() { Name = user.Name, Score = 0, UserId = user.Id };
        public GameState State { get; set; }
        public List<Player> Players => State.Players;
        public List<Hit> Hits => State.Hits;

        public Game(string state)
        {
            State = GameState.Parse(state);
        }

        public void Reset()
        {
            State = new GameState();
        }

        public string HandleMove(Move move)
        {
            EnsurePlayer(move.Message.User);
            if (HasMoveBeenClaimed(move)) return "already claimed!";
            if (move.Score() < 1) return "move not valid!";

            AddScore(move);
            return "score!";
        }

        public string Scoreboard()
        {
            Players.Sort((a, b) => a.Score < b.Score ? 1 : -1);
            string scores = String.Join("\n", Players.Select(p => $"{p.Name}: {p.Score}").ToArray());
            return $"Current scores:\n{scores}";
        }

        private void AddScore(Move move)
        {
            Players.Find(p => p.UserId == move.Message.User.Id).Score += move.Score();
            Hits.Add(new Hit() { UserId = move.Message.User.Id, MoveId = move.ToUniqueId() });
        }

        private bool HasMoveBeenClaimed(Move move)
        {
            return Hits.FindIndex(h => h.MoveId == move.ToUniqueId()) > -1;
        }

        private void EnsurePlayer(User user)
        {
            if (Players.FindIndex(p => p.UserId == user.Id) > -1) return;
            Players.Add(UserToPlayer(user));
        }
    }
}
