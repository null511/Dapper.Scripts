using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dapper.Scripts.Text
{
    internal static class SqlDebugPrinter
    {
        private static readonly Type[] stringTypes = {
            typeof(char),
            typeof(string),
            typeof(DateTime),
        };


        public static string GetDebugString(string sql, object parameters)
        {
            var _text = sql;
            var _params = GetParameterCollection(parameters);

            foreach (var paramName in _params.Keys) {
                var paramValue = _params[paramName];
                var displayValue = GetParameterDisplayValue(paramValue);

                var eParamName = Regex.Escape(paramName);
                var pattern = $"[^\\w]({eParamName})[^\\w]";
                var matches = Regex.Matches(_text, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                for (var i = matches.Count-1; i >= 0; i--) {
                    var match = matches[i];

                    var g = match.Groups[1];

                    _text = ReplaceTextSegment(_text, g.Index, g.Length, displayValue);
                }
            }

            return _text;
        }

        public static IDictionary<string, object> GetParameterCollection(object parameters)
        {
            if (parameters == null) return null;

            if (parameters is IDictionary<string, object> paramDictionary)
                return paramDictionary;

            var paramType = parameters.GetType();
            var paramList = paramType.GetProperties();
            paramDictionary = new Dictionary<string, object>();

            foreach (var param in paramList) {
                var paramValue = param.GetValue(parameters);
                paramDictionary[param.Name] = paramValue;
            }

            return paramDictionary;
        }

        private static string ReplaceTextSegment(string text, int start, int length, string replacementText)
        {
            var text_pre = text.Substring(0, start);
            var text_post = text.Substring(start + length);
            return string.Concat(text_pre, replacementText, text_post);
        }

        private static string GetParameterDisplayValue(object value)
        {
            if (value == null) return "null";

            var paramType = value.GetType();

            if (paramType == typeof(DateTime))
                return $"'{(DateTime)value:yyyy-MM-dd HH:mm:ss.fff}'";

            if (stringTypes.Contains(paramType))
                return $"'{value}'";

            if (value is IEnumerable collection) {
                var values = collection.OfType<object>()
                    .Select(GetParameterDisplayValue);

                return $"({string.Join(", ", values)})";
            }

            return value.ToString();
        }
    }
}
