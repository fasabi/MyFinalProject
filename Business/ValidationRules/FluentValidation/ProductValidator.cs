using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(3);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün ismi A ile başlamalıdır !");

            //Eğer belirli bir değer için validation koyma istenirse when() özelliği kullanılır.
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(15).When(p => p.CategoryId == 5);
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
