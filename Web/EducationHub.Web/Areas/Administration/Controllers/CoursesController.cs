﻿namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Lessons;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Courses;
    using ViewModels.Administration.Courses;
    using Web.ViewModels.Categories;
    using Services.Data.Categories;

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesService coursesService;
        private readonly ILessonsService lessonsService;
        private readonly ICategoriesService categoriesService;

        public CoursesController(
            ICoursesService coursesService,
            ILessonsService lessonsService,
            ICategoriesService categoriesService)
        {
            this.coursesService = coursesService;
            this.lessonsService = lessonsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.coursesService.AllWithDeletedAsync<CourseAdminViewModel>());

        public IActionResult Create()
        {
            return this.RedirectToActionPermanent("Create", "Courses", new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.coursesService.GetByIdWithDeletedAsync<EditCourseAdminViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseAdminViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.coursesService.EditAsync(model.Id, model.Title, model.Description, model.IsDeleted, model.CategoryId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var lesoonsInCourse = this.lessonsService.DeleteAllInCourseAsync(id);
            await this.coursesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
