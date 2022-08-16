using FluentValidation;
using FluentValidation.Results;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Models;
using OpenLibrary.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;
        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationsResult)
        {
            foreach (var error in validationsResult.Errors) Notify(error.ErrorMessage);
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV,TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
