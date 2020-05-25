﻿using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using GameSource.Services.Contracts;
using System.Collections.Generic;

namespace GameSource.Services
{
    public class GenreService : IGenreService
    {
        private IGenreRepository repo;

        public GenreService(IGenreRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Genre> GetAll()
        {
            return repo.GetAll();
        }

        public Genre GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Genre genre)
        {
            repo.Insert(genre);
        }

        public void Update(Genre genre)
        {
            repo.Update(genre);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
