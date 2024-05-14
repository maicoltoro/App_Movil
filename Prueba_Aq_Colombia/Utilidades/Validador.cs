using Prueba_Aq_Colombia.DTOs;

namespace Prueba_Aq_Colombia.Utilidades
{
    class Validador
    {
        public async Task<string> validadorDatos(TareaDto tareas)
        {
            var resultValidador = new TareaDtoValidator().Validate(tareas);
            string error = "";
            if (!resultValidador.IsValid)
            {
                foreach (var failure in resultValidador.Errors)
                {
                    error += $"Error: {failure.ErrorMessage}";
                }
            }
            return error;
        }
    }
}
