public class ElectricType : PokemonType
{
    public ElectricType() => Element = ElementType.Electric;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Flying => 2.0f,
            ElementType.Water => 2.0f,

            ElementType.Dragon => 0.5f,
            ElementType.Electric => 0.5f,
            ElementType.Grass => 0.5f,

            ElementType.Ground => 0.0f,

            _ => 1.0f
        };
    }
}