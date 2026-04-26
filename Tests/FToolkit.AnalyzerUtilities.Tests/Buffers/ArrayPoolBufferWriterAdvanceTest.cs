using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterAdvanceTest
{
    [Fact]
    public void WrittenSpanが更新される()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var span = writer.GetSpan(4);

        span[0] = 1;
        span[1] = 2;
        span[2] = 3;

        writer.Advance(3);

        writer.WrittenSpan.Length.ShouldBe(3);
        writer.WrittenSpan[0].ShouldBe((byte)1);
        writer.WrittenSpan[1].ShouldBe((byte)2);
        writer.WrittenSpan[2].ShouldBe((byte)3);
    }

    [Fact]
    public void 書き込まれたデータ数が負_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        Should.Throw<ArgumentOutOfRangeException>(() => writer.Advance(-1));
    }

    [Fact]
    public void 書き込まれたデータ数がバッファーサイズを超える_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var length = writer.GetSpan().Length;

        Should.Throw<ArgumentOutOfRangeException>(() => writer.Advance(length + 1));
    }
}
