
public class RandomNumGenerator
{
    
    public RandomNumGenerator()
    {}
    
    public string randNum()
    {
        Random rand = new Random();
        int result = rand.Next();
        
        return result.ToString();
    }
}