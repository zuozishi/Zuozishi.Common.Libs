using System;
using Xunit;
using Zuozishi.Common.Libs.Extensions;

namespace Zuozishi.Common.Libs.Test
{
    public class ExtensionsTest
    {
        [Fact]
        public void 时间戳()
        {
            var time = DateTime.Parse("2021-04-27 09:56:17");
            var secTimestamp = time.GetTimestampSeconds();
            var msecTimestamp = time.GetTimestampMilliseconds();
            Assert.Equal(1619488577L, secTimestamp);
            Assert.Equal(1619488577000L, msecTimestamp);
        }

        [Theory]
        [InlineData(new object[] { new object[] { 2, 4, 6, 8 }, "|", "2|4|6|8" })]
        [InlineData(new object[] { new object[] { "asd", "zxc", "qwe" }, ",", "asd,zxc,qwe" })]
        public void 数组转分隔符字符串(object[] array, string split, string res)
        {
            Assert.Equal(array.ToString(split), res);
        }

        [Theory]
        [InlineData(new object[] { new object[] { 2, 4, 6, 8 }, 6, 2 })]
        [InlineData(new object[] { new object[] { "asd", "zxc", "qwe" }, "asd", 0 })]
        public void 查找元素所在的位置(object[] array, object item, int index)
        {
            int index1 = array.IndexOf(item);
            Assert.Equal(index, index1);
        }

        [Fact]
        public void Json格式化()
        {
            var json = "{\"name\":\"asd\"}";
            json = json.PrettyJson();
            var pts = json.Split('\n');
            Assert.Equal(3, pts.Length);
        }


        [Theory]
        [InlineData(new object[] { 16 })]
        [InlineData(new object[] { 32 })]
        [InlineData(new object[] { 64 })]
        [InlineData(new object[] { 65 })]
        public void GUID(int length)
        {
            string guid = StringExtension.GUID(length);
            Assert.Equal(length, guid.Length);
        }
    }
}
