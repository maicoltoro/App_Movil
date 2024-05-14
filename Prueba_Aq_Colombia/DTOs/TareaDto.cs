using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation;

namespace Prueba_Aq_Colombia.DTOs
{
    public partial class TareaDto:ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string nombre;

        [ObservableProperty]
        public string descripcion;

        [ObservableProperty]
        public string estado;

        [ObservableProperty]
        public string categoria;
    }

    public class TareaDtoValidator : AbstractValidator<TareaDto>
    {
        public TareaDtoValidator()
        {
            RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre es obligatorio.").MaximumLength(50);
            RuleFor(x => x.Descripcion).NotNull().WithMessage("La descripcion es obligatoria.").MaximumLength(50);
            RuleFor(x => x.Estado).NotNull().WithMessage("El estado es obligatorio.").MaximumLength(50);
            RuleFor(x => x.Categoria).NotNull().WithMessage("La categoria es obligatoria.").MaximumLength(50);
        }
    }
}
