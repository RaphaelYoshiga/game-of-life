using System.Collections.Generic;

namespace GameOfLife.Kata
{
    public class InitialSeed
    {
        public InitialSeed(IEnumerable<Cell> cells = null)
        {
            Cells = cells;
        }

        public IEnumerable<Cell> Cells { get; }
    }
}