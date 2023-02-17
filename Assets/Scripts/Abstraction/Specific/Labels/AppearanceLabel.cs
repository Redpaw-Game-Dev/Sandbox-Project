namespace Sandbox.Abstraction
{
    public class AppearanceLabel : ILabel
    {
        public override bool Equals(object obj)
        {
            return obj is AppearanceLabel;
        }

        public override int GetHashCode()
        {
            return nameof(AppearanceLabel).GetHashCode();
        }
    }
}