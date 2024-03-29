﻿namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        Task<IEnumerable<T>> AllAsync<T>();

        Task<IEnumerable<T>> AllWithDeletedAsync<T>();

        Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId, int page = 1, int itemsPerPage = 10);

        Task<T> GetByIdAsync<T>(string id);

        Task<T> GetByIdWithDeletedAsync<T>(string id);

        Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId, int page, int itemsPerPage = 4);

        Task CreateAsync(string title, string description, string userId, int categoryId);

        Task DeleteAsync(string id);

        int GetCountByCategory(int id);

        Task EditAsync(string id, string title, string description, bool isDeleted, int categoryId);

        int GetAllWithDelethedCount();

        Task<IEnumerable<T>> GetAllWithDeletedAsync<T>(int page, int itemsPerPage);

        int GetCountByUser(string userId);
    }
}
