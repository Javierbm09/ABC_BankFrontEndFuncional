using ABC_BankApi.Model;

namespace ABC_BankApi.Servicios
{
    public interface IServicio_Api
    {
        Task<List<Contacto>> Lista();
        Task<Contacto> Obtener(int id);
        Task<bool> Guardar(Contacto objeto);
        Task<bool> Editar(Contacto objeto);
        Task<bool> Eliminar(int id);

    }
}
