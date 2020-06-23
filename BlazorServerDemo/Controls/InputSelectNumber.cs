using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerDemo.Controls
{
    /// <summary>
    /// help from https://github.com/dotnet/aspnetcore/issues/11181
    /// </summary>    
    public class InputSelectNumber<T> : InputSelect<T>
    {
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                if (string.IsNullOrEmpty(value))
                {
                    result = default;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    if (int.TryParse(value, out int resultInt))
                    {
                        result = (T)(object)resultInt;
                        validationErrorMessage = null;
                        return true;
                    }
                    else
                    {
                        result = default;
                        validationErrorMessage = "The chosen value is not a valid number.";
                        return false;
                    }
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
        }
    }
}
