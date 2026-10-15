namespace DOMTrace.Models
{
    public class ComboBoxOption
    {
        public object Value { get; set; }
        public string Text { get; set; }

        public ComboBoxOption() { }
        public ComboBoxOption(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
