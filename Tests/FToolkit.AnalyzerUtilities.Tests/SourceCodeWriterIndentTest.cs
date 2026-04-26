using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests;

public sealed class SourceCodeWriterIndentTest
{
    [Fact]
    public void インデント1つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.Indent())
        {
            writer.WriteLine("value2");
        }

        writer.WrittenSpan.ToString()
            .ShouldBe("""
                value1
                    value2

                """);
    }

    [Fact]
    public void インデント2つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.Indent())
        {
            writer.WriteLine("value2");

            using (writer.Indent())
            {
                writer.WriteLine("value3");
            }
        }

        writer.WrittenSpan.ToString()
            .ShouldBe("""
                value1
                    value2
                        value3

                """);
    }

    [Fact]
    public void インデント開始位置が行頭_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();

        Should.Throw<InvalidOperationException>(() => writer.Indent());
    }

    [Fact]
    public void インデント開始位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.Write("value");

        Should.Throw<InvalidOperationException>(() => writer.Indent());
    }

    [Fact]
    public void インデント終了位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        Should.Throw<InvalidOperationException>(() =>
        {
            using (writer.Indent())
            {
                writer.Write("value2");
            }
        });
    }
}
