using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Api.Models
{
    public class Cell : GamePiece
    {
        public int ActualValue { get; set; }
        public int UserValue { get; set; }
        public int ColumnId { get; set; }
        public int RowId { get; set; }
        public int SquareId { get; set; }
    }
}
