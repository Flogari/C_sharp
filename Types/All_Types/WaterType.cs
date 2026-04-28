public class WaterType : PokemonType
{
    public WaterType() => Element = ElementType.Water;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {

            ElementType.Fire => 2.0f,
            ElementType.Ground => 2.0f,
            ElementType.Rock => 2.0f,

            ElementType.Dragon => 0.5f,
            ElementType.Grass => 0.5f,
            ElementType.Water => 0.5f,

            _ => 1.0f
        };
    }
}