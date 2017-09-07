﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Scripts
{
    internal static class Moustache
    {
        private const string MoustacheStartChars = "{{";
        private const string MoustacheStopChars = "}}";


        public static string MoustacheReplace<T>(this string text, IDictionary<string, T> valueCollection, MoustacheNotFoundBehavior notFoundBehavior = MoustacheNotFoundBehavior.Source)
        {
            if (string.IsNullOrEmpty(text)) return text;
            if (valueCollection == null) throw new ArgumentNullException(nameof(valueCollection));

            var read_pos = 0;
            var result = new StringBuilder();
            while (read_pos < text.Length) {
                var x = text.IndexOf(MoustacheStartChars, read_pos, StringComparison.Ordinal);
                if (x < 0) break;

                var y = text.IndexOf(MoustacheStopChars, x, StringComparison.Ordinal);
                if (y < 0) break;

                result.Append(text, read_pos, x - read_pos);
                read_pos = y + MoustacheStopChars.Length;

                var item_key = text.Substring(x + MoustacheStartChars.Length, y - x - MoustacheStartChars.Length);

                if (valueCollection.TryGetValue(item_key, out T item_value)) {
                    result.Append(item_value);
                    continue;
                }

                switch (notFoundBehavior) {
                    case MoustacheNotFoundBehavior.Source:
                        result.Append(text.Substring(x, y - x + MoustacheStartChars.Length));
                        break;
                }
            }

            if (read_pos < text.Length) {
                result.Append(text, read_pos, text.Length - read_pos);
            }

            return result.ToString();
        }
    }

    public enum MoustacheNotFoundBehavior
    {
        Empty,
        Source,
    }
}
