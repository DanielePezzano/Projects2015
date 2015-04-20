using Models.Base;
using System;

namespace _2015ProjectsBackEndWs.DTO
{
    public class BaseDto<T> where T:BaseEntity
    {
        private T _Model = default(T);

        public T Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public BaseDto(T model)
        {
            if (model == null) throw new ArgumentNullException();
            this._Model = model;
        }
    }
}