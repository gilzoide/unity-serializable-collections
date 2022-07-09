namespace Gilzoide.SerializableCollections
{
    ///<summary>
    /// Implementations are expected to have exactly 1 visible field.
    /// This single field will be drawn in place of the whole struct/class,
    /// removing the need to expand/collapse the title just for a single field.
    /// </summary>
    public interface ISingleFieldDrawable
    {
    }

    /// <summary>
    /// Implementations are expected to have exactly 2 visible fields.
    /// The first field will be used as Key, the second one as Value.
    /// </summary>
    public interface IKeyValuePairDrawable
    {
    }
}
