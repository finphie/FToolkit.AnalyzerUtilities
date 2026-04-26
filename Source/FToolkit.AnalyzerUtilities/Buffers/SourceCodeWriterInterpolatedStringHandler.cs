using System.Globalization;
using System.Runtime.CompilerServices;

namespace FToolkit.AnalyzerUtilities.Buffers;

/// <summary>
/// <see cref="SourceCodeWriter"/>で使用する文字列補間ハンドラーです。
/// </summary>
[InterpolatedStringHandler]
public readonly ref struct SourceCodeWriterInterpolatedStringHandler
{
    readonly SourceCodeWriter _writer;

    /// <summary>
    /// <see cref="SourceCodeWriterInterpolatedStringHandler"/>の新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="literalLength">定数文字の数</param>
    /// <param name="formattedCount">補間式の数</param>
    /// <param name="writer">ソースコードを書き込むクラスのインスタンス</param>
    public SourceCodeWriterInterpolatedStringHandler(int literalLength, int formattedCount, SourceCodeWriter writer)
    {
        _ = literalLength;
        _ = formattedCount;
        _writer = writer;
    }

    /// <summary>
    /// 文字列を追加します。
    /// </summary>
    /// <param name="value">文字列</param>
    public void AppendLiteral(string value)
        => _writer.Write(value);

    /// <summary>
    /// 値を文字列としてを追加します。
    /// </summary>
    /// <typeparam name="T">値の型</typeparam>
    /// <param name="value">値</param>
    public void AppendFormatted<T>(T value)
    {
        if (value is IFormattable formattableValue)
        {
            AppendLiteral(formattableValue.ToString(null, CultureInfo.InvariantCulture));
            return;
        }

        AppendLiteral(value?.ToString()!);
    }
}
