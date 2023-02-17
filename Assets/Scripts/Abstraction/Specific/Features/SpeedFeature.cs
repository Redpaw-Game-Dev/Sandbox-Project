namespace Sandbox.Abstraction
{
    public class SpeedFeature : FloatFeature
    {
        public override string Name => "Speed";

        public override bool Equals(object obj)
        {
            return obj is SpeedFeature;
        }

        public override int GetHashCode()
        {
            return nameof(SpeedFeature).GetHashCode();
        }
    }
}