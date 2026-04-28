public class FairyType : PokemonType
{
    public FairyType() => Element = ElementType.Fairy;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Dark => 2.0f,
            ElementType.Dragon => 2.0f,
            ElementType.Fighting => 2.0f,

            ElementType.Fire => 0.5f,
            ElementType.Poison => 0.5f,
            ElementType.Steel => 0.5f,

            _ => 1.0f
        };
    }
}