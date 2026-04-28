public class IceType : PokemonType
{
    public IceType() => Element = ElementType.Ice;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Dragon => 2.0f,
            ElementType.Flying => 2.0f,
            ElementType.Grass => 2.0f,
            ElementType.Ground => 2.0f,

            ElementType.Fire => 0.5f,
            ElementType.Ice => 0.5f,
            ElementType.Steel => 0.5f,
            ElementType.Water => 0.5f,

            _ => 1.0f
        };
    }
}