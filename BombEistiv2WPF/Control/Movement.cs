﻿using System;
using System.Collections.Generic;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Control
{
    public class Movement
    {

        public static void Move(string direction, Player p)
        {

            if (direction == Direction.Up.ToString())
            {
                p.Percenty -= p.Speed;
                p.NewSens = Direction.Up;
            }
            else if (direction == Direction.Down.ToString())
            {
                p.Percenty += p.Speed;
                p.NewSens = Direction.Down;
            }
            else if (direction == Direction.Right.ToString())
            {
                p.Percentx += p.Speed;
                p.NewSens = Direction.Right;
            }
            else if (direction == Direction.Left.ToString())
            {
                p.Percentx -= p.Speed;
                p.NewSens = Direction.Left;
            }
        }

        public static void ChangeFace(List<Player> p)
        {
            try
            {
                foreach (var player in p)
                {
                    if (player.NewSens != player.Sens)
                    {
                        player.Sens = player.NewSens;
                        player.changeFace(Texture._.TypetextureList[Texture._.GetTextureKey(player)]);
                    }

                }
            }catch{}

        }

        public static Bomb PutABomb(Player p)
        {
            return p.PutABomb();
        }
    }
}
