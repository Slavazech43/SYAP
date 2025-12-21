using System.Reflection;

namespace Domain.Common;

public abstract record Enumeration<TEnum>
    where TEnum : Enumeration<TEnum>
{
    public int Key { get; }
    public string Name { get; }

    protected Enumeration(int key, string name)
    {
        Key = key;
        Name = name;
    }

    // Ленивая инициализация фабрик (сканирование сборки один раз на TEnum)
    private static readonly Lazy<Factories> _factories = new Lazy<Factories>(ScanEnumerations);

    public static TEnum FromKey(int key)
    {
        var f = _factories.Value;

        if (!f.ByKey.TryGetValue(key, out var factory))
            throw new ArgumentException($"Неизвестный ключ перечисления {typeof(TEnum).Name}: {key}");

        return factory();
    }

    public static TEnum FromName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"Name пустой для перечисления {typeof(TEnum).Name}.");

        var n = name.Trim();

        var f = _factories.Value;

        if (!f.ByName.TryGetValue(n, out var factory))
            throw new ArgumentException($"Неизвестное имя перечисления {typeof(TEnum).Name}: {n}");

        return factory();
    }

    public static IReadOnlyList<TEnum> List()
        => _factories.Value.All;

    public override string ToString() => Name;

    private sealed class Factories
    {
        public Dictionary<int, Func<TEnum>> ByKey { get; } = new();
        public Dictionary<string, Func<TEnum>> ByName { get; } = new(StringComparer.OrdinalIgnoreCase);
        public List<TEnum> All { get; } = new();
    }

    private static Factories ScanEnumerations()
    {
        var result = new Factories();

        var enumType = typeof(TEnum);
        var asm = enumType.Assembly;

        // Ищем все конкретные наследники TEnum в этой же сборке
        var types = asm
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && enumType.IsAssignableFrom(t))
            .ToArray();

        for (int i = 0; i < types.Length; i++)
        {
            var t = types[i];

            // По методичке: конструктор без параметров
            var ctor = t.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: Type.EmptyTypes,
                modifiers: null);

            if (ctor == null)
                continue;

            Func<TEnum> factory = () => (TEnum)ctor.Invoke(null);

            // Создаём один экземпляр, чтобы узнать Key/Name
            var instance = factory();

            if (result.ByKey.ContainsKey(instance.Key))
                throw new InvalidOperationException($"Дубликат Key={instance.Key} в {enumType.Name}.");

            if (result.ByName.ContainsKey(instance.Name))
                throw new InvalidOperationException($"Дубликат Name='{instance.Name}' в {enumType.Name}.");

            result.ByKey[instance.Key] = factory;
            result.ByName[instance.Name] = factory;
            result.All.Add(instance);
        }

        // Удобно: сортируем по ключу
        result.All.Sort((a, b) => a.Key.CompareTo(b.Key));

        return result;
    }
}
