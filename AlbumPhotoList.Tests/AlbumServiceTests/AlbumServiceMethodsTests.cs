using System;
using Xunit;
using AlbumPhotoList;
using AlbumServices;

namespace AlbumPhotoList.Tests
{
    public class AlbumServiceMethodsTests
    {

        [Fact]
        public void TestDisplayUserMessage_Empty()
        {
            Assert.Contains( "[Enter]", AlbumServiceMethods.GetUserMessage("empty"));
        }

        [Fact]
        public void TestIsNumberInRange_20()
        {
            var result = AlbumServiceMethods.IsNumberInRange("20");
            Assert.True(result, "20 is in range");
        }

        [Fact]
        public void TestIsNumberInRange_200()
        {
            var result = AlbumServiceMethods.IsNumberInRange("200");
            Assert.False(result, "200 is not in range");
        }

        [Fact]
        public void TestIsNumberInRange_XYZ()
        {
            var result = AlbumServiceMethods.IsNumberInRange("XYZ");
            Assert.False(result, "XYZ is not a number");
        }

        [Fact]
        public void TestIsNumberInRange_Symbol()
        {
            var result = AlbumServiceMethods.IsNumberInRange("!");
            Assert.False(result, "! is not a number");
        }

        [Fact]
        public void TestIsNumberInRange_EmptyString()
        {
            var result = AlbumServiceMethods.IsNumberInRange(string.Empty);
            Assert.False(result, "Empty.String is not in range");
        }

        [Fact]
        public void TestIsAlphaQ_Q()
        {
            var result = AlbumServiceMethods.IsAlphaQ("Q");
            Assert.True(result, "Q is Q");
        }

        [Fact]
        public void TestIsAlphaQ_20()
        {
            var result = AlbumServiceMethods.IsAlphaQ("20");
            Assert.False(result, "20 is not Q");
        }

        [Fact]
        public void TestIsAlphaQ_XYZ()
        {
            var result = AlbumServiceMethods.IsAlphaQ("XYZ");
            Assert.False(result, "XYZ is not Q");
        }

        [Fact]
        public void TestIsAlphaQ_Symbol()
        {
            var result = AlbumServiceMethods.IsAlphaQ("!");
            Assert.False(result, "! is not Q");
        }

        [Fact]
        public void TestIsAlphaQ_EmptyString()
        {
            var result = AlbumServiceMethods.IsAlphaQ(string.Empty);
            Assert.False(result, "Empty.String is not Q");
        }

        [Fact]
        public void TestGetPhotoList_2()
        {
            var result = (System.Net.HttpWebResponse)AlbumServiceMethods.GetPhotoWebResponse("2");
            Assert.True(result.StatusCode==System.Net.HttpStatusCode.OK, "WebResponse status code is not OK");
        }

        [Fact]
        public void TestBuildPhotoList_notFound()
        {
            AlbumServiceMethods.GetPhotoList("400");
            var result = AlbumServiceMethods.BuildPhotoList();
            Assert.Contains("cannot be located", AlbumServiceMethods.GetUserMessage("photos"));
        }

    }
}
