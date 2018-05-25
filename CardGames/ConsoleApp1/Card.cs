using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Card
    {
        public Face Face { get; set; }
        public Color Color { get; set; }

        public Card(Face face, Color color)
        {
            Face = face;
            Color = color;
        }
    }
}
