﻿using System.Drawing;
using System.Windows.Input;
using Triangles.ViewModels.Commands;

namespace Triangles.ViewModels.MainWindowViewModels
{
    /// <summary>
    /// Вьюмодель главного окна со свойсвами для отображения во вью
    /// </summary>
    public partial class MainWindowViewModel : AViewModelBase
    {
        private string? _nestingLevelMax;
        private Bitmap? _bitmap = new Bitmap(300, 300);

        private readonly AsyncCommand _openFileCommand;          // - команда открытия файла 


        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowViewModel()
        {
            _openFileCommand = new AsyncCommand(GetTrianglesCoordsAsync);
        }


        /// <summary>
        /// Максимальный уровень вложенности
        /// </summary>
        public string? NestingLevelMax
        { 
            get => _nestingLevelMax;
            set => Set(ref _nestingLevelMax, value);
        }


        /// <summary>
        /// 
        /// </summary>
        public Bitmap? Bitmap
        {
            get
            {
                using (Graphics g = Graphics.FromImage(_bitmap))
                {
                    g.Clear(Color.Transparent);
                    g.DrawLine(Pens.Black, 10, 10, 140, 140);
                }
                return _bitmap;
            }
        }


        /// <summary>
        /// Открытие файла
        /// </summary>
        public ICommand OpenFileCommand => _openFileCommand;



    }
}
