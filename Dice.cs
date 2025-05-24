public class Dice
{
    public List<int> Faces { get; }

    public Dice(IEnumerable<int> faces)
    {
        if (faces == null)
            throw new ArgumentNullException(nameof(faces),
                "Dice input is null. Example of correct format: 2,2,4,4,9,9");

        Faces = faces.ToList();

        if (Faces.Count == 0)
            throw new ArgumentException(
                "Dice must have at least one face. Example: 2,2,4,4,9,9");

        if (Faces.Count < 6)
            throw new ArgumentException(
                "Each dice must have at least 6 faces. Example: 2,2,4,4,9,9");

        if (Faces.Any(f => f < 0))
            throw new ArgumentException(
                "Negative face values are not supported. Use only non-negative integers. Example: 1,3,5,7,9,11");       
    }

    public int Roll(int index)
    {
        if (index < 0 || index >= Faces.Count)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"Index must be in range [0..{Faces.Count - 1}].");

        return Faces[index];
    }

    public override string ToString()
    {
        return string.Join(",", Faces);
    }
}


