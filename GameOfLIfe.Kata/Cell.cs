namespace GameOfLife.Kata
{
    public class Cell
    {
        private bool _isAlive = true;

        public Cell(int x, int y)
        {

        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        public void Die()
        {
            _isAlive = false;
        }
    }
}