using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wikeasy.Models;
using Wikeasy.Services;

namespace WikeasyNUnitTest.Services
{
    class WikipediaApiDataServiceTest
    {
        static IWikipediaApiDataService _service;
        [SetUp]
        public void Setup()
        {
            _service = new WikipediaApiDataService();

        }

        [Test]
        public async Task GetWikiData()
        {
            // Arrage
            WikiData result = new WikiData();

            result = await _service.GetWikiData("Jack Dorsey");

            Console.WriteLine(result);
            Assert.Pass();
        }
    }
}
