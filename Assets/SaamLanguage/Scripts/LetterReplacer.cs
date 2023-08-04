using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LetterReplacer
{
    private static Dictionary<char, char> replacements = new Dictionary<char, char>()
    {
        { 'A', (char)256 },
        { 'À', (char)256 },
        { 'a', (char)257 },
        { 'à', (char)257 },
        { 'E', (char)274 },
        { 'Å', (char)274 },
        { 'e', (char)275 },
        { 'å', (char)275 },
        { 'O', (char)332 },
        { 'Î', (char)332 },
        { 'o', (char)333 },
        { 'î', (char)333 },
        { 'U', (char)362 },
        { 'u', (char)363 },

    };
    private const char makron = (char)772;

    public static string ReplaceHats(this string text)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i]  == makron)
            {
                if (i == 0)
                    continue;
                char c = text[i - 1];
                if (!replacements.ContainsKey(c))
                    continue;
                builder.Remove(builder.Length - 1, 1);
                builder.Append(replacements[c]);
            } 
            else
            {
                builder.Append(text[i]);
            }
        }
        return builder.ToString();
    }
}
