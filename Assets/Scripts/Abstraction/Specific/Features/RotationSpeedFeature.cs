namespace Scripts.Abstraction
{
    public class RotationSpeedFeature : FloatFeature
    {
        public override string Name => "Rotation Speed";

        public override bool Equals(object obj)
        {
            return obj is RotationSpeedFeature;
        }

        public override int GetHashCode()
        {
            return nameof(RotationSpeedFeature).GetHashCode();
        }
    }
}