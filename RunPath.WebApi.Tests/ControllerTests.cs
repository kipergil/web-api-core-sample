using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using RunPath.WebApi.Controllers;
using RunPath.WebApi.Domain;

namespace RunPath.WebApi.Tests
{
    public class ControllerTests : DependencyResolver
    {
        public HomeController HomeController { get; set; }

        public ControllerTests()
        {
            HomeController = new HomeController(PhotoService, AlbumService, Cache);
        }

        [Test]
        public void get_endpoint_tests()
        {
            var action = HomeController.GetPhotos();
            TestActionResult<List<Photo>>(action.Result);

            var action2 = HomeController.GetPhotosById(1);
            TestActionResult<Photo>(action2.Result);

            var action3 = HomeController.GetAlbums();
            TestActionResult<List<Album>>(action3.Result);

            var action4 = HomeController.GetAlbumsById(1);
            TestActionResult<Album>(action4.Result);

            var action5 = HomeController.GetAlbumsByUserId(1);
            TestActionResult<List<Album>>(action5.Result);

            var action6 = HomeController.GetAlbumsWithPhotosByUserId(1, true);
            var value = TestActionResult<List<Album>>(action6.Result);
            Assert.IsTrue(value?.FirstOrDefault()?.albumPhotos.Any());
            Assert.IsTrue(value?.LastOrDefault()?.albumPhotos.Any());


            var action7 = HomeController.GetAlbumsWithPhotosByUserId(1, false);
            var value2 = TestActionResult<List<Album>>(action7.Result);
            Assert.IsTrue(!value?.FirstOrDefault()?.albumPhotos.Any());
            Assert.IsTrue(!value?.LastOrDefault()?.albumPhotos.Any());

        }

        private static T TestActionResult<T>(ActionResult<T> action)
        {
            var actionResult1 = action.Result as ObjectResult;
            var value = (T)actionResult1?.Value;
            var statusCode = actionResult1?.StatusCode;
            Assert.IsNotNull(value);

            if (value.GetType().IsGenericType)
            {
                //list should have elements
                var enumerable = value as IEnumerable;
                var list = enumerable?.Cast<object>().ToList();
                Assert.IsTrue(list?.Any());
            }

            Assert.IsTrue(statusCode == 200);
            return value;
        }
    }
}