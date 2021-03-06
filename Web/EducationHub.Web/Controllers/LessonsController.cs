﻿namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;
    using Services.Data.Lessons;
    using ViewModels.Categories;
    using ViewModels.Lessons;

    public class LessonsController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ILessonsService lessonsService;

        public LessonsController(ICategoriesService categoriesService, ILessonsService lessonsService)
        {
            this.categoriesService = categoriesService;
            this.lessonsService = lessonsService;
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateLessonInputModel
            {
                CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

                return this.View(model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.lessonsService.CreateAsync(model.Title, model.Description, model.VideoUrl, userId, model.CategoryId);

            return this.RedirectToAction("MyResources", "Users");
        }

        public async Task<IActionResult> ByCategory(int id, int page = 1)
        {
            const int ItemsPerPage = 4;

            if (page < 1)
            {
                page = 1;
            }

            var viewModel = new PagingLessonsViewModel
            {
                CategoryId = id,
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.lessonsService.GetCountByCategory(id),
                Lessons = await this.lessonsService.GetByCategoryIdAsync<ByCategoryLessonViewModel>(id, page, ItemsPerPage),
            };

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;

                viewModel = new PagingLessonsViewModel
                {
                    CategoryId = id,
                    ItemsPerPage = ItemsPerPage,
                    PageNumber = page,
                    ItemsCount = this.lessonsService.GetCountByCategory(id),
                    Lessons = await this.lessonsService.GetByCategoryIdAsync<ByCategoryLessonViewModel>(id, page, ItemsPerPage),
                };
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.lessonsService.ByIdAsync<DetailsLessonViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> ByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = await this.lessonsService.GetByUserIdAsync<ListingLessonsViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.lessonsService.ByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLessonViewModel model)
        {
            var viewModel = await this.lessonsService.ByIdAsync<EditLessonViewModel>(model.Id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

                return this.View(model);
            }

            await this.lessonsService.EditAsync(model.Id, model.Title, model.Description, model.VideoUrl, model.CategoryId);

            this.TempData["Message"] = "Successfully changed.";
            return this.RedirectToAction(nameof(this.ByUser));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.lessonsService.ByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            this.TempData["Message"] = "Successfully deleted resource.";
            await this.lessonsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.ByUser));
        }
    }
}
