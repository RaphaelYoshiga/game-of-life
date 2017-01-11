using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace GameOfLife.Kata.UnitTests
{
    [TestFixture]
    public class GameOfLifeShould
    {
        private int _tickCount;
        private CancellationTokenSource _cancellationTokenSource;
        private IEnumerable<Cell> _firstIterationCells;
        private List<Cell> _zeroIterationCells;

        [SetUp]
        public void BeforeEachTest()
        {
            _cancellationTokenSource = new CancellationTokenSource();

        }

        [Test]
        public void GenerateTickZero()
        {
            var gameOfLife = new GameOfLifeSimulation(new InitialSeed());
            gameOfLife.Ticks += ReceiveTicks;

            gameOfLife.StartSimulation(_cancellationTokenSource);

            _tickCount.Should().BeGreaterThan(0);
        }

        [Test]
        public void GenerateMultipleTicks()
        {
            var gameOfLife = new GameOfLifeSimulation(new InitialSeed(new List<Cell>()));
            gameOfLife.Ticks += ReceiveTicks;

            gameOfLife.StartSimulation(_cancellationTokenSource);

            Thread.Sleep(1000);
            _tickCount.Should().BeGreaterThan(10);
        }

        [Test]
        public void HaveDeadCellsConsideringOnlyTwoHaveBeenProvided()
        {
            var initialSeed = new InitialSeed(new List<Cell> { new Cell(1, 1), new Cell(1, 2) });
            var gameOfLife = new GameOfLifeSimulation(initialSeed);
            gameOfLife.Ticks += ReceiveFirstTick;

            gameOfLife.StartSimulation(_cancellationTokenSource);
            Thread.Sleep(1000);

            _zeroIterationCells.All(p => p.IsAlive()).Should().BeTrue();
            _firstIterationCells.All(p => !p.IsAlive()).Should().BeTrue();
        }

        private void ReceiveFirstTick(object sender, TickEventArgs e)
        {
            if (_tickCount == 0)
                _zeroIterationCells = e.Cells.ToList();

            if (_tickCount == 1)
                _firstIterationCells = e.Cells.ToList();

            _tickCount++;

        }

        private void ReceiveTicks(object sender, TickEventArgs e)
        {
            _tickCount++;
        }
    }
}
