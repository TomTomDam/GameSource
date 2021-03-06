﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using GameSource.Services.GameSourceUser.Contracts;
using GameSource.ViewModels.GameSource.GameViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameSource.Controllers.GameSource
{
    [Route("games")]
    public class GamesController : Controller
    {
        private IGameService gameService;
        private IGenreService genreService;
        private IDeveloperService developerService;
        private IPublisherService publisherService;
        private IPlatformService platformService;
        private IReviewService reviewService;
        private IUserService userService;
        private IWebHostEnvironment hostingEnv;
        private UserManager<User> userManager;

        public GamesController(IGameService gameService, 
            IGenreService genreService, 
            IDeveloperService developerService, 
            IPublisherService publisherService, 
            IPlatformService platformService, 
            IReviewService reviewService, 
            IUserService userService,
            IWebHostEnvironment hostingEnv, 
            UserManager<User> userManager)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.developerService = developerService;
            this.publisherService = publisherService;
            this.platformService = platformService;
            this.reviewService = reviewService;
            this.userService = userService;
            this.hostingEnv = hostingEnv;
            this.userManager = userManager;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            GameIndexViewModel viewModel = new GameIndexViewModel
            {
                Games = gameService.GetAll(),
                Genres = genreService.GetAll(),
                Developers = developerService.GetAll(),
                Publishers = publisherService.GetAll(),
                Platforms = platformService.GetAll(),
                Reviews = reviewService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            if (id == 0)
                return NotFound();

            Game game = gameService.GetByID(id);
            if (game == null)
                return NotFound();

            GameDetailsViewModel viewModel = new GameDetailsViewModel
            {
                Game = game,
                Genre = genreService.GetByID(game.GenreID),
                Developer = developerService.GetByID(game.DeveloperID),
                Publisher = publisherService.GetByID(game.PublisherID),
                Platform = platformService.GetByID(game.PlatformID),
                Review = reviewService.GetByID(game.Reviews.Select(x => x.GameID = game.ID).FirstOrDefault()),
                SignedInUser = userManager.GetUserAsync(HttpContext.User).Result ?? null
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            GameCreateViewModel viewModel = new GameCreateViewModel();
            viewModel.Game = new Game();
            viewModel.Genres = genreService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Developers = developerService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Publishers = publisherService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Platforms = platformService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameCreateViewModel viewModel)
        {
            Game game = new Game
            {
                ID = viewModel.Game.ID,
                Name = viewModel.Game.Name,
                Description = viewModel.Game.Description,
                GenreID = viewModel.Game.GenreID,
                DeveloperID = viewModel.Game.DeveloperID,
                PublisherID = viewModel.Game.PublisherID,
                PlatformID = viewModel.Game.PlatformID,
                Reviews = viewModel.Game.Reviews,
                CoverImageFilePath = null
            };

            string coverImageFileName = Path.GetFileName(viewModel.Game.CoverImage.FileName);
            string filePath = Path.Combine(hostingEnv.WebRootPath, "images\\Game\\CoverImage", coverImageFileName);

            game.CoverImageFilePath = coverImageFileName;

            gameService.Insert(game);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return NotFound();

            Game game = gameService.GetByID(id);
            if (game == null)
                return NotFound();

            GameEditViewModel viewModel = new GameEditViewModel();
            viewModel.Game = game;

            viewModel.Genres = genreService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.GenreID = game.GenreID;

            viewModel.Developers = developerService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.DeveloperID = game.DeveloperID;

            viewModel.Publishers = publisherService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.PublisherID = game.PublisherID;

            viewModel.Platforms = platformService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.PlatformID = game.PlatformID;

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameEditViewModel viewModel)
        {
            Game game = gameService.GetByID(viewModel.Game.ID);

            game.Name = viewModel.Game.Name;
            game.Description = viewModel.Game.Description;
            game.GenreID = viewModel.GenreID;
            game.DeveloperID = viewModel.DeveloperID;
            game.PublisherID = viewModel.PublisherID;
            game.PlatformID = viewModel.PlatformID;

            string uniqueFileName = null;
            string coverImageFolder = Path.Combine(hostingEnv.WebRootPath, "images\\Game\\CoverImage");
            if (viewModel.Game.CoverImage != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Game.CoverImage.FileName;
                string filePath = Path.Combine(coverImageFolder, uniqueFileName);
                await viewModel.Game.CoverImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

                game.CoverImageFilePath = uniqueFileName;
                game.CoverImage = viewModel.Game.CoverImage;

                gameService.Update(game);
            }

            return RedirectToAction("Details", game);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Game game = gameService.GetByID(id);
            if (game == null)
                return NotFound();

            GameDeleteViewModel viewModel = new GameDeleteViewModel
            {
                Game = gameService.GetByID(id),
                Genre = genreService.GetByID(game.GenreID),
                Developer = developerService.GetByID(game.DeveloperID),
                Publisher = publisherService.GetByID(game.PublisherID),
                Platform = platformService.GetByID(game.PlatformID),
                Reviews = reviewService.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(GameDeleteViewModel viewModel)
        {
            Game game = gameService.GetByID(viewModel.Game.ID);
            if (game == null)
                return NotFound();

            gameService.Delete(game.ID);
            return RedirectToAction("Index");
        }
    }
}
