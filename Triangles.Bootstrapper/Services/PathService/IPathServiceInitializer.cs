using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles.Bootstrapper.Services.PathService
{
    /// <summary>
    /// Контракт для инициализации сервиса работы с путями к файлам и каталогам (для Bootstrapper-а)
    /// </summary>
    public interface IPathServiceInitializer
    {
        /// <summary>
        /// Инициализировать сервис
        /// </summary>
        void Initialize();
    }
}
