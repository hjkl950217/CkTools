namespace 语法验证与学习
{
    [AttributeUsage(AttributeTargets.GenericParameter, Inherited = false, AllowMultiple = false)]
    public sealed class GenericParameterAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.ReturnValue, Inherited = false, AllowMultiple = false)]
    public sealed class ReturnValueAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
    public sealed class EventAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
    public sealed class DelegateAttribute : Attribute
    {
    }
}