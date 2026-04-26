using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterWrittenSpanTest
{
    [Fact]
    public void 初期状態が空()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        writer.WrittenSpan.Length.ShouldBe(0);
    }

    [Fact]
    public void 複数回書き込み_WrittenSpanが正しい()
    {
        using var writer = new ArrayPoolBufferWriter<int>();

        var span1 = writer.GetSpan(2);
        span1[0] = 1;
        span1[1] = 2;
        writer.Advance(2);

        var span2 = writer.GetSpan(2);
        span2[0] = 3;
        span2[1] = 4;
        writer.Advance(2);

        writer.WrittenSpan.Length.ShouldBe(4);
        writer.WrittenSpan[0].ShouldBe(1);
        writer.WrittenSpan[1].ShouldBe(2);
        writer.WrittenSpan[2].ShouldBe(3);
        writer.WrittenSpan[3].ShouldBe(4);
    }
}
