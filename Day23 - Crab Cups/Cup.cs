namespace Day23
{
    public record Cup
    {
        public int Number { get; }

        public int Next { get; set; }

        public Cup(int number)
        {
            Number = number;
        }

        public Cup(int number, int next)
        {
            Number = number;
            Next = next;
        }

        public static implicit operator Cup(int number) => new Cup(number);

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}