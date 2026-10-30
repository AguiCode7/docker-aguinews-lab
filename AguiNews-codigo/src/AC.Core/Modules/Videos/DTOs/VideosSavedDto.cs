namespace AC.Core.Modules.Videos.DTOs;

// Respuesta del POST: cuántos videos son nuevos y cuántos se actualizaron.
public record VideosSavedDto(int Created, int Updated);
