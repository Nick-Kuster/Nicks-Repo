using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BlazorApp1.Models;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Game game = new Game();
            game.InitGame();
            //foreach (var square in game.Squares)
            //{
            //    var cellsInSquare =  game.Cells.Where(x => x.SquareId == square.Id).ToList();
            //    //var sb = new StringBuilder($"Square {square.Id}: ");
            //    //foreach (var cell in cellsInSquare)
            //    //{
            //    //    sb.Append($"| {cell.ActualValue} | ");
            //    //    if (cell.Id % 3 == 0)
            //    //        sb.Append($"\n");
            //    //}

            //    //Console.WriteLine(sb.ToString());
            //}
            var sb = new StringBuilder("Welcome To Sudoku! \n");
            foreach (var cell in game.Cells)
            {
                sb.Append($"| {cell.ActualValue} | ");
                if (cell.Id % 9 == 0)
                    sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
            Assert.Pass();
        }
        
        [Test]
        public void Test2()
        {
            Game game = new Game();
            var sw = new Stopwatch();
            var totalTime = 0; 
            for (int i = 0; i < 20; i++)
            {
                sw.Start();

                game.InitGame();

                sw.Stop();
                totalTime += sw.Elapsed.Seconds;
            }

            var average = totalTime / 20;
            Console.WriteLine(average);
            //StringBuilder sb = new StringBuilder();
            //foreach (var cell in game.Cells)
            //{
            //    sb.Append($"| {cell.ActualValue} | ");
            //    if (cell.Id % 9 == 0)
            //    {
            //        sb.Append($"\n");
            //    }
            //}

            //Console.WriteLine(sb.ToString());

            Assert.Pass();
        }
    }
}