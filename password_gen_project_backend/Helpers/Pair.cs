public class Pair<T, U, S>
{
    public Pair()
    {

    }

    public Pair(T first, U second, S third)
    {
        this.First = first;
        this.Second = second;
        this. Third = third;
    }

    public T First { get; set; }
    public U Second { get; set; }
    public S Third { get; set; }
}