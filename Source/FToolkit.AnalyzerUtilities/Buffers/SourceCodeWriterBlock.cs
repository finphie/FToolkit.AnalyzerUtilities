using FToolkit.AnalyzerUtilities.Extensions;

namespace FToolkit.AnalyzerUtilities.Buffers;

/// <summary>
/// ソースコードのブロックを表します。
/// </summary>
public readonly ref struct SourceCodeWriterBlock : IDisposable
{
    readonly SourceCodeWriter _writer;

    /// <summary>
    /// <see cref="SourceCodeWriterBlock"/>構造体の新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="writer">ソースコードを書き込むクラスのインスタンス</param>
    /// <exception cref="ArgumentNullException"><paramref name="writer"/>が<see langword="null"/>です。</exception>
    internal SourceCodeWriterBlock(SourceCodeWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        _writer = writer;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        InvalidOperationException.ThrowIfNotEndWithNewLine(_writer.WrittenSpan);

        _writer.DecreaseIndent();
        _writer.WriteLine(Constants.CloseBlock);
    }
}
