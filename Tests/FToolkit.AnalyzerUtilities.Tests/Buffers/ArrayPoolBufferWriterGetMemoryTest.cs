using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterGetMemoryTest
{
    [Fact]
    public void 要求サイズが0_空でないMemoryを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var memory = writer.GetMemory(0);

        memory.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void 要求サイズが1以上_1以上のバッファーを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var memory = writer.GetMemory(100);

        memory.Length.ShouldBeGreaterThanOrEqualTo(100);
    }

    [Fact]
    public void バッファー拡張_元データが保持される()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        var memory = writer.GetMemory(4);
        memory.Span[0] = 1;
        memory.Span[1] = 2;
        writer.Advance(2);

        writer.GetMemory(memory.Length);

        writer.WrittenSpan.Length.ShouldBe(2);
        writer.WrittenSpan[0].ShouldBe((byte)1);
        writer.WrittenSpan[1].ShouldBe((byte)2);
    }

    [Fact]
    public void 要求サイズが負_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        Should.Throw<ArgumentOutOfRangeException>(() => writer.GetMemory(-1));
    }
}
