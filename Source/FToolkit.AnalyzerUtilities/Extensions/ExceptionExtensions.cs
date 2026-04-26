namespace FToolkit.AnalyzerUtilities.Extensions;

/// <summary>
/// 例外に関する拡張メソッドを定義したクラスです。
/// </summary>
static class ExceptionExtensions
{
    extension(InvalidOperationException)
    {
        /// <summary>
        /// 指定された文字列が改行で終わっていない場合に例外をスローします。
        /// </summary>
        /// <param name="span">文字列</param>
        /// <exception cref="InvalidOperationException">指定された文字列が改行で終わっていない場合にスローします。</exception>
        public static void ThrowIfNotEndWithNewLine(ReadOnlySpan<char> span)
        {
            if (span.EndsWith(Constants.DefaultNewLine))
            {
                return;
            }

            throw new InvalidOperationException("文字列が改行で終わっていません。");
        }
    }
}
