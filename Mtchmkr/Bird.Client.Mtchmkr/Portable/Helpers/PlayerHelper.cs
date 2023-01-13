//using System.Drawing;

using Bird.Client.Mtchmkr.Portable.Models;
using System;

namespace Bird.Client.Mtchmkr.Helpers
{
    public static class PlayerHelper
    {
        private static BasePlayerModel[] m_Players;

        public static BasePlayerModel[] Players
        {
            get 
            { 
                if (null == m_Players)
                {
                    m_Players = Create();
                }
                return m_Players; }
            set { m_Players = value; }
        }
        static BasePlayerModel[] Create()
        {
            BasePlayerModel[] models = new BasePlayerModel[]
            {
                Create1() , Create2(),Create3(), Create4(),Create5()
            };


            return models;
        }
        private static BasePlayerModel Create1()
        {
            return new BasePlayerModel
            {
                FirstName = "James ",
                LastName = "Boughton",
                PlayerImage = "JB.jpg",
                Games = GameHelper.CreateObservable(),
            };
        }
        private static BasePlayerModel Create2()
        {
            return new BasePlayerModel
            {
                FirstName = "Andrew ",
                LastName = "Bird",
                PlayerImage = "AB.jpg",
                Games = GameHelper.CreateObservable(),
            };
        }
        private static BasePlayerModel Create3()
        {
            return new BasePlayerModel
            {
                FirstName = "Lenny ",
                LastName = "McLean",
                PlayerImage = "Lenny.jpg",
                Games = GameHelper.CreateObservable(),
            };
        }
        private static BasePlayerModel Create4()
        {
            return new BasePlayerModel
            {
                FirstName = "Lenny ",
                LastName = "Henry",
                PlayerImage = "LennyHenry.jpg",
                Games = GameHelper.CreateObservable(),
            };
        }
        private static BasePlayerModel Create5()
        {
            return new BasePlayerModel
            {
                FirstName = "Tucker Jenkins",
                LastName = "Tucker Jenkins",
                PlayerImage = "Tucker.jpg",
                Games = GameHelper.CreateObservable(),
            };
        }

        public static IPlayer Build(IPlayer player)
        {
            var players = PlayerHelper.Create();
            Random rnd = new Random();
            int index = rnd.Next(0, players.Length - 1);
            BasePlayerModel model = players[index];
            player.PlayerKey = model.PlayerKey;
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.Skill = model.Skill;
            player.Games = model.Games;
            player.PlayerImage = model.PlayerImage;
            return player;
        }
    }
}
