using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterGetSpanTest
{
    [Fact]
    public void 要求サイズが0_空でないSpanを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var span = writer.GetSpan(0);

        span.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void 要求サイズが1以上_1以上のバッファーを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var span = writer.GetSpan(100);

        span.Length.ShouldBeGreaterThanOrEqualTo(100);
    }

    [Fact]
    public void バッファー拡張_元データが保持される()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        var span = writer.GetSpan(4);
        span[0] = 1;
        span[1] = 2;
        writer.Advance(2);

        writer.GetSpan(span.Length);

        writer.WrittenSpan.Length.ShouldBe(2);
        writer.WrittenSpan[0].ShouldBe((byte)1);
        writer.WrittenSpan[1].ShouldBe((byte)2);
    }

    [Fact]
    public void 要求サイズが負_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        Should.Throw<ArgumentOutOfRangeException>(() => writer.GetSpan(-1));
    }
}
