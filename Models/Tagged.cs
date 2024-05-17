namespace magick.Models;

public class Tagged<T>(T instance, Guid? tag)
{
    public Guid Tag { get; } = tag ?? Guid.NewGuid();
    public T Instance { get; } = instance;


    public Tagged(T instance)
        : this(instance, null) { }
    
    public static Tagged<T> FromPair(KeyValuePair<Guid, T> pair)
        => new(pair.Value, pair.Key);
}