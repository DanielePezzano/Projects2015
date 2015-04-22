using _2015ProjectsBackEndWs.DTO;
using Models.Base;

namespace _2015ProjectsBackEndWs.DataMapper.Interface
{
    interface IDataMapper<T> where T : BaseEntity
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseDto<T> EntityToModel(T entity);
    }
}
