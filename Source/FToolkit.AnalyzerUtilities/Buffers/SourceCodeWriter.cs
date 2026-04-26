using System.Buffers;
using System.Runtime.CompilerServices;
using FToolkit.AnalyzerUtilities.Extensions;

namespace FToolkit.AnalyzerUtilities.Buffers;

/// <summary>
/// ソースコードを書き込むクラスです。
/// </summary>
public sealed class SourceCodeWriter : IDisposable
{
    const char Space = ' ';
    const int IndentSize = 4;

    readonly ArrayPoolBufferWriter<char> _bufferWriter = new();
    readonly List<char[]> _indentations = [[]];

    int _currentIndentationLevel;

    /// <summary>
    /// 書き込み済みのバッファを取得します。
    /// </summary>
    public ReadOnlySpan<char> WrittenSpan => _bufferWriter.WrittenSpan;

    ReadOnlySpan<char> CurrentIndentation => _indentations[_currentIndentationLevel];

    /// <inheritdoc/>
    public void Dispose()
        => _bufferWriter.Dispose();

    /// <summary>
    /// インデントします。
    /// </summary>
    /// <returns>インデントを戻すための<see cref="IDisposable"/>を実装したオブジェクトを返します。</returns>
    /// <exception cref="InvalidOperationException">現在のソースコード末尾が改行で終わっていない場合にスローします。</exception>
    public SourceCodeWriterIndent Indent()
    {
        InvalidOperationException.ThrowIfNotEndWithNewLine(WrittenSpan);

        IncreaseIndent();
        return new SourceCodeWriterIndent(this);
    }

    /// <summary>
    /// ブロックを書き込みます。
    /// </summary>
    /// <returns>開いているブロックを閉じるための<see cref="IDisposable"/>を実装したオブジェクトを返します。</returns>
    /// <exception cref="InvalidOperationException">現在のソースコード末尾が改行で終わっていない場合にスローします。</exception>
    public SourceCodeWriterBlock WriteBlock()
    {
        InvalidOperationException.ThrowIfNotEndWithNewLine(WrittenSpan);

        WriteLine(Constants.OpenBlock);
        IncreaseIndent();

        return new(this);
    }

    /// <summary>
    /// 改行を書き込みます。
    /// </summary>
    public void WriteLine()
        => _bufferWriter.Write(Constants.DefaultNewLine);

    /// <summary>
    /// 文字列を書き込みます。
    /// </summary>
    /// <param name="text">文字列</param>
    public void WriteLine(ReadOnlySpan<char> text)
    {
        Write(text);
        WriteLine();
    }

    /// <summary>
    /// 文字列を書き込みます。
    /// </summary>
    /// <param name="handler">文字列補間ハンドラー</param>
    public void WriteLine([InterpolatedStringHandlerArgument("")] ref SourceCodeWriterInterpolatedStringHandler handler)
    {
        _ = handler;
        WriteLine();
    }

    /// <summary>
    /// 文字列を書き込みます。
    /// </summary>
    /// <param name="text">文字列</param>
    public void Write(ReadOnlySpan<char> text)
    {
        if (_bufferWriter.WrittenSpan.EndsWith(Constants.DefaultNewLine))
        {
            _bufferWriter.Write(CurrentIndentation);
        }

        _bufferWriter.Write(text);
    }

    /// <summary>
    /// 文字列を書き込みます。
    /// </summary>
    /// <param name="handler">文字列補間ハンドラー</param>
    public void Write([InterpolatedStringHandlerArgument("")] ref SourceCodeWriterInterpolatedStringHandler handler)
    {
        _ = this;
        _ = handler;
    }

    /// <summary>
    /// インデントを増やします。
    /// </summary>
    internal void IncreaseIndent()
    {
        if (++_currentIndentationLevel != _indentations.Count)
        {
            return;
        }

        var newIndentation = new char[_indentations[^1].Length + IndentSize];
        newIndentation.AsSpan().Fill(Space);

        _indentations.Add(newIndentation);
    }

    /// <summary>
    /// インデントを減らします。
    /// </summary>
    internal void DecreaseIndent() => _currentIndentationLevel--;
}
