using dreamleague.shared.Properties;
using FluentValidation;

namespace dreamleague.shared.Services
{
    public abstract class GenericService<TRequest, TResponse> : IService<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public GenericService () { }
        public GenericService (IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        public async Task<TResponse> Execute(TRequest request)
        {
            try
            {
                if (_validator != null)
                    await _validator.ValidateAndThrowAsync(request);

                return await OnExecute(request);
            }
            catch (ValidationException exception)
            {
                throw new Exception(Resources.BadRequestError, exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"{Resources.InternalServerError} {exception.Message}", exception);
            }
        }

        protected abstract Task<TResponse> OnExecute(TRequest request);
    }
}
