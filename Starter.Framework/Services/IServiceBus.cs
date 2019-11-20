﻿using System.Threading.Tasks;

namespace Starter.Framework.Services
{
    public interface IServiceBus
    {
        Task Send<T>(T entity);

        Task<T> Receive<T>() where T : new();
    }
}