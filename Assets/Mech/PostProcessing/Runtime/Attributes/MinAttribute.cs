namespace UnityEngine.PostProcessing
{
    public sealed class Min_Attribute : PropertyAttribute
    {
        public readonly float min;

        public Min_Attribute(float min)
        {
            this.min = min;
        }
    }
}
