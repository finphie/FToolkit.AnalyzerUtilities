using System.Buffers;
using System.Runtime.CompilerServices;
using FToolkit.AnalyzerUtilities.Extensions;

namespace FToolkit.AnalyzerUtilities.Buffers;

/// <summary>
/// ソースコードを書き込むクラスです。
/// </summary>
public sealed class SourceCodeWriter : IDisposable
{
    const int IndentSize = 4;
    const char Space = ' ';

    readonly ArrayPoolBufferWriter<char> _bufferWriter = new();

    char[] _indentBuffer = new char[IndentSize * 5];
    int _currentIndentationLevel;

    /// <summary>
    /// <see cref="SourceCodeWriter"/>クラスの新しいインスタンスを初期化します。
    /// </summary>
    public SourceCodeWriter()
        => Array.Fill(_indentBuffer, Space);

    /// <summary>
    /// 書き込み済みのバッファを取得します。
    /// </summary>
    public ReadOnlySpan<char> WrittenSpan => _bufferWriter.WrittenSpan;

    ReadOnlySpan<char> CurrentIndentation
    {
        get
        {
            var length = _currentIndentationLevel * IndentSize;
            return _indentBuffer.AsSpan(0, length);
        }
    }

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
        var needed = ++_currentIndentationLevel * IndentSize;

        if (_indentBuffer.Length >= needed)
        {
            return;
        }

        var oldLength = _indentBuffer.Length;
        var newSize = Math.Max(oldLength * 2, needed);
        Array.Resize(ref _indentBuffer, newSize);

        Array.Fill(_indentBuffer, Space, oldLength, _indentBuffer.Length - oldLength);
    }

    /// <summary>
    /// インデントを減らします。
    /// </summary>
    internal void DecreaseIndent() => _currentIndentationLevel--;
}
