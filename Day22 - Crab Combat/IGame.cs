namespace Day22
{
    public interface IGame
    {
        public Deck Play();

        public Deck Winner { get; }
    }
}