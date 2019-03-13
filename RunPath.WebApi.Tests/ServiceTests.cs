using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi.Tests
{
    public class ServiceTests : DependencyResolver
    {
        public ServiceTests()
        {
        }

        [Test]
        public void api_urls_are_valid()
        {
            Assert.IsTrue(PhotoService.ApiUrl != null);
            Assert.IsTrue(PhotoService.ApiUrl.Contains("photos"));
            Assert.IsTrue(PhotoService.ApiUrl.StartsWith("http"));

            Assert.IsTrue(AlbumService.ApiUrl != null);
            Assert.IsTrue(AlbumService.ApiUrl.Contains("albums"));
            Assert.IsTrue(AlbumService.ApiUrl.StartsWith("http"));
        }


        [Test]
        public void get_photo_by_id_with_valid_input()
        {
            var photo = PhotoService.GetById(1);
            Assert.IsNotNull(photo);
            Assert.IsTrue(photo.id == 1);
        }


        [Test]
        public void get_photo_by_id_with_not_valid()
        {
            var photo = PhotoService.GetById(-1);
            Assert.IsNull(photo);
        }


        [Test]
        public void get_album_by_id_valid_input()
        {
            var album = AlbumService.GetById(1);
            Assert.IsNotNull(album);
            Assert.IsTrue(album.id == 1);
        }


        [Test]
        public void get_album_by_id_not_valid_input()
        {
            var album = AlbumService.GetById(-1);
            Assert.IsNull(album);
        }

        [Test]
        public void get_album_by_user_id()
        {
            var albums = AlbumService.GetAllByUserId(1);
            Assert.IsNotNull(albums);
            Assert.IsTrue(albums.Any());
        }


        [Test]
        public void get_all_albums()
        {
            var albums = AlbumService.GetAll();
            Assert.IsNotNull(albums);
            Assert.IsTrue(albums.Any());
        }

        [Test]
        public void get_all_photos()
        {
            var photos = PhotoService.GetAll();
            Assert.IsNotNull(photos);
            Assert.IsTrue(photos.Any());
        }

    }

}