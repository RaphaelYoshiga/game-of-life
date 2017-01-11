using System;
using System.Collections.Generic;

namespace GameOfLife.Kata
{
    public class TickEventArgs : EventArgs
    {
        public TickEventArgs(IEnumerable<Cell> cells)
        {
            Cells = cells;
        }

        public IEnumerable<Cell> Cells { get; }
    }
}