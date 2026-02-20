using System;
using System.Text;
using System.Text.RegularExpressions;

namespace LMSBackend.Infrastructure.Tests.Helpers
{
    public static class SqlFormattingExtensions
    {
        // =============================================================
        // Parameters
        // =============================================================
        public static string NormalizeEfParameters(this string sql)
        {
            return Regex.Replace(
                sql,
                @"@__(?<name>[a-zA-Z0-9_]+?)_\d+",
                match => "@" + ToPascalCase(match.Groups["name"].Value));
        }

        private static string ToPascalCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var parts = Regex.Split(value, @"[_\s]+");
            var sb = new StringBuilder();

            foreach (var part in parts)
            {
                if (part.Length == 0) continue;

                sb.Append(char.ToUpperInvariant(part[0]));
                if (part.Length > 1)
                    sb.Append(part.Substring(1));
            }

            return sb.ToString();
        }

        // =============================================================
        // Cleanup
        // =============================================================
        public static string CleanBrackets(this string sql)
        {
            return sql.Replace("[", "").Replace("]", "");
        }

        // =============================================================
        // SQL keyword formatting (tree-style)
        // =============================================================
        public static string FormatSqlKeywords(this string sql)
        {
            return sql
                .Replace(" FROM ", "\nFROM ")
                .Replace(" LEFT JOIN ", "\n    LEFT JOIN ")
                .Replace(" INNER JOIN ", "\n    INNER JOIN ")
                .Replace(" RIGHT JOIN ", "\n    RIGHT JOIN ")
                .Replace(" FULL JOIN ", "\n    FULL JOIN ")
                .Replace(" ON ", "\n        ON ")
                .Replace(" WHERE ", "\nWHERE\n    ")
                .Replace(" AND ", "\n    AND ")
                .Replace(" OR ", "\n    OR ")
                .Replace(" ORDER BY ", "\nORDER BY\n    ")
                .Replace(" GROUP BY ", "\nGROUP BY\n    ");
        }

        // =============================================================
        // SELECT column formatting
        // =============================================================
        public static string FormatSelectColumns(this string sql)
        {
            sql = Regex.Replace(sql, @"[ \t]+", " ").Trim();

            var selectIndex = IndexOfWord(sql, "SELECT", 0);
            if (selectIndex < 0) return sql;

            var fromIndex = FindTopLevelFromIndex(sql, selectIndex);
            if (fromIndex < 0) return sql;

            var selectBodyStart = selectIndex + "SELECT".Length;
            var selectHeader = sql.Substring(selectIndex, selectBodyStart - selectIndex).Trim();

            var selectBody = sql.Substring(selectBodyStart, fromIndex - selectBodyStart).Trim();
            var fromBody = sql.Substring(fromIndex + "FROM".Length).Trim();

            var sb = new StringBuilder();

            // ✅ KEEP SELECT / TOP TOGETHER
            sb.AppendLine(selectHeader);

            int depth = 0;
            var current = new StringBuilder();

            foreach (var c in selectBody)
            {
                if (c == '(') depth++;
                if (c == ')') depth--;

                if (c == ',' && depth == 0)
                {
                    sb.AppendLine("    " + current.ToString().Trim() + ",");
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            if (current.Length > 0)
                sb.AppendLine("    " + current.ToString().Trim());

            sb.AppendLine("FROM");
            sb.AppendLine(fromBody);

            return sb.ToString();
        }


        // =============================================================
        // CASE formatting
        // =============================================================
        public static string FormatCaseExpressions(this string sql)
        {
            return sql
                .Replace(" CASE ", "\n    CASE\n        ")
                .Replace(" WHEN ", "\n        WHEN ")
                .Replace(" THEN ", "\n            THEN ")
                .Replace(" ELSE ", "\n            ELSE ")
                .Replace(" END ", "\n    END ");
        }

        // =============================================================
        // Parenthesis nesting (UP TO 4 LEVELS)
        // =============================================================
        public static string FormatNestedParentheses(
            this string sql,
            int maxDepth = 4,
            int indentSize = 4)
        {
            var sb = new StringBuilder();
            int depth = 0;
            bool atLineStart = true;

            for (int i = 0; i < sql.Length; i++)
            {
                // Detect "( SELECT" (subquery start)
                if (sql[i] == '(' && IsSubqueryStart(sql, i))
                {
                    sb.AppendLine();
                    sb.Append(new string(' ', Math.Min(depth, maxDepth) * indentSize));
                    sb.Append("(");

                    depth++;
                    sb.AppendLine();
                    atLineStart = true;

                    continue;
                }

                // Detect ")" that closes a subquery
                if (sql[i] == ')' && depth > 0)
                {
                    depth--;

                    sb.AppendLine();
                    sb.Append(new string(' ', Math.Min(depth, maxDepth) * indentSize));
                    sb.Append(")");

                    atLineStart = false;
                    continue;
                }

                if (atLineStart && !char.IsWhiteSpace(sql[i]))
                {
                    sb.Append(new string(' ', Math.Min(depth, maxDepth) * indentSize));
                    atLineStart = false;
                }

                sb.Append(sql[i]);

                if (sql[i] == '\n')
                    atLineStart = true;
            }

            return sb.ToString();
        }

        private static bool IsSubqueryStart(string sql, int index)
        {
            // Look ahead for "( SELECT"
            var lookahead = sql.Substring(index)
                .TrimStart('(', ' ', '\t', '\r', '\n');

            return lookahead.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);
        }

        // =============================================================
        // Nested SQL keyword cleanup (inside subqueries)
        // =============================================================
        public static string FormatNestedSqlKeywords(this string sql)
        {
            return sql
                .Replace(" SELECT ", "\nSELECT ")
                .Replace(" FROM ", "\nFROM ")
                .Replace(" WHERE ", "\nWHERE ")
                .Replace(" ORDER BY ", "\nORDER BY ")
                .Replace(" GROUP BY ", "\nGROUP BY ")
                .Replace(" AND ", "\nAND ")
                .Replace(" OR ", "\nOR ");
        }

        // =============================================================
        // TOP(n) normalization
        // =============================================================
        public static string NormalizeTopClause(this string sql)
        {
            return Regex.Replace(
                sql,
                @"TOP\s*\(\s*(\d+)\s*[\r\n]*\)",
                m => $"TOP({m.Groups[1].Value})",
                RegexOptions.IgnoreCase
            );
        }

        // =============================================================
        // Stored procedure wrapper
        // =============================================================
        public static string WrapAsStoredProcedure(
            this string sql,
            string schema,
            string procedureName,
            string parameters)
        {
            return $"""
            =============================================================
            -- STORED PROCEDURE TEMPLATE
            =============================================================

            CREATE OR ALTER PROCEDURE {schema}.{procedureName}
                {parameters}
            AS
            BEGIN
                SET NOCOUNT ON;

                {sql}

            END
            GO
            =============================================================
            """;
        }

        // =============================================================
        // Helpers
        // =============================================================
        private static int FindTopLevelFromIndex(string sql, int startIndex)
        {
            int depth = 0;

            for (int i = startIndex; i < sql.Length - 4; i++)
            {
                if (sql[i] == '(') depth++;
                else if (sql[i] == ')') depth--;

                if (depth == 0 && IsWordAt(sql, i, "FROM"))
                    return i;
            }

            return -1;
        }

        private static int IndexOfWord(string s, string word, int startIndex)
        {
            for (int i = startIndex; i <= s.Length - word.Length; i++)
            {
                if (IsWordAt(s, i, word))
                    return i;
            }
            return -1;
        }

        private static bool IsWordAt(string s, int index, string word)
        {
            if (index < 0 || index + word.Length > s.Length)
                return false;

            if (!s.AsSpan(index, word.Length)
                  .Equals(word, StringComparison.OrdinalIgnoreCase))
                return false;

            bool leftOk = index == 0 || !char.IsLetterOrDigit(s[index - 1]);
            bool rightOk = index + word.Length >= s.Length ||
                           !char.IsLetterOrDigit(s[index + word.Length]);

            return leftOk && rightOk;
        }
    }
}
