//using System.Drawing;

using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bird.Client.Mtchmkr.Helpers
{

    public class GameHelper
    {
        private static GameModel[] m_Games;
        private static GameModel[] Games
        {
            get 
            { 
                if (null == m_Games)
                {
                    m_Games = Create();
                }
                return m_Games; 
            }
        }
        public static ObservableCollection<GameModel> CreateObservable()
        {
            ObservableCollection<GameModel> _Collection = new ObservableCollection<GameModel>();
            foreach(var item in Create())
            {
                _Collection.Add(item);
            }
            return _Collection;
        }
        static GameModel[] Create()
        {
            List<GameModel> _Games = new List<GameModel>();
            _Games.Add(new GameModel
            {
                Key = System.Guid.NewGuid(),
                GameName="Snooker",
                Description = "Snooker Descriotion",
                GameImage="Snooker2.jpg"
            });
            _Games.Add(new GameModel
            {
                Key = System.Guid.NewGuid(),
                GameName = "Pool",
                Description = "Pool Descriotion",
                GameImage = "PoolNew.jpg"
            });
            _Games.Add(new GameModel
            {
                Key = System.Guid.NewGuid(),
                GameName = "Pool",
                Description = "Pool Descriotion",
                GameImage = "PoolNew.jpg"
            });
            _Games.Add(new GameModel
            {
                Key = System.Guid.NewGuid(),
                GameName = "9 Ball",
                Description = "American 9 Ball Pool",
                GameImage = "NineBall.jpg"
            });
            _Games.Add(new GameModel
            {
                Key = System.Guid.NewGuid(),
                GameName = "9 Ball",
                Description = "American 9 Ball Pool",
                GameImage = "NineBall.jpg"
            });
            return _Games.ToArray();
        }
        public static IGame Build(IGame game)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, Games.Length-1);
            var item = Games[index];
            game.GameKey = item.Key;
            game.GameName = item.GameName;
            game.GameImage = item.GameImage;
            return game;
        }
        public static ObservableCollection<GameModel> CreateCollection()
        {
            ObservableCollection<GameModel> _Collection = new ObservableCollection<GameModel>();
            foreach(var item in Create())
            {
                _Collection.Add(item);
            }
            return _Collection;
        }
    }
}
