using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests;

public sealed class SourceCodeWriterWriteTest
{
    [Fact]
    public void 文字列()
    {
        using var writer = new SourceCodeWriter();
        writer.Write("class A");

        writer.WrittenSpan.ToString()
            .ShouldBe("class A");
    }

    [Fact]
    public void 補間文字列()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"return {1};");

        writer.WrittenSpan.ToString()
            .ShouldBe("return 1;");
    }

    [Fact]
    public void 開始位置が改行以外()
    {
        using var writer = new SourceCodeWriter();

        writer.Write("x:");
        writer.Write("class A");

        writer.WrittenSpan.ToString()
            .ShouldBe("x:class A");
    }
}
