public class SteelType : PokemonType
{
    public SteelType() => Element = ElementType.Steel;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {

            ElementType.Fairy => 2.0f,
            ElementType.Ice => 2.0f,
            ElementType.Rock => 2.0f,

            ElementType.Electric => 0.5f,
            ElementType.Fire => 0.5f,
            ElementType.Steel => 0.5f,
            ElementType.Water => 0.5f,

            _ => 1.0f
        };
    }
}