using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Api.Models
{
    public class Game
    {
        public Board Board { get; set; }

        public void InitGame()
        {
            BuildBoard();

            PopulateBoard();

            //this.FillCellsWithValues();
            BuildRows();
        }

        private void BuildBoard()
        {
            Board = new Board
            {
                Cells = BuildGamePieces<Cell>(81),
                Rows = BuildGamePieces<Row>(9),
                Columns = BuildGamePieces<Column>(9),
                Squares = BuildGamePieces<Square>(9)
            };
        }

        private List<T> BuildGamePieces<T>(int quantity) where T : GamePiece, new()
        {
            var pieces = new T[quantity];

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = new T();
                pieces[i].Id = i + 1;
            }

            return pieces.ToList();
        }

        private void PopulateBoard()
        {
            var currentRow = 1;
            var currentColumn = 1;
            var currentSquare = 1;

            foreach (var cell in Board.Cells)
            {
                cell.RowId = currentRow;
                cell.ColumnId = currentColumn;
                cell.SquareId = currentSquare;
                cell.ActualValue = 0;
                currentColumn++;

                if (cell.Id % 3 == 0)
                {
                    currentSquare++;
                }

                if (cell.Id % 27 == 0)
                {
                    currentRow++;
                    currentColumn = 1;
                    continue;
                }

                if (cell.Id % 9 == 0)
                {
                    currentSquare = currentSquare - 3;
                    currentRow++;
                    currentColumn = 1;
                }
            }
        }
        private void FillCellsWithValues()
        {
            int iterations = 0;
            for (int i = 0; i < Board.Cells.Count; i++)
            {
                var cell = Board.Cells[i];
                var masterList = new List<int>();
                var rowUnavailableNumbers = Board.Rows.FirstOrDefault(x => x.Id == cell.RowId)?.UnavailableNumbers ?? new List<int>();
                var columnUnavailableNumbers = Board.Columns.FirstOrDefault(x => x.Id == cell.ColumnId)?.UnavailableNumbers ?? new List<int>();
                var squareUnavailableNumbers = Board.Squares.FirstOrDefault(x => x.Id == cell.SquareId)?.UnavailableNumbers ?? new List<int>();
                var listOfUnavailableNumbers = rowUnavailableNumbers
                                                .Concat(columnUnavailableNumbers)
                                                .Concat(squareUnavailableNumbers)
                                                .Distinct()
                                                .ToList();
                var numberList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                var availableNumbers = numberList.Except(listOfUnavailableNumbers).ToList();
                var random = new Random();

                var randomIndex = random.Next(availableNumbers.Count);
                int actualValue;
                try
                {
                    actualValue = availableNumbers[randomIndex];
                }
                catch (Exception e)
                {
                    var row = Board.Rows.FirstOrDefault(x => x.Id == cell.RowId);
                    row.ClearUnavailableNumbers();
                    //i = -1;
                    //iterations++;
                    cell.ActualValue = 0;
                    continue;
                }
                cell.ActualValue = actualValue;
                rowUnavailableNumbers?.Add(actualValue);
                columnUnavailableNumbers?.Add(actualValue);
                squareUnavailableNumbers?.Add(actualValue);
            }
            Console.WriteLine(iterations + " iterations");
        }
        private void FillCellsWithValues(List<Cell> Cells)
        {
            foreach (var cell in Cells)
            {
                var rowUnavailableNumbers =
                    Board.Rows.FirstOrDefault(x => x.Id == cell.RowId)?.UnavailableNumbers ?? new List<int>();
                var columnUnavailableNumbers =
                    Board.Columns.FirstOrDefault(x => x.Id == cell.ColumnId)?.UnavailableNumbers ??
                    new List<int>();
                var squareUnavailableNumbers =
                    Board.Squares.FirstOrDefault(x => x.Id == cell.SquareId)?.UnavailableNumbers ??
                    new List<int>();
                var listOfUnavailableNumbers = rowUnavailableNumbers
                    .Concat(columnUnavailableNumbers)
                    .Concat(squareUnavailableNumbers)
                    .Distinct()
                    .ToList();
                var numberList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                var availableNumbers = numberList.Except(listOfUnavailableNumbers).ToList();
                var random = new Random();

                var randomIndex = random.Next(availableNumbers.Count);
                int actualValue;

                actualValue = availableNumbers[randomIndex];

                cell.ActualValue = actualValue;
                rowUnavailableNumbers?.Add(actualValue);
                columnUnavailableNumbers?.Add(actualValue);
                squareUnavailableNumbers?.Add(actualValue);
            }
        }

        private void BuildRows()
        {
            for (int i = 0; i < Board.Rows.Count; i++)
            {
                var row = Board.Rows[i];
                var startingCellIndex = 9 * i;
                var cells = Board.Cells.GetRange(startingCellIndex, 9);
                try
                {
                    FillCellsWithValues(cells);
                }
                catch (Exception e)
                {
                    ResetRow(cells);
                    i--;
                }
            }
        }

        private void ResetRow(List<Cell> row)
        {
            foreach (var cell in row)
            {
                var rowUnavailableNumbers = Board.Rows.FirstOrDefault(x => x.Id == cell.RowId)?.UnavailableNumbers ?? new List<int>();
                var columnUnavailableNumbers = Board.Columns.FirstOrDefault(x => x.Id == cell.ColumnId)?.UnavailableNumbers ?? new List<int>();
                var squareUnavailableNumbers = Board.Squares.FirstOrDefault(x => x.Id == cell.SquareId)?.UnavailableNumbers ?? new List<int>();

                rowUnavailableNumbers.Remove(cell.ActualValue);
                columnUnavailableNumbers.Remove(cell.ActualValue);
                squareUnavailableNumbers.Remove(cell.ActualValue);

                cell.ActualValue = 0;
            }
        }
        private void ClearBoard()
        {
            foreach (var cell in Board.Cells)
                cell.ActualValue = 0;
            this.ClearCellContainers(Board.Rows);
            this.ClearCellContainers(Board.Columns);
            this.ClearCellContainers(Board.Squares);
        }

        private void ClearCellContainers<T>(List<T> cellContainers) where T : CellContainer
        {
            foreach (var c in cellContainers)
                c.ClearUnavailableNumbers();
        }

        private void PrintBoardToConsole()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cell in Board.Cells)
            {
                sb.Append($"| {cell.ActualValue} | ");
                if (cell.Id % 9 == 0)
                {
                    sb.Append($"\n");
                }
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
