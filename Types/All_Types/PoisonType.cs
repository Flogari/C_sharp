public class PoisonType : PokemonType
{
    public PoisonType() => Element = ElementType.Poison;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {

            ElementType.Fairy => 2.0f,
            ElementType.Grass => 2.0f,

            ElementType.Ghost => 0.5f,
            ElementType.Ground => 0.5f,
            ElementType.Poison => 0.5f,
            ElementType.Rock => 0.5f,

            ElementType.Steel => 0.0f,

            _ => 1.0f
        };
    }
}