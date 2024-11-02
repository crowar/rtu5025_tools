using System.Text;

namespace RtuCommon.Extensions;

public static class ArrayExtension
{
    public static (T[] Result, int LastIndex) GetSlice<T>(this T[] source, int index, int length)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        
        if (source.Length == 0)
            throw new ArgumentException("Source array is empty", nameof(source));
        
        if (source.Length < index)
            throw new ArgumentException("Source array is too small", nameof(source));
        
        var arr = new T[length];
        
        for (var i = index; i < index+length; i++)
        {
            arr[i-index] = source[i];
        }
        
        return (arr, index + length);
        
     }
    
    public static string ToHexString(this byte[] ba)
    {
        var hex = new StringBuilder(ba.Length * 2);
        foreach (var b in ba)
            hex.Append($"{b:x2} ");
        return hex.ToString();
    }

    public static byte[] CleanAfterZero(this byte[] source)
    {
        return source.TakeWhile(b => b != 0x00).ToArray();
    }

    public static byte[] ExcludeEmpty(this byte[] source)
    {
        //if byte equals 0x00 or 0xff then it is empty
        
        var res = new List<byte>();
        
        foreach (var b in source)
        {
            if (b != 0xff) //b != 0x00 && 
            {
                res.Add(b);
            }
        }
        
        return res.ToArray();
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        while (source.Any())
        {
            yield return source.Take(chunkSize);
            source = source.Skip(chunkSize);
        }
    }
    
}