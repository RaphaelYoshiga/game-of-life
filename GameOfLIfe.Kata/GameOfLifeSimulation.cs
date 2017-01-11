using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Kata
{
    public class GameOfLifeSimulation
    {
        private IEnumerable<Cell> _cells;
        public EventHandler<TickEventArgs> Ticks;

        public GameOfLifeSimulation(InitialSeed initialSeed)
        {
            _cells = initialSeed.Cells;
        }

        public void StartSimulation(CancellationTokenSource cancellationToken)
        {
            Task.Factory.StartNew(Simulation(cancellationToken), TaskCreationOptions.LongRunning);

        }

        private Action Simulation(CancellationTokenSource cancellationToken)
        {
            return () =>
            {
                int count = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cells = count == 0 ? _cells : IterateTick();
                    Ticks?.Invoke(this, new TickEventArgs(cells));
                    count++;
                }
            };
        }

        private IEnumerable<Cell> IterateTick()
        {
            foreach (var cell in _cells)
                cell.Die();

            return _cells;
        }
    }
}