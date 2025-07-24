﻿namespace Medilab_Dapper.Repositories.ImageRepository
{
    public interface IImageService
    {
        /// <summary>
        /// Saves an image file from the computer to projects wwwroot/images folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns> Returns a strings value for the model's ImageUrl property </returns>
        Task<string> SaveImageAsync(IFormFile file, string folder);  // returns URL string
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
