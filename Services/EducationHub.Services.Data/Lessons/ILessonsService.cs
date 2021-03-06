﻿namespace EducationHub.Services.Data.Lessons
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILessonsService
    {
        Task CreateAsync(string title, string description, string videoUrl, string userId, int categoryId, string courseId = null);

        Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId, int page, int itemsPerPage);

        Task<T> ByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId);

        Task EditAsync(string id, string title, string description, string videoUrl, int categoryId, string courseId = null);

        Task DeleteAsync(string id);

        int GetCountByCategory(int id);
    }
}
