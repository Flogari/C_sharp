public class FireType : PokemonType
{
    public FireType() => Element = ElementType.Fire;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Bug => 2.0f,
            ElementType.Grass => 2.0f,
            ElementType.Ice => 2.0f,
            ElementType.Steel => 2.0f,

            ElementType.Dragon => 0.5f,
            ElementType.Fire => 0.5f,
            ElementType.Rock => 0.5f,
            ElementType.Water => 0.5f,
            
            _ => 1.0f
        };
    }
}