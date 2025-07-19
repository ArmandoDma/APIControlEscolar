using System.ComponentModel.DataAnnotations;
using System;

namespace APIControlEscolar.ValidationAttributes
{
    public class RangoFechaNacimientoAttribute : ValidationAttribute
    {
        private readonly int _minYear;

        public RangoFechaNacimientoAttribute(int minYear = 1950) // Puedes hacer el año mínimo configurable
        {
            _minYear = minYear;
            // Mensaje de error predeterminado si no se especifica uno en el atributo
            ErrorMessage = $"La fecha de nacimiento debe ser a partir del año {_minYear} y no puede ser en el futuro.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Si la propiedad no es requerida, el atributo [Required] debe manejar esto.
                // Si es requerida pero llega nula aquí, podría ser un problema.
                return ValidationResult.Success; // O new ValidationResult("La fecha de nacimiento es obligatoria.");
            }

            DateTime fechaNacimiento;

            // Intentar convertir el valor a DateTime
            if (value is DateTime)
            {
                fechaNacimiento = (DateTime)value;
            }
            else if (value is DateOnly) // Si estás usando DateOnly como en el modelo Alumno
            {
                fechaNacimiento = ((DateOnly)value).ToDateTime(TimeOnly.MinValue); // Convertir DateOnly a DateTime
            }
            else
            {
                // Si el tipo de dato no es DateTime o DateOnly, esto es un error de uso del atributo.
                return new ValidationResult("El atributo RangoFechaNacimientoAttribute solo puede aplicarse a propiedades de tipo DateTime o DateOnly.");
            }

            // Obtener la fecha actual (solo la parte de la fecha para comparar)
            DateTime fechaActual = DateTime.Today;

            // Validación: No debe ser anterior al año mínimo
            if (fechaNacimiento.Year < _minYear)
            {
                return new ValidationResult($"La fecha de nacimiento no puede ser anterior al año {_minYear}.", new[] { validationContext.MemberName });
            }

            // Validación: No debe ser una fecha en el futuro (no puede ser posterior a la fecha actual)
            if (fechaNacimiento > fechaActual)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser en el futuro.", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success; // La validación es exitosa
        }
    }
}