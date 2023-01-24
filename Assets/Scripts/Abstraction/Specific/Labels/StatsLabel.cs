namespace Scripts.Abstraction
{
    public class StatsLabel : ILabel
    {
        public override bool Equals(object obj)
        {
            return obj is StatsLabel;
        }

        public override int GetHashCode()
        {
            return nameof(StatsLabel).GetHashCode();
        }
    }
}