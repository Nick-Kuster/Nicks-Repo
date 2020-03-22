using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Api.Models
{
    public class Board
    {
        public List<Cell> Cells { get; set; }
        public List<Row> Rows { get; set; }
        public List<Column> Columns { get; set; }
        public List<Square> Squares { get; set; }
    }
}
