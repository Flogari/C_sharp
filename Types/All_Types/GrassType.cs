public class GrassType : PokemonType
{
    public GrassType() => Element = ElementType.Grass;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Ground => 2.0f,
            ElementType.Rock => 2.0f,
            ElementType.Water => 2.0f,

            ElementType.Bug => 0.5f,
            ElementType.Dragon => 0.5f,
            ElementType.Fire => 0.5f,
            ElementType.Flying => 0.5f,
            ElementType.Grass => 0.5f,
            ElementType.Poison => 0.5f,
            ElementType.Steel => 0.5f,

            _ => 1.0f
        };
    }
}