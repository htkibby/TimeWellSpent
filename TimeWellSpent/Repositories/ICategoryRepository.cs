﻿using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public interface ICategoryRepository
    {
        void DeleteCategory(int id);
        List<Category> GetAllActivities();
        Category GetCategoryById(int id);
        void InsertCategory(Category activity);
        void UpdateCategory(Category activity);
    }
}