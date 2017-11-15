﻿using System.Threading.Tasks;

namespace RepositoryApp.Service.Services.Interfaces
{
    public interface IDirectoryService
    {
        Task CreateDirectory(string path);
        bool DirectoryExist(string path);
        Task RemoveDirectory(string path);
        Task RenameDirectory(string path, string newName);
    }
}