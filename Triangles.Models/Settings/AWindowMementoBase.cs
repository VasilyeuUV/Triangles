using System.Runtime.Serialization;

namespace Triangles.Models.Settings
{
    /// <summary>
    /// Абстрактный класс Memento 
    /// (для использования разными окнами))
    /// </summary>
    [DataContract]                                   // - этот класс - контракт данных, который может быть сериализован (необязателен, но повышает читаемость кода)
    internal abstract class AWindowMementoBase
    {
        /// <summary>
        /// Координата окна по горизонтали
        /// </summary>
        [DataMember(Name = "left")]                  // - помечает свойство, которое должно быть сериализовано, с именем, которое будет указано в JSON документе. Имя позволит жёстко не привязываться к имени свойства                                                     
        public double Left { get; set; }

        /// <summary>
        /// Координата окна по вертикали
        /// </summary>
        [DataMember(Name = "top")]
        public double Top { get; set; }

        /// <summary>
        /// Ширина окна
        /// </summary>
        [DataMember(Name = "width")]
        public double Width { get; set; }

        /// <summary>
        /// Высота окна
        /// </summary>
        [DataMember(Name = "height")]
        public double Height { get; set; }

        /// <summary>
        /// Флаг состояния окна развёрнутого в полный экран (true)
        /// </summary>
        [DataMember(Name = "isMaximized")]
        public bool IsMaximized { get; set; }
    }
}
