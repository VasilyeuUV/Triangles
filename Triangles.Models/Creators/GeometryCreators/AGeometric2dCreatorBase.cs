using Triangles.Contracts.Creators;

namespace Triangles.Models.Creators.GeometryCreators
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AGeometric2dCreatorBase : ICreator
    {

        //#################################################################################################################
        #region IGeometryCreator

        /// <summary>
        /// Создать геометрическую 2D фигуру
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>

        public abstract T Create<T>(IEnumerable<int> coords)
            where T : class;

        #endregion // IGeometryCreator
    }
}
