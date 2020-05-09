using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Wikeasy.ViewModels;

namespace WikeasyNUnitTest.ViewModels
{
    class HomeViewModelTest 
    {
        private HomeViewModel _vm;
        [SetUp]
        public void Setup()
        {
            _vm = new HomeViewModel();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void GetAmguitySolutionsTest()
        {
            // Arrange
            ObservableCollection<string> result = new ObservableCollection<string>();
            string description = "<p><b>Tom Holland</b> may refer to:</p>\n\n<ul><li><a href=\"/wiki/Tom_Holland_(Australian_footballer)\" title=\"Tom Holland (Australian footballer)\">Tom Holland (Australian footballer)</a> (1885–1946), Australian footballer</li>\n<li><a href=\"/wiki/Tom_Holland_(footballer,_born_1902)\" title=\"Tom Holland (footballer, born 1902)\">Tom Holland (footballer, born 1902)</a> (1902–1987) English footballer</li>\n<li><a href=\"/wiki/Tom_Holland_(artist)\" title=\"Tom Holland (artist)\">Tom Holland (artist)</a> (born 1936), American visual artist</li>\n<li><a href=\"/wiki/Tom_Holland_(director)\" title=\"Tom Holland (director)\">Tom Holland (director)</a> (born 1943), American film director</li>\n<li><a href=\"/wiki/Tom_Holland_(politician)\" title=\"Tom Holland (politician)\">Tom Holland (politician)</a> (born 1961), Kansas state senator</li>\n<li><a href=\"/wiki/Tom_Holland_(author)\" title=\"Tom Holland (author)\">Tom Holland (author)</a> (born 1968), English writer and historian</li>\n<li><a href=\"/wiki/Tom_Holland_(actor)\" title=\"Tom Holland (actor)\">Tom Holland (actor)</a> (born 1996), English actor</li></ul>\n\n";
            string expectation = "Tom Holland (Australian footballer)";
            // Process
            result = _vm.GetAmguitySolutions(description);

            // Assert
            Assert.AreEqual(expectation,result[0]);
            Console.WriteLine(result[0]);
        }
    }
}
