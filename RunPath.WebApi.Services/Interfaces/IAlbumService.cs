using System.Collections.Generic;
using RunPath.WebApi.Domain;

namespace RunPath.WebApi.Services.Interfaces
{
    public interface IAlbumService
    {
        string ApiUrl { get; set; }
        List<Album> GetAllByUserId(int userId);
        List<Album> GetAll();
        Album GetById(int id);
    }
}