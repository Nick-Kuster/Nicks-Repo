using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Models
{
    public class CellContainer : GamePiece
    {
        public List<int> UnavailableNumbers { get; set; } = new List<int>();

        public void ClearUnavailableNumbers() => this.UnavailableNumbers.Clear();
    }
}
