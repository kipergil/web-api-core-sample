using System.Collections.Generic;
using RunPath.WebApi.Domain;

namespace RunPath.WebApi.Services.Interfaces
{
    public interface IPhotoService
    {
        string ApiUrl { get; set; }
        List<Photo> GetAllByAlbumId(int albumId);
        List<Photo> GetAll();
        Photo GetById(int id);
    }
}