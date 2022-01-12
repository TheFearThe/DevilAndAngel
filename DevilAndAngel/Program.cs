﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace DevilAndAngel
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game play = new Game(525,525);
            Angel angel = new Angel(525,525);
            Devil devil = new Devil();
            Console.WindowHeight = 51;
            Console.WindowWidth = 50;            
            while (true)
            {
                DrawField(play,angel);                
                play.Field[angel.previos_pos.X, angel.previos_pos.Y] = 0;
                do
                {
                    var key = Console.ReadKey(true).Key;
                    angel.position = angel.previos_pos;
                    angel.position = SetDirection(key, angel.position);
                } while (play.Field[angel.position.X, angel.position.Y] == 3);
                play.Field[angel.position.X, angel.position.Y] = 1;
                angel.previos_pos = angel.position;
                DevilRound(play, devil, angel);
            }
        }

        static void DrawField(Game a, Angel b)
        {
            for (int x = b.position.X - 25; x < b.position.X + 26; x++)            
                for (int y = b.position.X - 25; y < b.position.X + 26; y++)
                {
                    Console.SetCursorPosition(x % 50, y % 50);
                    if (a.Field[x, y] == 0)
                        Console.Write(" ");
                    if (a.Field[x, y] == 1)
                        Console.Write("O");
                    if (a.Field[x, y] == 2)
                        Console.Write("@");
                    if (a.Field[x, y] == 3)
                        Console.Write("#");
                }                            
        }

        static void DevilRound(Game play,Devil devil,Angel angel)
        {            
            ConsoleKey key = ConsoleKey.Escape;
            devil.position.X = play.screen.X * 50;
            devil.position.Y = play.screen.Y * 50 + 1;            
            play.Field[devil.position.X, devil.position.Y] = 2;
            devil.previos_pos = devil.position;
            bool visited = true;
            do
            {
                DrawField(play,angel);
                key = ConsoleKey.F24;
                while (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                    visited = false;
                }
                
                devil.previos_pos = devil.position;
                devil.position = SetDirection(key, devil.position);
                play.Field[devil.position.X, devil.position.Y] = 2;
                if (!visited)
                { 
                    play.Field[devil.previos_pos.X, devil.previos_pos.Y] = 0;
                    visited = true;
                }
            } while (key != ConsoleKey.Enter);
            play.Field[devil.position.X, devil.position.Y] = 3;
        }

        static Point SetDirection(ConsoleKey key,Point p)
        {
            if (key == ConsoleKey.W)            
                p.Y--;
            if (key == ConsoleKey.X)
                p.Y++;
            if (key == ConsoleKey.A)
                p.X--;
            if (key == ConsoleKey.D)
                p.X++;

            if (key == ConsoleKey.E)
            {
                p.X++;
                p.Y--;
            }
            if (key == ConsoleKey.Q)
            {
                p.X--;
                p.Y--;
            }
            if (key == ConsoleKey.Z)
            {
                p.X--;
                p.Y++;
            }
            if (key == ConsoleKey.C)
            {
                p.X++;
                p.Y++;
            }
            return p;
        }               
    }
}
