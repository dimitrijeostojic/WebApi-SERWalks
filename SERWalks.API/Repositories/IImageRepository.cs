using SERWalks.API.Models.Domain;

namespace SERWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
