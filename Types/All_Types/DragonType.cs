public class DragonType : PokemonType
{
    public DragonType() => Element = ElementType.Dragon;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Dragon => 2.0f,

            ElementType.Steel => 0.5f,

            ElementType.Fairy => 0.0f,
            
            _ => 1.0f
        };
    }
}