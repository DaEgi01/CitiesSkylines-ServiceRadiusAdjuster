using ServiceRadiusAdjuster;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ServiceRadiusAdjusterTests;

public class DictionaryExtensionsTests
{
    [Fact]
    public void DictionaryCombine_New()
    {
        var aDic = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f }
        };
        var bDic = new Dictionary<string, float>()
        {
            { "test4", 4.0f },
            { "test5", 5.0f },
            { "test6", 6.0f }

        };
        var expected = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f },
            { "test4", 4.0f },
            { "test5", 5.0f },
            { "test6", 6.0f }
        };

        var actual = aDic.CombineAndUpdate(bDic);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void DictionaryCombine_Overlap()
    {
        var aDic = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f }
        };
        var bDic = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f }

        };
        var expected = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f }
        };

        var actual = aDic.CombineAndUpdate(bDic);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void DictionaryCombine_UpdateOverlap()
    {
        var aDic = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f }
        };
        var bDic = new Dictionary<string, float>()
        {
            { "test1", 5.0f },
            { "test2", 6.0f },
            { "test3", 7.0f }
        };
        var expected = new Dictionary<string, float>()
        {
            { "test1", 5.0f },
            { "test2", 6.0f },
            { "test3", 7.0f }
        };

        var actual = aDic.CombineAndUpdate(bDic);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void DictionaryCombine_New_Overlap_UpdateOverlap()
    {
        var aDic = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 3.0f }
        };
        var bDic = new Dictionary<string, float>()
        {
            { "test2", 2.0f },
            { "test3", 4.0f },
            { "test4", 5.0f }
        };
        var expected = new Dictionary<string, float>()
        {
            { "test1", 1.0f },
            { "test2", 2.0f },
            { "test3", 4.0f },
            { "test4", 5.0f }
        };

        var actual = aDic.CombineAndUpdate(bDic);

        actual.Should().BeEquivalentTo(expected);
    }
}