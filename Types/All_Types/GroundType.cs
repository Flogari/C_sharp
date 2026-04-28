public class GroundType : PokemonType
{
    public GroundType() => Element = ElementType.Ground;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Electric => 2.0f,
            ElementType.Fire => 2.0f,
            ElementType.Poison => 2.0f,
            ElementType.Rock => 2.0f,
            ElementType.Steel => 2.0f,

            ElementType.Bug => 0.5f,
            ElementType.Grass => 0.5f,

            ElementType.Flying => 0.0f,

            _ => 1.0f
        };
    }
}