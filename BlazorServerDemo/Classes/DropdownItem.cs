namespace BlazorServerDemo.Classes
{
    public class DropdownItem<T>
    {
        public DropdownItem(T value, string text)
        {
            Value = value;
            Text = text;
        }

        public T Value { get; }
        public string Text { get; }
    }
}
