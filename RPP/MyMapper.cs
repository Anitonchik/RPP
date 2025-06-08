namespace RPP;

public class MyMapper
{
    public static To MapObject<To>(object obj, To newObject)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(newObject);

        var typeFrom = obj.GetType();
        var typeTo = newObject.GetType();

        var propertiesFrom = typeFrom.GetProperties().Where(x => x.CanRead).ToArray();
        foreach (var property in typeTo.GetProperties().Where(x => x.CanWrite))
        {
            var propertyFrom = propertiesFrom.FirstOrDefault(x => x.Name == property.Name);
            if (propertyFrom is null)
            {
                continue;
            }
            property.SetValue(newObject, propertyFrom.GetValue(obj)!);
        }
        return newObject;
    }
}
