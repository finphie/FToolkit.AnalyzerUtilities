using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

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

    [Fact]
    public void IFormattable以外()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"{(1, 2)}");

        writer.WrittenSpan.ToString()
            .ShouldBe("(1, 2)");
    }

    [Fact]
    public void Null()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"{(string?)null}");

        writer.WrittenSpan.ToString()
            .ShouldBeEmpty();
    }

    [Fact]
    public void 空文字()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"{string.Empty}");

        writer.WrittenSpan.ToString()
            .ShouldBeEmpty();
    }
}
