using FToolkit.AnalyzerUtilities.Buffers;
using Shouldly;
using Xunit;

namespace FToolkit.AnalyzerUtilities.Tests;

public sealed class SourceCodeWriterWriteBlockTest
{
    [Fact]
    public void ブロック1つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.WriteBlock())
        {
            writer.WriteLine("value2");
        }

        writer.WrittenSpan.ToString()
            .ShouldBe("""
                value1
                {
                    value2
                }

                """);
    }

    [Fact]
    public void ブロック2つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.WriteBlock())
        {
            writer.WriteLine("value2");

            using (writer.WriteBlock())
            {
                writer.WriteLine("value3");
            }
        }

        writer.WrittenSpan.ToString()
            .ShouldBe("""
                value1
                {
                    value2
                    {
                        value3
                    }
                }

                """);
    }

    [Fact]
    public void ブロック開始位置が行頭_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();

        Should.Throw<InvalidOperationException>(() => writer.WriteBlock());
    }

    [Fact]
    public void ブロック開始位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.Write("value");

        Should.Throw<InvalidOperationException>(() => writer.WriteBlock());
    }

    [Fact]
    public void ブロック終了位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        Should.Throw<InvalidOperationException>(() =>
        {
            using (writer.WriteBlock())
            {
                writer.Write("value2");
            }
        });
    }
}
