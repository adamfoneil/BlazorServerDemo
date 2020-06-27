using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace BlazorServerDemo.Services
{
    public class Highligher
    {
        public MarkupString Apply(string content, string searchString, string style = "background-color:yellow")
        {
            if (string.IsNullOrEmpty(searchString)) return new MarkupString(content);

            var words = searchString
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => word)
                .Select(wordGrp => new { word = wordGrp.Key, markup = $"<span style=\"{style}\">{wordGrp.Key}</span>" })
                .ToDictionary(item => item.word, item => item.markup);

            string result = content;

            foreach (var kp in words)
            {
                result = result.Replace(kp.Key, kp.Value, StringComparison.OrdinalIgnoreCase);
            }

            return new MarkupString(result);
        }
    }
}
